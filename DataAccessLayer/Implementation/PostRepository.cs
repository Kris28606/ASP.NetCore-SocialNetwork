﻿using DataAccessLayer.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Implementation
{
    public class PostRepository : IPostRepository
    {
        private readonly UserContext context;

        public PostRepository(UserContext context)
        {
            this.context = context;
        }

        public void Add(Post entity)
        {
            context.Add(entity);
        }

        public void Delete(Post entity)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Post> GetAllForHome(int i, int skip, int take)
        {
            User user = context.Users.Include(u=> u.Following).SingleOrDefault(u => u.Id == i);
            List<Post> posts = new List<Post>();
            user.Following.ForEach(u => posts.AddRange(u.Posts));
            posts=posts.OrderByDescending(s => s.Date).ToList();
            return posts.Skip(skip).Take(take).ToList();

        } 

        public Post SearchById(Post entity)
        {
            return context.Posts.SingleOrDefault(s => s.PostId == entity.PostId);
        }

        public void Update(Post entity)
        {
            throw new NotImplementedException();
        }
    }
}
