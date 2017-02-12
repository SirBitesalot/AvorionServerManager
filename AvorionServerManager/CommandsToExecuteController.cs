using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace AvorionServerManager
{

    public class CommandsController: ApiController
    {
        [Authorize]
        public HttpResponseMessage Post([FromBody]AvorionServerCommand value)
        {
            CommandsApiData.AddCommand(value);
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            StringBuilder resultBuilder = new StringBuilder();
            resultBuilder.Append("Authorized");
            resp.Content = new StringContent(resultBuilder.ToString(), System.Text.Encoding.UTF8, "text/plain");
            return resp;
        }
    }
}
