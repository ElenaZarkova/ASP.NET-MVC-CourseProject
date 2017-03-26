using System.Security.Principal;
using Microsoft.AspNet.SignalR;
using Moq;
using NUnit.Framework;
using CourseProject.Web.Hubs;
using CourseProject.Services.Contracts;
using Microsoft.AspNet.SignalR.Hubs;

namespace CourseProject.Web.Tests.Hubs.ChatHubTests
{
    [TestFixture]
    public class CheckIfUserExistsAndConnect_Should
    {
        [Test]
        public void CallUsersServiceCheckIfUserExistsWithCorrectUsername()
        {
            // Arrange
            var mockedService = new Mock<IUsersService>();
            var chatHub = new ChatHub(mockedService.Object);

            mockedService.Setup(x => x.CheckIfUserExists(It.IsAny<string>())).Verifiable();

            var mockedGroups = new Mock<IGroupManager>();
            chatHub.Groups = mockedGroups.Object;

            var mockedUserIdentity = new Mock<IIdentity>();
            var mockedContext = new Mock<HubCallerContext>();
            mockedContext.Setup(x => x.User.Identity).Returns(mockedUserIdentity.Object);

            chatHub.Context = mockedContext.Object;

            var mockedClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var mockedCaller = new Mock<IClient>();
            mockedClients.Setup(x => x.Caller).Returns(mockedCaller.Object);
            chatHub.Clients = mockedClients.Object;

            var username = "username";

            // Act
            chatHub.CheckUsernameAndConnect(username);

            // Assert
            mockedService.Verify(x => x.CheckIfUserExists(username), Times.Once);
        }

        [Test]
        public void CallConnect_WhenUserExists()
        {
            // Arrange
            var mockedService = new Mock<IUsersService>();
            var chatHub = new ChatHub(mockedService.Object);

            mockedService.Setup(x => x.CheckIfUserExists(It.IsAny<string>())).Returns(true);

            var mockedGroups = new Mock<IGroupManager>();
            mockedGroups.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            chatHub.Groups = mockedGroups.Object;

            var mockedUserIdentity = new Mock<IIdentity>();
            var mockedContext = new Mock<HubCallerContext>();
            mockedContext.Setup(x => x.User.Identity).Returns(mockedUserIdentity.Object);

            chatHub.Context = mockedContext.Object;

            var mockedClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var mockedCaller = new Mock<IClient>();
            mockedClients.Setup(x => x.Caller).Returns(mockedCaller.Object);
            chatHub.Clients = mockedClients.Object;

            var username = "username";

            // Act
            chatHub.CheckUsernameAndConnect(username);

            // Assert
            mockedGroups.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallChatWith_WhenUserExists()
        {
            // Arrange
            var mockedService = new Mock<IUsersService>();
            var chatHub = new ChatHub(mockedService.Object);

            mockedService.Setup(x => x.CheckIfUserExists(It.IsAny<string>())).Returns(true);

            var mockedGroups = new Mock<IGroupManager>();
            chatHub.Groups = mockedGroups.Object;

            var mockedUserIdentity = new Mock<IIdentity>();
            var mockedContext = new Mock<HubCallerContext>();
            mockedContext.Setup(x => x.User.Identity).Returns(mockedUserIdentity.Object);

            chatHub.Context = mockedContext.Object;

            var mockedClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var mockedCaller = new Mock<IClient>();
            mockedCaller.Setup(x => x.ChatWith(It.IsAny<string>())).Verifiable();
            mockedClients.Setup(x => x.Caller).Returns(mockedCaller.Object);
            chatHub.Clients = mockedClients.Object;

            var username = "username";

            // Act
            chatHub.CheckUsernameAndConnect(username);

            // Assert
            mockedCaller.Verify(x => x.ChatWith(username), Times.Once);
        }

        [Test]
        public void CallShowErrorWithCorrectUsername_WhenUserDoesNotExist()
        {
            // Arrange
            var mockedService = new Mock<IUsersService>();
            var chatHub = new ChatHub(mockedService.Object);

            mockedService.Setup(x => x.CheckIfUserExists(It.IsAny<string>())).Returns(false);

            var mockedClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var mockedCaller = new Mock<IClient>();
            mockedCaller.Setup(x => x.ShowUsernameError(It.IsAny<string>())).Verifiable();
            mockedClients.Setup(x => x.Caller).Returns(mockedCaller.Object);
            chatHub.Clients = mockedClients.Object;

            var username = "username";

            // Act
            chatHub.CheckUsernameAndConnect(username);

            // Assert
            mockedCaller.Verify(x => x.ShowUsernameError(username), Times.Once);
        }
    }
}
