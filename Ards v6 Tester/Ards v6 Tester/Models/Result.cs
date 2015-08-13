using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ards_v6_Tester.Models
{
    public class ResInfo
    {
        public string Extention { get; set; }
        public string DialHostName { get; set; }
    }

    public class Result
    {
        public string SessionID { get; set; }
        public ResInfo ResourceInfo { get; set; }
    }
}
