using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class MessageDto : MyDto
    {
        public int MessageId { get; set; }
        public int FromId { get; set; }
        public int ForId { get; set; }
        public String MessageText { get; set; }
        public DateTime Time { get; set; }
    }
}
