﻿namespace ClothingStore.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync();
        //Task<Order> GetOrderAsync(int number); do it
        Task<Order> GetOrderByIdAsync(int id);

        Task CreateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
        Task SaveAsync();
    }
}
