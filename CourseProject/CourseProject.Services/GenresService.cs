using System;
using System.Collections.Generic;
using System.Linq;
using CourseProject.Data.Contracts;
using CourseProject.Models;
using CourseProject.Services.Contracts;

namespace CourseProject.Services
{
    public class GenresService : IGenresService
    {
        private readonly IBetterReadsData data;

        public GenresService(IBetterReadsData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("Better reads data cannot be null.");
            }

            this.data = data;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return this.data.Genres.All.ToList();
        }
    }
}
