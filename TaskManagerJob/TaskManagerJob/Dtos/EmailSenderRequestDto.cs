using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerJob.Dtos
{
    public class EmailSenderRequestDto
    {
        public string email { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
    }
}
