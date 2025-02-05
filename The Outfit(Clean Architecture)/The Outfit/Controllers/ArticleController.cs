using System.Diagnostics;
using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using The_Outfit.Models;
using WebApplication2.Models;

namespace The_Outfit.Controllers
{
    [Authorize(Policy = "Admin_only")]
    public class ArticleController : Controller
    {
        private IArticleService article;
        private readonly ILogger<ArticleController> _logger;
        private readonly IWebHostEnvironment _env;

        public ArticleController(ILogger<ArticleController> logger, IWebHostEnvironment env,IArticleService article)
        {
            _logger = logger;
            this.article = article;
            _env = env;
        }
        [HttpGet]
        public ViewResult AddArticle()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult AddArticle(string name,string color_details,decimal price,int quantity,string category,string features,List<IFormFile> files)
        {
            Article article1 = new Article();
            article1.Name = name;
            article1.Price = price;
            article1.Quantity = quantity;
            article1.Category = category;
            article1.Features = features;
            article1.colorDetails = color_details;
            string wwwrootPath = _env.WebRootPath;
            string path = Path.Combine(wwwrootPath, "ArticleImages");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string allimages = string.Empty;
            int i = 0;
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(path, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    string imagepath = Path.Combine("~/ArticleImages/", fileName);
                    i++;
                    if (i != files.Count)
                    {
                        allimages = allimages + imagepath + ",";
                    }
                    else
                    {
                        allimages = allimages + imagepath;

                    }
                }
            }
            article1.Images = allimages;
            //ArticleRepository articleRepository = new ArticleRepository();
            article.Add(article1);
            return View("ViewArticle", article.GetAll());
        }
        [AllowAnonymous]
        public ViewResult ViewArticle()
        {
            //ArticleRepository articleRepository = new ArticleRepository();
            
            return View(article.GetAll());
        }

        public IActionResult RemoveArticle(int ID)
        {
            //ArticleRepository articleRepository = new ArticleRepository();
            article.Delete(ID);
            return RedirectToAction("ViewArticle");
        }

        public ViewResult EditArticle(int id)
        {
            //ArticleRepository articleRepository = new ArticleRepository();
            Article p = article.EditArticle(id);
            return View("EditArticle", p);
            
        }

        [HttpPost]
        public IActionResult EditArticle(int id,string name, string color_details, decimal price, int quantity, string category, string features,string files)

        {
            Article article1 = new Article();
            article1.id = id;
            article1.Name = name;
            article1.colorDetails = color_details;
            article1.Price = price;
            article1.Quantity = quantity;
            article1.Category = category;
            article1.Features = features;
            article1.Images = files;
            //ArticleRepository articleRepository = new ArticleRepository();
            article.Update(article1);
            return RedirectToAction("ViewArticle", "Article");
        }

        public ViewResult Details(int id)
        {
            //ArticleRepository articleRepository = new ArticleRepository();
            Article p = article.GetById(id);
            return View("Details", p);
        }

       
        public IActionResult UploadFiles(List<IFormFile> myfile)
        {
            string wwwrootPath = _env.WebRootPath;
            string path = Path.Combine(wwwrootPath, "Uploadedfiles");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (var file in myfile)
            {
                if (file.Length > 0)
                {
                    string filePath = Path.Combine(path, file.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
