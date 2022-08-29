using BusinesLogicLayer.Implementation;
using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Hosting;
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
            UserService = new UserService(context);
        }
        public IPostService PostService { get; set; }
        public IUserService UserService { get; set; }
    }
}
