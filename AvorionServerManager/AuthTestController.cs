using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Windows.Forms;

namespace AvorionServerManager
{

    public class AuthtestController : ApiController
    {
        // GET api/values 
        [Authorize]
        public HttpResponseMessage Get()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            StringBuilder resultBuilder = new StringBuilder();
            resultBuilder.Append("Authorized");
            resp.Content = new StringContent(resultBuilder.ToString(), System.Text.Encoding.UTF8, "text/plain");
            return resp;
        }
    }
}
