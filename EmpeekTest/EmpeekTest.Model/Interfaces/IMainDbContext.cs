namespace EmpeekTest.Model.Interfaces
{
    using EmpeekTest.Model.Models;
    public interface IMainDbContext
    {
        IDbContext<Items> Items { get; set; }
        IDbContext<Type> Type { get; set; }
    }
}
