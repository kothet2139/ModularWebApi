
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ModularWebApi.Adapters.Security;
using ModularWebApi.Bootstrap;
using ModularWebApi.Modules.Orders.Application;
using ModularWebApi.Modules.Orders.Application.Behaviors;
using ModularWebApi.Modules.Orders.Application.Validators;
using ModularWebApi.Modules.Orders.Domain.Repositories;
using ModularWebApi.Modules.Orders.Infrastructure.Repositories;
using ModularWebApi.Modules.User.Domain;
using ModularWebApi.Modules.User.Domain.Repository;
using ModularWebApi.Modules.User.Infrastructure;
using ModularWebApi.Modules.User.Infrastructure.Repository;
using ModularWebApi.SharedKernel.Persistence;
using System.Reflection;
using System.Text;
using System.Text.Unicode;

namespace ModularWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var Configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("eCommerence"));
            });
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviors<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderCommandValidator>();

            builder.Services.Configure<JWTConfig>(Configuration.GetSection("JWT"));

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<IJWTProvider, JWTProvider>();

            builder.Services.AddControllers();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = Configuration["JWT:Audience"],
                    ValidIssuer = Configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(Configuration["JWT:APIKey"]!))
                });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseMiddleware<JwtMiddleware>();

            app.Run();
        }
    }
}
