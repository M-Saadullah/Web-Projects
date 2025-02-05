namespace The_Outfit.Models
{
    public interface IArticle
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
