using DataAccessLayer.Implementation;
using DataAccessLayer.Interfaces;
using Domain;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserContext context;

        public UnitOfWork(UserContext context)
        {
            this.UserRepository = new UserRepository(context);
            this.context = context;
        }

        public IUserRepository UserRepository { get; set; }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}