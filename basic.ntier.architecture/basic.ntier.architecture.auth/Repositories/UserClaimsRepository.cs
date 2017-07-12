namespace basic.ntier.architecture.auth.Repositories
{
    using System.Security.Claims;
    using Dapper;
    using Infrastructure;
    using Models;

    public class UserClaimsRepository
    {
        private DbManager db;

        /// <summary>
        /// Constructor that takes a DbManager instance 
        /// </summary>
        /// <param name="database"></param>
        public UserClaimsRepository(DbManager database)
        {
            db = database;
        }

        /// <summary>
        /// Returns a ClaimsIdentity instance given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public ClaimsIdentity FindByUserId(string userId)
        {
           ClaimsIdentity claims = new ClaimsIdentity();

           foreach (var c in db.Connection.Query("SELECT * FROM [AspNetUserClaims] WHERE User_Id=@UserId", new { UserId = userId }))
           {
               claims.AddClaim(new Claim(c.ClaimType, c.ClaimValue));
           }

           return claims;
        }

        /// <summary>
        /// Deletes all claims from a user given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public void Delete(int userId)
        {
            db.Connection.Execute(@"DELETE FROM [AspNetUserClaims] WHERE User_Id = @UserId", new { UserId = userId });
        }

        /// <summary>
        /// Inserts a new claim in UserClaims table
        /// </summary>
        /// <param name="userClaim">User's claim to be added</param>
        /// <param name="userId">User's id</param>
        /// <returns></returns>
        public void Insert(Claim userClaim, string userId)
        {
            db.Connection.Execute(@"INSERT INTO [AspNetUserClaims] (ClaimValue, ClaimType, User_Id) 
                VALUES (@Value, @Type, @UserId)", 
                    new { 
                        Value = userClaim.Value,
                        Type = userClaim.Type,
                        UserId = userId
                        });
        }

        /// <summary>
        /// Deletes a claim from a user 
        /// </summary>
        /// <param name="user">The user to have a claim deleted</param>
        /// <param name="claim">A claim to be deleted from user</param>
        /// <returns></returns>
        public void Delete(IdentityUser user, Claim claim)
        {
            db.Connection.Execute(@"DELETE FROM [AspNetUserClaims] 
            WHERE User_Id = @UserId AND ClaimValue = @Value AND ClaimType = @Type",
                new { 
                    UserId = user.Id,
                    Value = claim.Value,
                    Type = claim.Type 
                });
        }
    }
}