using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;

namespace BusinesLogicLayer.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork unit;
        public NotificationService(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public bool Create(Notification entity)
        {
            throw new NotImplementedException();
        }
    }
}
