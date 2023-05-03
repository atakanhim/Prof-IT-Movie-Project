using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Hubs
{
    public class MovieHub : Hub
    {
        //TODO: Gelen ve giden yorumalr için paramtere yazılacak
        public async Task SendComment() 
        {
            await Clients.All.SendAsync("ReceiveComment");
        }
    }
}
