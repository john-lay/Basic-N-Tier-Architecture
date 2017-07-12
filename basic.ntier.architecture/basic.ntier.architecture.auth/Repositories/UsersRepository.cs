namespace basic.ntier.architecture.auth.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Dapper;
    using Infrastructure;
    using Models;

    public class UsersRepository<TUser>
        where TUser : IdentityUser
    {
        private DbManager db;

        /// <summary>
        /// Constructor that takes a DbManager instance 
        /// </summary>
        /// <param name="database"></param>
        public UsersRepository(DbManager database)
        {
            db = database;
        }

        /// <summary>
        /// Returns the user's name given a user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserName(int userId)
        {
            return db.Connection.ExecuteScalar<string>("SELECT Name FROM [AspNetUsers] WHERE Id=@UserId", new { UserId = userId });
        }

        /// <summary>
        /// Returns a user ID given a user name
        /// </summary>
        /// <param name="userName">The user's name</param>
        /// <returns></returns>
        public int GetmemberId(string userName)
        {
            return db.Connection.ExecuteScalar<int>("SELECT Id FROM [AspNetUsers] WHERE UserName=@UserName", new { UserName = userName });
        }

        /// <summary>
        /// Returns an TUser given the user's id
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public TUser GetUserById(string userId)
        {
            return db.Connection.Query<TUser>("SELECT * FROM [AspNetUsers] WHERE Id=@UserId", new { UserId = userId })
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns a list of TUser instances given a user name
        /// </summary>
        /// <param name="userName">user's name</param>
        /// <returns></returns>
        public List<TUser> GetUserByName(string userName)
        {
            return db.Connection.Query<TUser>("SELECT * FROM [AspNetUsers] WHERE UserName=@UserName", new { UserName = userName })
                .ToList();
        }

        public List<TUser> GetUserByEmail(string email)
        {
            return null;
        }

        /// <summary>
        /// Return the user's password hash
        /// </summary>
        /// <param name="memberId">The user's id</param>
        /// <returns></returns>
        public string GetPasswordHash(string memberId)
        {
            return db.Connection.ExecuteScalar<string>("SELECT PasswordHash FROM [AspNetUsers] WHERE Id = @UserId", new { UserId = memberId });
        }

        /// <summary>
        /// Sets the user's password hash
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public void SetPasswordHash(int userId, string passwordHash)
        {
            db.Connection.Execute(@"
                    UPDATE
                        [AspNetUsers]
                    SET
                        PasswordHash = @PwdHash
                    WHERE
                        Id = @Id", new { PwdHash = passwordHash, Id = userId });
        }

        /// <summary>
        /// Returns the user's security stamp
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetSecurityStamp(int userId)
        {
            return db.Connection.ExecuteScalar<string>("SELECT SecurityStamp FROM [AspNetUsers] WHERE Id = @UserId", new { UserId = userId });
        }

        /// <summary>
        /// Inserts a new user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void Insert(TUser user)
        {
//            var id = db.Connection.ExecuteScalar<string>(@"INSERT INTO [AspNetUsers]
//                                    (UserName,  PasswordHash, SecurityStamp,Email,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed, AccessFailedCount,LockoutEnabled,LockoutEndDateUtc,TwoFactorEnabled)
//                            VALUES  (@name, @pwdHash, @SecStamp,@email,@emailconfirmed,@phonenumber,@phonenumberconfirmed,@accesscount,@lockoutenabled,@lockoutenddate,@twofactorenabled)
//                            SELECT Cast(SCOPE_IDENTITY() as int)",
//                             new {  
//                                    name = user.UserName,
//                                    pwdHash = user.PasswordHash,
//                                    SecStamp = user.SecurityStamp,
//                                    email = user.Email,
//                                    emailconfirmed = user.EmailConfirmed,
//                                    phonenumber = user.PhoneNumber,
//                                    phonenumberconfirmed = user.PhoneNumberConfirmed,
//                                    accesscount = user.AccessFailedCount,
//                                    lockoutenabled = user.LockoutEnabled,
//                                    lockoutenddate = user.LockoutEndDateUtc,
//                                    twofactorenabled = user.TwoFactorEnabled
//                             });
            var id = db.Connection.ExecuteScalar<string>(@"INSERT INTO [AspNetUsers] (Id, UserName, PasswordHash, SecurityStamp)
                            VALUES  (@Id, @UserName, @PasswordHash, @SecurityStamp)
                            SELECT Cast(SCOPE_IDENTITY() as int)",
                             new
                             {
                                 Id = user.Id,
                                 UserName = user.UserName,
                                 PasswordHash = user.PasswordHash,
                                 SecurityStamp = user.SecurityStamp
                             });

            // we need to set the id to the returned identity generated FROM the db
            user.Id = id;
        }

        /// <summary>
        /// Deletes a user FROM the Users table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        private void Delete(string userId)
        {
            db.Connection.Execute(@"DELETE FROM [AspNetUsers] WHERE Id = @UserId", new { UserId = userId });
        }

        /// <summary>
        /// Deletes a user FROM the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void Delete(TUser user)
        {
            Delete(user.Id);
        }

        /// <summary>
        /// Updates a user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void Update(TUser user)
        {
//            db.Connection
//              .Execute(@"
//                            UPDATE [AspNetUsers] set UserName = @userName, PasswordHash = @pswHash, SecurityStamp = @secStamp, 
//                Email=@email, EmailConfirmed=@emailconfirmed, PhoneNumber=@phonenumber, PhoneNumberConfirmed=@phonenumberconfirmed,
//                AccessFailedCount=@accesscount, LockoutEnabled=@lockoutenabled, LockoutEndDateUtc=@lockoutenddate, TwoFactorEnabled=@twofactorenabled  
//                WHERE Id = @userId",
//                new
//                {
//                    userName = user.UserName,
//                    pswHash = user.PasswordHash,
//                    secStamp = user.SecurityStamp,
//                    memberId = user.Id,
//                    email = user.Email,
//                    emailconfirmed = user.EmailConfirmed,
//                    phonenumber = user.PhoneNumber,
//                    phonenumberconfirmed = user.PhoneNumberConfirmed,
//                    accesscount = user.AccessFailedCount,
//                    lockoutenabled = user.LockoutEnabled,
//                    lockoutenddate = user.LockoutEndDateUtc,
//                    twofactorenabled = user.TwoFactorEnabled
//                }
//           );
            db.Connection
              .Execute(@"
                            UPDATE [AspNetUsers] SET UserName = @userName, PasswordHash = @pswHash, SecurityStamp = @secStamp
                WHERE Id = @Id",
                new
                {
                    userName = user.UserName,
                    pswHash = user.PasswordHash,
                    secStamp = user.SecurityStamp,
                    Id = user.Id
                }
           );
        }
    }
}