using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerJob.Utilities
{
    public class PostRequest<T>
    {
        public string Url { get; set; }
        public T Data { get; set; }

    }
}