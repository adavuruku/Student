using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student.Data;
using Student.UnitOfWork.Service;

namespace Student.UnitOfWork.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity:class
    {
        protected readonly DataContext _context;

        public Repository(DataContext context){
            _context =  context;
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate){
            //how this work 
            //you can create a func delegate
            // then assign it to linq expression which Expression<TDelegate> -> expression is a generic system names in Linq query
            //e.g Func<Student, bool> isTeenAger = s => s.Age > 12 && s.Age < 20; -> delegate
            //delegate takes in input of Student Object and output of a boolean
            //assigning it to an expression
            //Expression<Func<Student, bool>> = s => s.Age > 12 && s.Age < 20;
            //once applied in a Ling DbContex -> Linq convert this to an appropriate query
            //https://www.tutorialsteacher.com/linq/linq-expression
            return _context.Set<TEntity>().Where(predicate);
        }

        public  void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
    }
}