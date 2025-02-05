using System.Diagnostics;
using Application;
using Microsoft.AspNetCore.Mvc;
using The_Outfit.Models;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private IArticleService article;

        public HomeController(IArticleService article)
        {
            this.article = article;
        }

        public IActionResult Index()
        {
            //ArticleRepository articleRepository = new ArticleRepository();

            List<Article> articles = article.get("Sale");
            return View(articles);
        }

        public IActionResult SpecificArticle(int id)
        {
            //ArticleRepository articleRepository = new ArticleRepository();
            Article article1 = article.GetItem(id);

            return View(article1);
        }

       
        public ActionResult Search(string searchItem)
        {
            if (string.IsNullOrEmpty(searchItem))
            {
                return View(new List<Article>());
            }
            //ArticleRepository productRepository = new ArticleRepository();

            IEnumerable<Article> Products = article.get("Sale");
            var filteredProducts = Products
                .Where(p => p.Name.IndexOf(searchItem, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            return PartialView("_PartialArticleView", filteredProducts);
        }
    }
}
