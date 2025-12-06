using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Domain.Entities.Products;
using _2nd.Semester.Eksamen.Domain.Entities.Products.BookingProducts;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces;

namespace _2nd.Semester.Eksamen.Application.Services.BookingServices
{
    public class OrderLineService : IOrderLineService
    {
        private readonly IOrderLineRepository _orderLineRepository;

        public OrderLineService(IOrderLineRepository orderLineRepository)
        {
            _orderLineRepository = orderLineRepository;
        }

        public Task AddOrderLineAsync(OrderLine orderLine)
            => _orderLineRepository.AddOrderLineAsync(orderLine);

        public Task<List<OrderLine>> GetOrderLinesByOrderIdAsync(int orderId)
            => _orderLineRepository.GetOrderLinesByOrderIdAsync(orderId);

        public Task UpdateOrderLineAsync(OrderLine orderLine)
            => _orderLineRepository.UpdateOrderLineAsync(orderLine);

        public Task DeleteOrderLineAsync(OrderLine orderLine)
            => _orderLineRepository.DeleteOrderLineAsync(orderLine);
    }
}
