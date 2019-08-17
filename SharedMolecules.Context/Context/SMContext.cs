using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedMolecules.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharedMolecules.EFCore.Context
{
    public class SMContext<TUser, TRole, TKey
           , TUserClaim, TUserRole, TUserLogin
           , TRoleClaim, TUserToken> : IdentityDbContext<TUser, TRole, TKey
           , TUserClaim, TUserRole, TUserLogin
           , TRoleClaim, TUserToken>
           where TUser : IdentityUser<TKey>
           where TRole : IdentityRole<TKey>
           where TKey : IEquatable<TKey>
           where TUserClaim : IdentityUserClaim<TKey>
           where TUserRole : IdentityUserRole<TKey>
           where TUserLogin : IdentityUserLogin<TKey>
           where TRoleClaim : IdentityRoleClaim<TKey>
           where TUserToken : IdentityUserToken<TKey>
    {
        private DbContextOptions options;

        public SMContext(DbContextOptions options)
        {
            this.options = options;
        }
        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Methods
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is ITrackable trackable)
                {
                    var now = DateTime.UtcNow;
                    var user = GetCurrentUser();
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.LastUpdatedAt = now;
                            trackable.LastUpdatedBy = user;
                            break;

                        case EntityState.Added:
                            trackable.CreatedAt = now;
                            trackable.CreatedBy = user;
                            trackable.LastUpdatedAt = now;
                            trackable.LastUpdatedBy = user;
                            break;
                    }
                }
            }
        }

        private string GetCurrentUser()
        {
            return "UserName"; // TODO implement your own logic

            // If you are using ASP.NET Core, you should look at this answer on StackOverflow
            // https://stackoverflow.com/a/48554738/2996339
        }
        #endregion
    }
}
