using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using CourseProject.Services.Contracts;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace CourseProject.Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUsersService usersService;

        public ChatHub(IUsersService usersService)
        {
            if (usersService == null)
            {
                throw new ArgumentNullException("usersService");
            }

            this.usersService = usersService;
        }

        public override Task OnConnected()
        {
            string name = this.Context.User.Identity.GetUserName();

            Groups.Add(Context.ConnectionId, name);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = this.Context.User.Identity.GetUserName();

            Groups.Remove(Context.ConnectionId, name);

            return base.OnDisconnected(stopCalled);
        }

        public void CheckIfUserExists(string username)
        {
            var exists = this.usersService.CheckIfUserExists(username);
            // var exists = true;
            if (exists)
            {
                Clients.Caller.ChatWith(username);
            }
            else
            {
                Clients.Caller.ShowUsernameError(username);
            }
        }

        public void SendMessage(string username, string message)
        {
            var callerName = this.Context.User.Identity.GetUserName();
            Clients.Group(username).AddChatMessage(callerName, message);
        }
    }
}