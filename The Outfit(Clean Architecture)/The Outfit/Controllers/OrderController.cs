using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using The_Outfit.Models;

namespace The_Outfit.Controllers
{

    public class OrderController : Controller
    {
        private ICartService cart;
        public OrderController(ICartService cart)
        {
            this.cart = cart;
        }
        static string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TheOutfit;Integrated Security=True";
        [HttpPost]
        public IActionResult AddToCart(CartItems c)
        {
            if(User.Identity.IsAuthenticated)
            {
                CartItems cartItem = null;
                //CartRepository cartRepository = new CartRepository(connection);
                cartItem = cart.GetItem(c.id,User.Identity.Name);
                if(cartItem != null)
                {
                    cartItem.Quantity += c.Quantity;
                    cart.Update(cartItem);
                }
                else
                {
                    cartItem = new CartItems
                    {
                        Name = c.Name,
                        Price = c.Price,
                        Discprice = c.Discprice,
                        Image = c.Image,
                        Category = c.Category,
                        id = c.id,
                        Quantity = c.Quantity,
                        UserId = User.Identity.Name
                    };
                    cart.Add(cartItem);
                }
            }
            else
            {

                var cartItems = HttpContext.Session.Get<List<CartItems>>("CartProducts") ?? new List<CartItems>();
                var item = cartItems.SingleOrDefault(x => x.id == c.id);
                if (item != null)
                {
                    item.Quantity += c.Quantity;
                }
                else
                {
                    cartItems.Add(c);
                }

                HttpContext.Session.Set("CartProducts", cartItems);


            }

            //// Update the cookie with the correct cart item count
            int currentCartItemCount = 0;
            if (HttpContext.Request.Cookies.TryGetValue("cartItemCount", out var cartItemCountStr))
            {
                currentCartItemCount = Convert.ToInt32(cartItemCountStr);
            }
            int newCartItemCount = currentCartItemCount + c.Quantity;
            HttpContext.Response.Cookies.Append("cartItemCount", newCartItemCount.ToString());


            // Redirect based on category
            //return c.Category switch
            //{
            //    "Men" => RedirectToAction("SpecificArticle", "Men", new { Id = c.id }),
            //    "Women" => RedirectToAction("SpecificArticle", "Women", new { Id = c.id }),
            //    "Sale" => RedirectToAction("SpecificArticle", "Sale", new { Id = c.id }),
            //    _ => RedirectToAction("SpecificArticle", "Sale", new { Id = c.id }),
            //};
            return Json(newCartItemCount);
        }
        public ViewResult ViewCart()
        {
            List<CartItems> cartItems = new List<CartItems>();
            if (User.Identity.IsAuthenticated) // Check if the user is authenticated
            {
                //CartRepository cartRepository = new CartRepository(connection);

                cartItems = cart.GetItemsByUser(User.Identity.Name);
            }
            else
            {
                cartItems = HttpContext.Session.Get<List<CartItems>>("CartProducts") ?? new List<CartItems>();

            }
            if (cartItems.Count > 0)
            {
                ViewBag.empty = 1;
            }
            else
            {
                ViewBag.empty = 1;
            }
                return View(cartItems);
        }
        public IActionResult removefromCart(int ID)
        {
            
            if (User.Identity.IsAuthenticated) // Check if the user is authenticated
            {
                // Remove from database
                //CartRepository cartRepository = new CartRepository(connection);

                var cartItems = cart.GetItem(ID, User.Identity.Name);
                if (cartItems != null)
                {
                    cart.Remove(cartItems);
                }
                
            }
            else
            {
            var cartItems = HttpContext.Session.Get<List<CartItems>>("CartProducts") ?? new List<CartItems>();
            var item = cartItems.SingleOrDefault(x => x.id == ID);
            cartItems.Remove(item);
            HttpContext.Session.Set("CartProducts", cartItems);

            }

            return RedirectToAction("viewCart");
        }

        public IActionResult EmptyCart()
        {
            return View();
        }
	}
}

