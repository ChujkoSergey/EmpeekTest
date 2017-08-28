using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpeekTest.Model.Interfaces;
using EmpeekTest.Model.Models;

namespace EmpeekTest.Model.Contexts
{
    public class MainContext : IMainDbContext
    {
        #region Fields

        private readonly ItemsDbModel _context;
        private static IMainDbContext _instance;

        #endregion

        #region Constructors

        private MainContext()
        {
            _context = new ItemsDbModel();
            Items = new ItemsContext(_context);
            Type = new TypeContext(_context);
        }

        #endregion

        #region Properties

        public static IMainDbContext Instance => _instance ?? (_instance = new MainContext());

        public IDbContext<Items> Items { get; set; }
        public IDbContext<Models.Type> Type { get ; set; }

        #endregion

        #region Methods

        #endregion
    }
}
