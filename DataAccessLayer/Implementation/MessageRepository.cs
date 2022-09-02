using DataAccessLayer.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Implementation
{
    public class MessageRepository : IMessageRepository
    {
        private readonly UserContext context;

        public MessageRepository(UserContext context)
        {
            this.context = context;
        }

        public List<User> GetInboxUsers(int userId)
        {
            List<User> users = new List<User>();
            List<Message> messages= context.Messages.Include(m => m.FromUser).Where(m => m.ForId == userId).ToList();
            messages.ForEach(m =>
            {
                if (!users.Contains(m.FromUser) && m.FromId!=userId)
                {
                    users.Add(m.FromUser);
                }
            });
            messages=context.Messages.Include(m=> m.ForUser).Where(m=> m.FromId==userId).ToList();
            messages.ForEach(m =>
            {
                if (!users.Contains(m.ForUser) && m.ForId != userId)
                {
                    users.Add(m.ForUser);
                }
            });
            return users;
        }
        public void Add(Message entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Message entity)
        {
            throw new NotImplementedException();
        }

        public List<Message> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Message> GetChat(int forId, int fromId)
        {
            List<Message> tudje= context.Messages.Where(m => m.FromId == fromId && m.ForId == forId).ToList();
            List<Message> moje= context.Messages.Where(m => m.FromId == forId && m.ForId == fromId).ToList();
            moje.ForEach(m =>
            {
                tudje.Add(m);
            });
            tudje=tudje.OrderBy(t => t.Time).ToList();
            return tudje;
        }

        public Message SearchById(Message entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Message entity)
        {
            throw new NotImplementedException();
        }
    }
}
