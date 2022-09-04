using BusinesLogicLayer.Interfaces;
using BusinesLogicLayer.UnitOfWork;
using DataAccessLayer.UnitOfWork;
using Domain;
using Mapper;
using Microsoft.AspNetCore.Hosting;
using SocialNetwork.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.Implementation
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork unit;
        private readonly PostResponseMapper responseMapper;
        private readonly PostRequestMapper requestMapper;

        public PostService(UserContext context)
        {
            this.unit = new DataAccessLayer.UnitOfWork.UnitOfWork(context);
            this.responseMapper = new PostResponseMapper();
            this.requestMapper = new PostRequestMapper();
        }

        public bool Create(PostRequest entity)
        {
            User u = new User() { Id = entity.UserId };
            u = unit.UserRepository.SearchById(u);
            if (u != null)
            {
                try
                {
                    Post p = requestMapper.toEntity(entity);
                    DateTime now = DateTime.Now;
                    p.Date = now;
                    unit.PostRepository.Add(p);
                    unit.Save();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            };
            return false;
        }

        public List<PostResponse> GetAllMyPosts(int userId)
        {
            try
            {
                List<Post> posts=unit.PostRepository.GetMyPosts(userId);
                List<PostResponse> responses = new List<PostResponse>();
                posts.ForEach(p =>
                {
                    PostResponse response = responseMapper.toDto(p);
                    p.Reactions.ForEach(r =>
                    {
                        if (r.UserId == userId)
                        {
                            response.ILiked = true;
                        }
                    });
                    response.NumberOfLikes = p.Reactions.Count();
                    responses.Add(response);
                });
                return responses;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<PostResponse> GetAllForHome(int i)
        {
            List<Post> posts=unit.PostRepository.GetAllForHome(i, 0, 10);
            List<PostResponse> response = new List<PostResponse>();
            foreach(Post p in posts)
            {
                PostResponse pr = responseMapper.toDto(p);
                p.Reactions.ForEach(r =>
                {
                   if (r.UserId == i)
                   {
                       pr.ILiked = true;
                   }
                });
                pr.NumberOfLikes = p.Reactions.Count();
                response.Add(pr);
            }
            return response;
        }

        public bool LikeIt(int postId, string username)
        {
            User u = new User { UserName = username };
            u= unit.UserRepository.SearchByUsername(u);
            unit.PostRepository.LikeIt(postId, u.Id);
            unit.Save();
            return true;
        }

        public bool UnlikeIt(int postId, string username)
        {
            User u = new User { UserName = username };
            u = unit.UserRepository.SearchByUsername(u);
            bool result=unit.PostRepository.UnlikeIt(postId, u.Id);
            if(result)
            {
                unit.Save();
                return true;
            }
            return false;
        }
    }
}
