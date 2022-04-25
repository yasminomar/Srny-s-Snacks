using SRNY.Models;
using System.Collections.Generic;

namespace SRNY.Repository
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void Delete(int id);
        void FirstUpdate(int id, Order order);
        List<Order> GetAll();
        Order GetById(int id);
        Order GetByUserId(string id);
        Order GetOrderByCartId(int cartId);
        void Update(int id, Order order);
        int countOrder();
        List<Order> GetAllByUserId(string Id);

    }
}