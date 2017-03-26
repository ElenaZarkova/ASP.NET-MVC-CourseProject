using System.Security.Principal;
using System.Web;
using Moq;
using NUnit.Framework;
using CourseProject.Web.Hubs;
using CourseProject.Services.Contracts;
using Microsoft.AspNet.SignalR.Hubs;

namespace CourseProject.Web.Tests.Hubs.ChatHubTests
{
    [TestFixture]
    public class SendMessage_Should
    {
        [Test]
        public void SendMessageToCorrectGroup()
        {
            // Arrange
            var mockedService = new Mock<IUsersService>();
            var chatHub = new ChatHub(mockedService.Object);
            
            var mockedUserIdentity = new Mock<IIdentity>();
            var mockedContext = new Mock<HubCallerContext>();
            mockedContext.Setup(x => x.User.Identity).Returns(mockedUserIdentity.Object);
            chatHub.Context = mockedContext.Object;

            var mockedClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var mockedGroup = new Mock<IClient>();
            mockedClients.Setup(x => x.Group(It.IsAny<string>())).Returns(mockedGroup.Object);
            chatHub.Clients = mockedClients.Object;

            var username = "username";

            // Act
            chatHub.SendMessage(username, "message");

            // Assert
            var groupname = username + "_"; // caller name is empty
            mockedClients.Verify(x => x.Group(groupname), Times.Once);
        }

        [Test]
        public void SendCorrectMessage()
        {
            // Arrange
            var mockedService = new Mock<IUsersService>();
            var chatHub = new ChatHub(mockedService.Object);

            var mockedUserIdentity = new Mock<IIdentity>();
            var mockedContext = new Mock<HubCallerContext>();
            mockedContext.Setup(x => x.User.Identity).Returns(mockedUserIdentity.Object);
            chatHub.Context = mockedContext.Object;

            var mockedClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var mockedGroup = new Mock<IClient>();
            mockedGroup.Setup(x => x.AddChatMessage(It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            mockedClients.Setup(x => x.Group(It.IsAny<string>())).Returns(mockedGroup.Object);
            chatHub.Clients = mockedClients.Object;

            var message = "<script></script>";

            // Act
            chatHub.SendMessage("username", message);

            // Assert
            message = HttpUtility.HtmlEncode(message);
            mockedGroup.Verify(x => x.AddChatMessage(It.IsAny<string>(), message), Times.Once);
        }
    }
}
