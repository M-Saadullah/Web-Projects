using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Outfit.Models;

namespace Application
{
    public class ArticleServiceRepository:IArticleService
    {
        private readonly IArticle _Article;
        public ArticleServiceRepository(IArticle Article)
        {
            _Article = Article;
        }
        public int GetStockQuantity(int productId)
        {
            return _Article.GetStockQuantity(productId);
        }
        public void UpdateStockQuantity(int productId, int newQuantity)
        {
            _Article.UpdateStockQuantity(productId, newQuantity);
        }
        public void Add(Article article)
        {
            _Article.Add(article);  
        }
        public Article EditArticle(int ID)
        {
            return _Article.EditArticle(ID);
        }
        public void Update(Article article)
        {
            _Article.Update(article);
        }
        public void Delete(int ID)
        {
            _Article.Delete(ID);
        }
        public Article GetById(int ID)
        {
            return _Article.GetById(ID);
        }
        public List<Article> get(string categry)
        {
            return _Article.get(categry);
        }

        public IEnumerable<Article> GetAll()
        {
            return _Article.GetAll();
        }
        public Article GetItem(int ID)
        {
            return _Article.GetItem(ID); 
        }

    }
}
