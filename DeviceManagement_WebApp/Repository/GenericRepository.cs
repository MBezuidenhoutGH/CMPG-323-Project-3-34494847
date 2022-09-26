using DeviceManagement_WebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        //Check if parsed ID exists then return true if it exists or false if it does not exist
        public bool CheckID(Guid? id)
        {
            if (GetById(id) != null)
                return true;

            return false;
        }

        //Check if parsed class exists then return true if it exists or false if it does not exist
        public bool CheckClass(T t)
        {
            if (_context.Set<T>() != null)
                return true;

            return false;
        }

        //Using a combination of CheckID, CheckClass to return entire class set using GetByID
        public T CheckDetails(Guid? id)
        {
            if (CheckID(id))
            {
                var t = GetById(id);
                if (CheckClass(t))
                    return t;
            }
            return null;
        }

        //Update an existing record
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        //Save changes made to an existing record
        public void Save()
        {
            _context.SaveChanges();
        }   

        //Using a combination of Add() and Save() method to create a record
        public void Create(T t)
        {
            Add(t);
            Save();
        }

        //Using a combination of Update() and Save() method to edit a record
        public bool Edit(Guid id, T t)
        {
            try
            {
                Update(t);
                Save();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (CheckID(id))
                    throw;
                else 
                    return false;
            }
        }

        //Using a combination of Remove() and Save() method to remove a record
        public void DeleteConfirmed(T t)
        {
            Remove(t);
            Save();
        }
    }
}
