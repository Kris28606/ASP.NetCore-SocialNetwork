using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public void Add(TEntity entity);
        public void Update(TEntity entity);
        public List<TEntity> GetAll();
        public void Delete(TEntity entity);
        public TEntity SearchById(TEntity entity);
    }
}
