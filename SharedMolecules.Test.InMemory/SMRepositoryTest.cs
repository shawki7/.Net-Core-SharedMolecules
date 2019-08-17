using Microsoft.EntityFrameworkCore;
using System;

namespace SharedMolecules.UnitTest.InMemory
{
    public abstract class SMRepositoryTest<TContext, TIRepository>
         where TContext : DbContext
         where TIRepository : class
    {
        #region Properties
        public TIRepository Repository { get; set; }
        public TContext Context { get; set; }
        #endregion

        #region Constructor
        public SMRepositoryTest()
        {
            Repository = InitializeRepository();
        }

        #endregion

        #region Methods
        public abstract TIRepository InitializeRepository();
        public abstract void FillData(TContext context);

        public TContext FillAndGetContext()
        {

            var options = new DbContextOptionsBuilder<TContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Context = (TContext)Activator.CreateInstance(typeof(TContext), options);


            Context.Database.EnsureDeleted();

            FillData(Context);
            Context.SaveChanges();

            return Context;
        }
        #endregion
    }
}
