﻿namespace ClothingStore.Data.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get;}
        Task<List<Order>> GetAllOrdersAsync();        
        Task<Order> GetOrderByIdAsync(int id);

        Task CreateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
        Task SaveAsync();
    }
}
