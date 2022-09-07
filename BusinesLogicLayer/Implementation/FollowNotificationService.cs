using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.Implementation
{
    public class FollowNotificationService : IFollowNotificationService
    {
        private readonly IUnitOfWork unit;

        public FollowNotificationService(UserContext context)
        {
            unit = new DataAccessLayer.UnitOfWork.UnitOfWork(context);
        }

        public bool Create(Notification entity)
        {
            throw new NotImplementedException();
        }
    }
}
