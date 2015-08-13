using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web.Http;
using Ards_v6_Tester.Models;
using System.Net.Http;

namespace Ards_v6_Tester.Controllers
{
    public class ResultController : ApiController
    {
        static readonly ICallback repository = new Callback();

        public HttpResponseMessage PostResult(Result item)
        {
            repository.print(item);
            var response = Request.CreateResponse<Result>(HttpStatusCode.OK, item);
            return response;
        }
    }
}
