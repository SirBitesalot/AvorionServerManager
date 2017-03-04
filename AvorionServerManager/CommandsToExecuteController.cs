using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using AvorionServerManager.Commands;
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
            resultBuilder.Append("Success");
            resp.Content = new StringContent(resultBuilder.ToString(), System.Text.Encoding.UTF8, "text/plain");
            return resp;
        }
    }
}
