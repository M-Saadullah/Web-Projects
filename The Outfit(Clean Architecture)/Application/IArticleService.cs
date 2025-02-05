using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Outfit.Models;

namespace Application
{
    public interface IArticleService
    {
        public int GetStockQuantity(int productId);
        public void UpdateStockQuantity(int productId, int newQuantity);
        public void Add(Article article);
        public Article EditArticle(int ID);
        public void Update(Article article);
        public void Delete(int ID);
        public Article GetById(int ID);
        public List<Article> get(string categry);

        public IEnumerable<Article> GetAll();
        public Article GetItem(int ID);
    }
}
