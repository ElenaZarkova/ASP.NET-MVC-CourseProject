using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.Data.Contracts;
using CourseProject.Services.Contracts;

namespace CourseProject.Services
{
    public class UserService : IUsersService
    {
        private readonly IBetterReadsData data;

        public UserService(IBetterReadsData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            this.data = data;
        }

        public bool CheckIfUserExists(string username)
        {
            var exists = this.data.Users.All.Any(x => x.UserName == username);
            return exists;
        }
    }
}
