using MediatR;
using ModularWebApi.Modules.Orders.Application.Commands;
using ModularWebApi.Modules.Orders.Domain.Events;
using ModularWebApi.Modules.Orders.Domain.Repositories;

namespace ModularWebApi.Modules.Orders.Application.Handlers
{
    public class ConfirmOrderHandler : IRequestHandler<ConfirmOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediator _mediator;

        public ConfirmOrderHandler(IOrderRepository orderRepository, IMediator mediator)
        {
            _orderRepository = orderRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.id);
            if (order == null)
                throw new InvalidOperationException("Order not found");

            order.Confirm();

            await _orderRepository.UpdateAsync(order);

            return true;
        }
    }
}
