using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace AvorionServerManager
{

    public class ServerController : ApiController
    {
        // GET api/values 
        public static ManagerController _managerController;
        public HttpResponseMessage Get()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            StringBuilder resultBuilder = new StringBuilder();
                resultBuilder.Append("ServerProcessRunning:" + _managerController.ServerProcessRunning.ToString());
                resultBuilder.Append(Environment.NewLine);
                if (_managerController.LastUpdateTick != null)
                {
                    resultBuilder.Append("Last UpdateTick:" + _managerController.LastUpdateTick);
                }
                else
                {
                    resultBuilder.Append("Last UpdateTick:never");
                }
            

            resp.Content = new StringContent(resultBuilder.ToString(), System.Text.Encoding.UTF8, "text/plain");
            return resp;
        }
        [Authorize]
        public HttpResponseMessage Post([FromBody]string value)
        {
            StringBuilder resultBuilder = new StringBuilder();
            if (value == "startServer")
            {

                    _managerController.StartAvorionServer();
              
            }
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Content = new StringContent(resultBuilder.ToString(), System.Text.Encoding.UTF8, "text/plain");
            return resp;
        }

    }
}
