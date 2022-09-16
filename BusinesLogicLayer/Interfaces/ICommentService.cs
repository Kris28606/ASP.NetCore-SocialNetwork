using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogicLayer.Interfaces
{
    public interface ICommentService : IService<CommentRequest>
    {
        public List<CommentResponse> GetComments(int postId);
        public CommentResponse PostComment(CommentRequest request);
    }
}
