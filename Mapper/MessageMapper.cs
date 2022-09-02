using Domain;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class MessageMapper : GenericMapper<MessageDto, Message>
    {
        public MessageDto toDto(Message entity)
        {
            MessageDto dto = new MessageDto();
            dto.MessageId=entity.MessageId;
            dto.FromId = entity.FromId;
            dto.ForId = entity.ForId;
            dto.MessageText = entity.MessageText;
            dto.Time = entity.Time;
            return dto;
        }

        public Message toEntity(MessageDto dto)
        {
            Message m = new Message();
            m.ForId = dto.ForId;
            m.FromId = dto.FromId;
            m.MessageText = dto.MessageText;
            return m;
        }
    }
}
