using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Implementation
{
    public class ReactionRepository : IReactionRepository
    {
        private readonly UserContext context;

        public ReactionRepository(UserContext context)
        {
            this.context = context;
        }

        public void Add(Reaction entity)
        {
            context.Add(entity);
        }

        public void Delete(Reaction entity)
        {
            throw new NotImplementedException();
        }

        public List<Reaction> GetAll()
        {
            throw new NotImplementedException();
        }

        public Reaction SearchById(Reaction entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Reaction entity)
        {
            throw new NotImplementedException();
        }
    }
}
