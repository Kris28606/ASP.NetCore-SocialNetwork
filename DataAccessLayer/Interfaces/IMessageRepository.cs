using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        public List<Message> GetChat(int forId, int fromId);
        public List<User> GetInboxUsers(int userId);
        public Message Send(Message m);

    }
}
