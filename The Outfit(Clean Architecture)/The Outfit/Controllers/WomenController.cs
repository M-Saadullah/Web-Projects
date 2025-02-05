using Application;
using Microsoft.AspNetCore.Mvc;
using The_Outfit.Models;

namespace The_Outfit.Controllers
{
    public class WomenController : Controller
    {
        private IArticleService article;

        public WomenController(IArticleService article)
        {
            this.article = article;
        }
        public IActionResult Index()
        {
            //ArticleRepository articleRepository = new ArticleRepository();
            List<Article> articles = article.get("Women");

            return View(articles);
            
        }

        public IActionResult SpecificArticle(int id)
        {
            //ArticleRepository articleRepository = new ArticleRepository();
            Article article1 = article.GetItem(id);

            return View(article1);
        }
    }
}
