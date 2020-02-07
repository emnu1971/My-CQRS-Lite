using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Favv.BeCert.Web.Hubs
{
    public class CertificateHub : Hub
    {
        public async Task GetUpdateAsync()
        {
            var i = 0;
            do
            {
                await Clients.All.SendAsync("ReceiveUpdate", $"update from hub {i}");
                // simulate some server side processing ...
                Thread.Sleep(1000);
            } while (i++ != 10);

            
        }
    }
}
