namespace basic.ntier.architecture.auth.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Dapper;
    using Infrastructure;
    using Microsoft.AspNet.Identity;
    using Models;

    public class UserLoginsRepository
    {
        private DbManager db;

        /// <summary>
        /// Constructor that takes a DbManager instance 
        /// </summary>
        /// <param name="database"></param>
        public UserLoginsRepository(DbManager database)
        {
            db = database;
        }

        /// <summary>
        /// Deletes a login from a user in the UserLogins table
        /// </summary>
        /// <param name="user">User to have login deleted</param>
        /// <param name="login">Login to be deleted from user</param>
        /// <returns></returns>
        public void Delete(IdentityUser user, UserLoginInfo login)
        {
            db.Connection.Execute(@"DELETE FROM [AspNetUserLogins] 
                    WHERE UserId = @UserId 
                    AND LoginProvider = @LoginProvider 
                    AND ProviderKey = @ProviderKey",
                new 
                {   
                    UserId = user.Id,
                    LoginProvider = login.LoginProvider,
                    ProviderKey = login.ProviderKey
                });
        }

        /// <summary>
        /// Deletes all Logins from a user in the UserLogins table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public void Delete(int userId)
        {
            db.Connection.Execute(@"DELETE FROM [AspNetUserLogins] 
                    WHERE UserId = @UserId", new { UserId = userId });
        }

        /// <summary>
        /// Inserts a new login in the UserLogins table
        /// </summary>
        /// <param name="user">User to have new login added</param>
        /// <param name="login">Login to be added</param>
        /// <returns></returns>
        public void Insert(IdentityUser user, UserLoginInfo login)
        {
            db.Connection.Execute(@"INSERT INTO [AspNetUserLogins] 
                (LoginProvider, ProviderKey, UserId) 
                VALUES (@LoginProvider, @ProviderKey, @UserId)", 
                    new
                    {
                        LoginProvider = login.LoginProvider,
                        ProviderKey = login.ProviderKey,
                        UserId = user.Id
                    });
        }

        /// <summary>
        /// Return a userId given a user's login
        /// </summary>
        /// <param name="userLogin">The user's login info</param>
        /// <returns></returns>
        public string FindUserIdByLogin(UserLoginInfo userLogin)
        {
            return db.Connection.ExecuteScalar<string>(@"SELECT User_Id FROM [AspNetUserLogins] 
                WHERE LoginProvider = @LoginProvider AND ProviderKey = @ProviderKey",
                        new 
                        {   
                            LoginProvider = userLogin.LoginProvider,
                            ProviderKey = userLogin.ProviderKey
                        });
        }

        /// <summary>
        /// Returns a list of user's logins
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public List<UserLoginInfo> FindByUserId(string userId)
        {
            return db.Connection.Query<UserLoginInfo>("SELECT * FROM [AspNetUserLogins] WHERE User_Id = @UserId", new { UserId = userId })
                .ToList();
        }
    }
}