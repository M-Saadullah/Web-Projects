using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Outfit.Models;

namespace Application
{
    public class OrderServiceRepository:IOrderService
    {
        private readonly IOrder _Order;

        public OrderServiceRepository(IOrder order)
        {
            _Order = order;
        }
        public void Add(OrderDetail o)
        {
            _Order.Add(o);
        }

        public OrderDetail GetById(int id)
        {
            return _Order.GetById(id);
        }

        public List<CartItems> getOrderProducts(string id)
        {
            return _Order.getOrderProducts(id);
        }

        public void AddOrderProduct(List<OrderProduct> product)
        {
            _Order.AddOrderProduct(product);
        }

        public void deleteCartItems(string userId)
        {
            _Order.deleteCartItems(userId);
        }
    }
}
