using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using Dto;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork unit;
        private CommentResponseMapper mapper;

        public CommentService(IUnitOfWork unit)
        {
            this.unit = unit;
            mapper = new CommentResponseMapper();
        }
        public bool Create(CommentRequest entity)
        {
            throw new NotImplementedException();
        }

        public List<CommentResponse> GetComments(int postId)
        {
            List<Comment> comments = unit.CommentRepository.GetComments(postId);
            List<CommentResponse> commentsDto = new List<CommentResponse>();
            comments.ForEach(c =>
            {
                commentsDto.Add(mapper.toDto(c));
            });
            return commentsDto;
        }

        public CommentResponse PostComment(CommentRequest dto)
        {
            User u = new User { UserName = dto.Username };
            u = unit.UserRepository.SearchByUsername(u);
            Post p = new Post { PostId = dto.PostId };
            p = unit.PostRepository.SearchById(p);
            if (p.UserId == u.Id)
            {
                return null;
            }
            Comment c = new Comment();
            c.CommentText = dto.CommentText;
            c.PostId = dto.PostId;
            c.User = u;
            c.DatumVreme = DateTime.Now;
            Comment result = unit.CommentRepository.PostComment(c);
            if (result != null)
            {
                unit.Save();
                return mapper.toDto(c);
            }
            return null;
        }
    }
}
