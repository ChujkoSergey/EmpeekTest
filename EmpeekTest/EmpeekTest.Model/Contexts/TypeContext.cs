namespace EmpeekTest.Model.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EmpeekTest.Model.Interfaces;
    using System.Linq.Expressions;
    using EmpeekTest.Model.Messages; 

    public class TypeContext : IDbContext<Models.Type>
    {
        #region Fields

        private ItemsDbModel _context;

        #endregion

        #region Constructors

        public TypeContext() => _context = new ItemsDbModel();
        
        public TypeContext(ItemsDbModel context) => _context = context;

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool Delete(Expression<Func<Models.Type, bool>> expression)
        {
            var temp = _context.Type.Where(expression).ToList();
            if (temp.Count == 0)
            {
                return false;
            }
            _context.Type.RemoveRange(temp);
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<Models.Type> GetAll()
        {
            return _context.Type.ToList();
        }

        public IEnumerable<Models.Type> GetBy(Expression<Func<Models.Type, bool>> expression)
        {
            var temp = _context.Type.Where(expression).ToList();
            return (temp.Count != 0) ? temp : null;

        }

        public bool Insert(Models.Type newItem)
        {
            _context.Type.Add(newItem);
            return _context.SaveChanges() != 0;
        }

        public bool Update(Models.Type newItem, Expression<Func<Models.Type, bool>> expression)
        {
            var temp = _context.Type.Where(expression).ToList();
            if(temp.Count == 0)
            {
                return false;
            }
            foreach(var item in temp)
            {
                item.Name = newItem.Name;
            }
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<StatMessage> GetTypeStats()
        {
            var temp = _context.Items.GroupBy(x => x.TypeId).Select(value => new { Type = value.Key, Count = value.Count() });
            
        }

        #endregion
    }
}
