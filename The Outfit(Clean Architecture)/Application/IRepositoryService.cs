using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IRepositoryService<TEntity>
    {
        public void Add(TEntity entity);
        public TEntity GetById(int id);
        public IEnumerable<TEntity> GetAll();
        public void Update(TEntity entity);
        public void Delete(int id);
    }
}
