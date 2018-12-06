using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TeduCoreApp.Data.ViewModels.Bill;

namespace TeduCoreApp.WebApi.Signalr
{
    public class WebHub : Hub
    {
        public async Task NewMessage(BillViewModel message)
        {
            await Clients.All.SendAsync("messageReceived", message);
        }
        
    }
}