namespace basic.ntier.architecture.auth.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Dapper;
    using Infrastructure;
    using Models;

    public class UserRolesRepository
    {
        private DbManager db;

        /// <summary>
        /// Constructor that takes a DbManager instance 
        /// </summary>
        /// <param name="database"></param>
        public UserRolesRepository(DbManager database)
        {
            db = database;
        }

        /// <summary>
        /// Returns a list of user's roles
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public List<string> FindByUserId(string userId)
        {
            return db.Connection.Query<string>("SELECT Role.Name FROM [AspNetRoles] r JOIN [AspNetUserRoles] ur on ur.RoleId = r.Id WHERE ur.UserId=@UserId", new { UserId = userId })
                .ToList();
        }

        /// <summary>
        /// Deletes all roles FROM a user in the UserRoles table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public void Delete(int userId)
        {
            db.Connection.Execute(@"DELETE FROM [AspNetUserRoles] WHERE UserId = @UserId", new { UserId = userId });
        }

        /// <summary>
        /// Inserts a new role for a user in the UserRoles table
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="roleId">The role's id</param>
        /// <returns></returns>
        public void Insert(IdentityUser user, string roleId)
        {
            db.Connection.Execute(@"INSERT INTO [AspNetUserRoles] (UserId, RoleId) VALUES (@UserId, @RoleId)",
                new { UserId = user.Id, RoleId = roleId });
        }
    }
}