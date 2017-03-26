﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using CourseProject.Data.Contracts;

namespace CourseProject.Data.Repositories
{
    public class EfRepository<T> : IEfRepository<T>
        where T : class
    {
        private readonly IBetterReadsDbContext dbContext;
        private readonly IDbSet<T> dbSet;

        public EfRepository(IBetterReadsDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("Database context cannot be null.");
            }

            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<T>();
        }

        public IQueryable<T> All
        {
            get
            {
                return this.dbSet;
            }
        }
       
        public T GetById(object id)
        {
            return this.dbSet.Find(id);
        }

        public void Add(T entity)
        {
            var entry = this.GetAttachedEntry(entity);
            entry.State = EntityState.Added;
        }

        public void Delete(T entity)
        {
            var entry = this.GetAttachedEntry(entity);
            entry.State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            var entry = this.GetAttachedEntry(entity);
            entry.State = EntityState.Modified;
        }

        private DbEntityEntry GetAttachedEntry(T entity)
        {
            var entry = this.dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }

            return entry;
        }
    }
}
