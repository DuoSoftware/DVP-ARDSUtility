using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ards_v6_Tester
{
    public class CommonLoader
    {
        private static CommonLoader _instance;
        private static string _ardsServer;

        private CommonLoader()
        {
            _ardsServer = ConfigurationManager.AppSettings["ArdsServer"];
        }

        public static CommonLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CommonLoader();
                }

                return _instance;
            }
        }

        public string ArdsServer
        {
            get
            {
                return _ardsServer;
            }
        }
    }
}
