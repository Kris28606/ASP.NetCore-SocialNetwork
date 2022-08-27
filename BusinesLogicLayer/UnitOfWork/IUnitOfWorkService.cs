using BusinesLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        public IPostService PostService { get; set; }
    }
}
