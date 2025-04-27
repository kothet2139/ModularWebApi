using MediatR;
using ModularWebApi.Modules.Orders.Application.Commands;
using ModularWebApi.Modules.Orders.Domain.Entities;
using ModularWebApi.Modules.Orders.Domain.Repositories;

namespace ModularWebApi.Modules.Orders.Application.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderRepository.AddAsync(request.order);

            return request.order.Id;
        }
    }
}
