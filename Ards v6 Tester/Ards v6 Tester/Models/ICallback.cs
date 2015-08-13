using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ards_v6_Tester.Models;

namespace Ards_v6_Tester.Models
{
    public interface ICallback
    {
        void print(Result item);
        void print(ResultList item);
    }
}
