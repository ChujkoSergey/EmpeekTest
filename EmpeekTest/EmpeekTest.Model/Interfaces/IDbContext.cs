namespace EmpeekTest.Model.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IDbContext<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetBy(Expression<Func<T, bool>> expression);
        bool Insert(T newItem);
        bool Delete(Expression<Func<T, bool>> expression);
        bool Update(T newItem, Expression<Func<T, bool>> expression);
    }
}
