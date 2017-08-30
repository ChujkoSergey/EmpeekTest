using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpeekTest.Model.Messages
{
    public class InfoRequestMessage
    {
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
