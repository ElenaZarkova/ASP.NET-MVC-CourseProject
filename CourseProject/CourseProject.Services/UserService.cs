﻿using System;
using System.Linq;
using CourseProject.Data.Contracts;
using CourseProject.Services.Contracts;

namespace CourseProject.Services
{
    public class UsersService : IUsersService
    {
        private readonly IBetterReadsData data;

        public UsersService(IBetterReadsData data)
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
