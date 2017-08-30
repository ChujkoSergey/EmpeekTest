namespace EmpeekTest.Model.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using EmpeekTest.Model.Interfaces;
    using EmpeekTest.Model.Models;
    using EmpeekTest.Model.Messages;

    public class ItemsContext : IDbContext<Items>
    {
        #region Fields

        private ItemsDbModel _context;

        #endregion

        #region Constructors

        public ItemsContext() => _context = new ItemsDbModel();

        public ItemsContext(ItemsDbModel context) => _context = context;

        #endregion

        #region Properties

        #endregion

        #region Methods

        public bool Delete(Expression<Func<Items, bool>> expression)
        {
            var temp = _context.Items.Where(expression).ToList();
            if(temp.Count == 0)
            {
                return false;
            }
            _context.Items.RemoveRange(temp);
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<Items> GetAll()
        {
            return _context.Items.ToList();
        }

        public IEnumerable<Items> GetBy(Expression<Func<Items, bool>> expression)
        {
            var temp = _context.Items.Where(expression).ToList();
            return (temp.Count != 0) ? temp : null;
        }

        public bool Insert(Items newItem)
        {
            _context.Items.Add(newItem);
            return _context.SaveChanges() != 0;
        }

        public bool Update(Items newItem, Expression<Func<Items, bool>> expression)
        {
            var temp = _context.Items.Where(expression).ToList();
            if(temp.Count == 0)
            {
                return false;
            }
            foreach(var item in temp)
            {
                item.Name = newItem.Name;
                item.TypeId = newItem.TypeId;
            }
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<ItemsInfoMessage> GetItemInfoPage(int page, int count)
        {
            var tempItems = _context.Items.ToList().Skip((page - 1) * count).Take(count);
            return from items in tempItems
                   join type in _context.Type on items.TypeId equals type.Id
                   select new ItemsInfoMessage() { Id = items.Id, Name = items.Name, Type = type.Name };
        }

        #endregion
    }
}
