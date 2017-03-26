using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Web.Tests.Hubs.ChatHubTests
{
    public interface IClient
    {
        void ShowUsernameError(string username);

        void ChatWith(string username);

        void AddChatMessage(string callerName, string message);
    }
}
