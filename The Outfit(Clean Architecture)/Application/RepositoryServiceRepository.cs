using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Outfit.Models;

namespace Application
{
    public class RepositoryServiceRepository<TEntity>:IRepositoryService<TEntity>
    {
        private readonly IRepository<TEntity> _Repository;

        public RepositoryServiceRepository(IRepository<TEntity> repository)
        {
            _Repository = repository;
        }

        public void Add(TEntity entity)
        {
            _Repository.Add(entity);    
        }
        public TEntity GetById(int id)
        {
            return _Repository.GetById(id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _Repository.GetAll();
        }
        public void Update(TEntity entity)
        {
            _Repository.Update(entity);
        }
        public void Delete(int id)
        {
            _Repository.Delete(id); 
        }
    }
}
