﻿using Domain;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.Interfaces
{
    public interface IFollowNotificationService : INotificationService
    {
        public void CreateFollow(int id, string username);
        public List<FollowNotificationDto> GetAllForUser(int id);
        public void ConfirmFollow(int userId, int followId);
    }
}
