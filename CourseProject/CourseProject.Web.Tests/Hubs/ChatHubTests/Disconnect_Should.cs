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
    public class Disconnect_Should
    {
        [Test]
        public void CallGroupsAdd()
        {
            // Arrange
            var mockedService = new Mock<IUsersService>();
            var chatHub = new ChatHub(mockedService.Object);

            var mockedGroups = new Mock<IGroupManager>();
            mockedGroups.Setup(x => x.Remove(It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            chatHub.Groups = mockedGroups.Object;

            var mockedUserIdentity = new Mock<IIdentity>();
            var mockedContext = new Mock<HubCallerContext>();
            mockedContext.Setup(x => x.User.Identity).Returns(mockedUserIdentity.Object);

            chatHub.Context = mockedContext.Object;

            // Act
            chatHub.Disconnect("username");

            // Assert
            mockedGroups.Verify(x => x.Remove(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallGroupsAddWithCorrectConnectionId()
        {
            // Arrange
            var mockedService = new Mock<IUsersService>();
            var chatHub = new ChatHub(mockedService.Object);

            var mockedGroups = new Mock<IGroupManager>();
            mockedGroups.Setup(x => x.Remove(It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            chatHub.Groups = mockedGroups.Object;

            var mockedUserIdentity = new Mock<IIdentity>();
            var mockedContext = new Mock<HubCallerContext>();
            mockedContext.Setup(x => x.User.Identity).Returns(mockedUserIdentity.Object);

            var connectionId = "12345";
            mockedContext.Setup(x => x.ConnectionId).Returns(connectionId);

            chatHub.Context = mockedContext.Object;

            // Act
            chatHub.Disconnect("username");

            // Assert
            mockedGroups.Verify(x => x.Remove(connectionId, It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallGroupsAddWithCorrectGroupName()
        {
            // Arrange
            var mockedService = new Mock<IUsersService>();
            var chatHub = new ChatHub(mockedService.Object);

            var mockedGroups = new Mock<IGroupManager>();
            mockedGroups.Setup(x => x.Remove(It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            chatHub.Groups = mockedGroups.Object;

            var mockedUserIdentity = new Mock<IIdentity>();
            var mockedContext = new Mock<HubCallerContext>();
            mockedContext.Setup(x => x.User.Identity).Returns(mockedUserIdentity.Object);

            chatHub.Context = mockedContext.Object;

            // caller username is empty string
            var username = "username";
            var groupname = "_" + username;

            // Act
            chatHub.Disconnect(username);

            // Assert
            mockedGroups.Verify(x => x.Remove(It.IsAny<string>(), groupname), Times.Once);
        }
    }
}
