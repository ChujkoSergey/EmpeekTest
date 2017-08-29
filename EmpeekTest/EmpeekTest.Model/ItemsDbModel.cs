namespace EmpeekTest.Model
{

    using System.Data.Entity;
    using EmpeekTest.Model.Models;

    public class ItemsDbModel: DbContext
    {
        public ItemsDbModel():base("DbConnection")
        {

        }

        public DbSet<Items> Items { get; set; }
        public DbSet<Type> Type { get; set; }

        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            
            dbModelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
        }
    }
}
