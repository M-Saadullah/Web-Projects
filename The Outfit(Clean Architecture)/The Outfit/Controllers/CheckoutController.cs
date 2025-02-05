using Application;
using Microsoft.AspNetCore.Mvc;
using The_Outfit.Models;

namespace The_Outfit.Controllers
{
    
    public class CheckoutController : Controller
    {
        private IOrderService order;
        private ICartService cart;
        private IArticleService article;
        public CheckoutController(IOrderService order, ICartService cart, IArticleService article)
        {
            this.order = order;
            this.cart = cart;
            this.article = article;
        }

        static string connect = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"TheOutfit\";Integrated Security=True;";
        [HttpGet]
        public ViewResult OrderProcessing()
        {
            return View();
        }
        [HttpPost]
        public IActionResult OrderProcessing(OrderDetail o)
        {
            Thread.Sleep(1000);
            long timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Random random = new Random((int)timestamp);
            o.Id = random.Next();
            TempData["Order-Id"] = o.Id;
            o.PlacedAt = DateTime.Now.Date.ToString("dd-MM-yyyy");
            List<CartItems> cartItems = new List<CartItems>();
            if (User.Identity.IsAuthenticated) // Check if the user is authenticated
            {
                
                cartItems = cart.GetItemsByUser(User.Identity.Name);
                foreach (var c in cartItems)
                {
                    int availableStock = article.GetStockQuantity(c.id);
                    if (c.Quantity > availableStock)
                    {
                        TempData["ErrorCheckout"] =$"Sorry You Are too Late for {c.Name} Requested quantity is not available in stock.";
                        return RedirectToAction("ViewCart", "Order");
                    }
                }
            }
            else
            {
                // Fetch cart items from the session for anonymous users
                cartItems = HttpContext.Session.Get<List<CartItems>>("CartProducts") ?? new List<CartItems>();
                foreach (var c in cartItems)
                {
                    int availableStock = article.GetStockQuantity(c.id);
                    if (c.Quantity > availableStock)
                    {
                        TempData["ErrorCheckout"] = $"Sorry You Are too Late...Requested quantity for {c.Name} is not available in stock.";
                        return RedirectToAction("ViewCart", "Order");
                    }
                }
            }
            foreach (var c in cartItems)
            {
                int availableStock = article.GetStockQuantity(c.id);
                article.UpdateStockQuantity(c.id, availableStock - c.Quantity);
            }
            var productIds = cartItems.Select(item => item.id).ToList();
            List<OrderProduct> orderProducts = new List<OrderProduct>();
            foreach (var pid in productIds)
            {
                orderProducts.Add(new OrderProduct { OrderId = o.Id, ProductId = pid });
            }
            order.Add(o);
            order.AddOrderProduct(orderProducts);
            return RedirectToAction("Complete", "Checkout");
        }
        public ViewResult Error()
        {
            return View();
        }
        //public ViewResult Complete()
        //{
        //    int id = Convert.ToInt32(TempData["Order-Id"]);
        //    CustomerOrder customerOrder = new CustomerOrder();
        //    customerOrder.OrderDetail = order.getOrder(id);
        //    List<CartItems> cartItems = new List<CartItems>();
        //    if (User.Identity.IsAuthenticated) // Check if the user is authenticated
        //    {

        //        customerOrder.Products = order.getOrderProducts(User.Identity.Name);
        //        order.deleteCartItems(User.Identity.Name);
        //    }
        //    else
        //    {
        //        // Fetch cart items from the session for anonymous users
        //        List<CartItems> c = new List<CartItems>();
        //        cartItems = HttpContext.Session.Get<List<CartItems>>("CartProducts") ?? new List<CartItems>();
        //        customerOrder.Products = cartItems;
        //        HttpContext.Session.Set("CartProducts", c);
        //    }
        //    return View(customerOrder);
        //}
    }
}
