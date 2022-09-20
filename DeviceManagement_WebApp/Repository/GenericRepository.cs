using DeviceManagement_WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DeviceManagement_WebApp.Repository
{
    //This class is where all the code is written once to follow the DRY (Don't Repeat Yourself) principle
    //Refer to the IGenericRepository which is an interface that contains all the method definitions
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ConnectedOfficeContext _context;

        public GenericRepository(ConnectedOfficeContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public T GetById(Guid? id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        //Custom code I wrote that were not provided so that all outcomes can be achieved:

        //Update an existing record
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        //Save changes of any changes made to an existing record
        public void Save()
        {
            _context.SaveChanges();
        }

        //Check if ID exists then return true if it exists or false if it does not exist
        public bool Any(Guid? id)
        {
            if (GetById(id) != null)
                return true;

            return false;
        }
    }
}
