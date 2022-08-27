using BusinesLogicLayer.Implementation;
using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        public UnitOfWorkService(UserContext context)
        {
            PostService = new PostService(context);
        }
        public IPostService PostService { get; set; }
 
    }
}
