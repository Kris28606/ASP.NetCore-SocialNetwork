using BusinesLogicLayer.Interfaces;
using BusinesLogicLayer.UnitOfWork;
using DataAccessLayer.UnitOfWork;
using Domain;
using Mapper;
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
                Post p = requestMapper.toEntity(entity);

                unit.PostRepository.Add(p);
                unit.Save();
                return true;
            };
            return false;
        }

        public List<PostResponse> GetAllMyPosts(int userId)
        {
            try
            {
                User u = new User { Id = userId };
                u = unit.UserRepository.SearchById(u);
                if (u != null)
                {
                    List<PostResponse> reponse = new List<PostResponse>();
                    foreach (Post post in u.Posts)
                    {
                        PostResponse post2 = responseMapper.toDto(post);
                        reponse.Add(post2);
                    }
                    return reponse;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<PostResponse> GetAllForHome(int i)
        {
            List<Post> posts=unit.PostRepository.GetAllForHome(i);
            List<PostResponse> response = new List<PostResponse>();
            foreach(Post p in posts)
            {
                PostResponse pr = responseMapper.toDto(p);
                response.Add(pr);
            }
            return response;
        }
    }
}
