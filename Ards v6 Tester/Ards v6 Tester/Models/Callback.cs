using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ards_v6_Tester.Models
{
    public class Callback:ICallback
    {
        public void print(Result item)
        {
            var form = Form1.Instance;
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            form.UpdatingTextBox(msg);
        }

        public void print(ResultList item)
        {
            var form = Form1.Instance;
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            form.UpdatingTextBox(msg);
        }
    }
}
