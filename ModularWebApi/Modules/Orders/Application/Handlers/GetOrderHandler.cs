using MediatR;
using ModularWebApi.Modules.Orders.Application.Commands;
using ModularWebApi.Modules.Orders.Domain.Entities;
using ModularWebApi.Modules.Orders.Domain.Repositories;

namespace ModularWebApi.Modules.Orders.Application.Handlers
{
    public class GetOrderHandler : IRequestHandler<GetOrderCommand, Order>
    {
        private IOrderRepository _orderRepository;
        public GetOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(GetOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetByIdAsync(request.id);
        }
    }
}
