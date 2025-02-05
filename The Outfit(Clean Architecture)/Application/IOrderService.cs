using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Outfit.Models;

namespace Application
{
    public interface IOrderService
    {
        public void Add(OrderDetail o);

        public OrderDetail GetById(int id);

        public List<CartItems> getOrderProducts(string id);

        public void AddOrderProduct(List<OrderProduct> product);

        public void deleteCartItems(string userId);

    }
}
