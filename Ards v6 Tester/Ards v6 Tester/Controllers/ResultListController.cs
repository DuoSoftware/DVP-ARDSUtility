using Ards_v6_Tester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Ards_v6_Tester.Controllers
{
    public class ResultListController : ApiController
    {
        static readonly ICallback repository = new Callback();

        public HttpResponseMessage PostResultList(ResultList item)
        {
            repository.print(item);
            var response = Request.CreateResponse<ResultList>(HttpStatusCode.OK, item);
            return response;
        }
    }
}
