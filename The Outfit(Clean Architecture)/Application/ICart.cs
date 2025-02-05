using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Outfit.Models;

namespace Application
{
    public interface ICart
    {
        public CartItems GetItem(int id, string userId);
        public void Add(CartItems cartItem);
        public void Update(CartItems cartItem);
        public List<CartItems> GetItemsByUser(string userId);
        public void Remove(CartItems cartItem);
    }
}
