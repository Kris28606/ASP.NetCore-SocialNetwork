using Domain;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class CommentResponseMapper : GenericMapper<CommentResponse, Comment>
    {
        UserMapper userMapper = new UserMapper();
        public CommentResponse toDto(Comment entity)
        {
            CommentResponse res = new CommentResponse();
            res.CommentText = entity.CommentText;
            res.User = userMapper.toDto(entity.User);
            res.PostId = entity.PostId;
            res.Date = entity.DatumVreme;
            return res;
        }

        public Comment toEntity(CommentResponse dto)
        {
            return null;
        }
    }
}
