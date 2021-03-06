﻿namespace basic.ntier.architecture.auth.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Infrastructure;
    using Microsoft.AspNet.Identity;
    using Models;
    using Repositories;

    /// <summary>
    /// Class that implements the key ASP.NET Identity user store iterfaces
    /// </summary>
    public class UserStore<TUser> : IUserLoginStore<TUser, string>,
        IUserClaimStore<TUser, string>,
        IUserRoleStore<TUser, string>,
        IUserPasswordStore<TUser, string>,
        //IUserSecurityStampStore<TUser, string>,
        //IQueryableUserStore<TUser, string>,
        //IUserEmailStore<TUser, string>,
        //IUserPhoneNumberStore<TUser, string>,
        //IUserTwoFactorStore<TUser, string>,
        //IUserLockoutStore<TUser, string>,
        IUserStore<TUser, string>
        where TUser : IdentityUser
    {
        private UsersRepository<TUser> usersRepo;
        private RolesRepository rolesRepo;
        private UserRolesRepository userRolesRepo;
        private UserClaimsRepository userClaimsRepo;
        private UserLoginsRepository userLoginsRepo;
        public DbManager Database { get; private set; }

        public IQueryable<TUser> Users
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        /// <summary>
        /// Default constructor that initializes a new database
        /// instance using the Default Connection string
        /// </summary>
        public UserStore()
        {
            Database = new DbManager();
            usersRepo = new UsersRepository<TUser>(Database);
            rolesRepo = new RolesRepository(Database);
            userRolesRepo = new UserRolesRepository(Database);
            userClaimsRepo = new UserClaimsRepository(Database);
            userLoginsRepo = new UserLoginsRepository(Database);
        }

        /// <summary>
        /// Insert a new TUser in the UserTable
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task CreateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            usersRepo.Insert(user);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns an TUser instance based on a userId query 
        /// </summary>
        /// <param name="userId">The user's Id</param>
        /// <returns></returns>
        public Task<TUser> FindByIdAsync(string userId)
        {
            //if (string.IsNullOrEmpty(userId))
            //{
            //    throw new ArgumentException("Null or empty argument: userId");
            //}

            TUser result = usersRepo.GetUserById(userId) as TUser;
            if (result != null)
            {
                return Task.FromResult<TUser>(result);
            }

            return Task.FromResult<TUser>(null);
        }

        /// <summary>
        /// Returns an TUser instance based on a userName query 
        /// </summary>
        /// <param name="userName">The user's name</param>
        /// <returns></returns>
        public Task<TUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Null or empty argument: userName");
            }

            var result = usersRepo.GetUserByName(userName);

            if (result != null && result.Count == 1)
            {
                return Task.FromResult<TUser>(result[0]);
            }

            return Task.FromResult<TUser>(null);
        }

        /// <summary>
        /// Updates the UsersTable with the TUser instance values
        /// </summary>
        /// <param name="user">TUser to be updated</param>
        /// <returns></returns>
        public Task UpdateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            usersRepo.Update(user);

            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            if (Database != null)
            {
                Database.Dispose();
                Database = null;
            }
        }

        /// <summary>
        /// Inserts a claim to the UserClaimsTable for the given user
        /// </summary>
        /// <param name="user">User to have claim added</param>
        /// <param name="claim">Claim to be added</param>
        /// <returns></returns>
        public Task AddClaimAsync(TUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("user");
            }

            userClaimsRepo.Insert(claim, user.Id);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns all claims for a given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            ClaimsIdentity identity = userClaimsRepo.FindByUserId(user.Id);

            return Task.FromResult<IList<Claim>>(identity.Claims.ToList());
        }

        /// <summary>
        /// Removes a claim froma user
        /// </summary>
        /// <param name="user">User to have claim removed</param>
        /// <param name="claim">Claim to be removed</param>
        /// <returns></returns>
        public Task RemoveClaimAsync(TUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            userClaimsRepo.Delete(user, claim);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Inserts a Login in the UserLoginsTable for a given User
        /// </summary>
        /// <param name="user">User to have login added</param>
        /// <param name="login">Login to be added</param>
        /// <returns></returns>
        public Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            userLoginsRepo.Insert(user, login);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns an TUser based on the Login info
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Task<TUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            var userId = userLoginsRepo.FindUserIdByLogin(login);
            if (!string.IsNullOrEmpty(userId))
            {
                TUser user = usersRepo.GetUserById(userId) as TUser;
                if (user != null)
                {
                    return Task.FromResult<TUser>(user);
                }
            }

            return Task.FromResult<TUser>(null);
        }

        /// <summary>
        /// Returns list of UserLoginInfo for a given TUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            List<UserLoginInfo> userLogins = new List<UserLoginInfo>();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            List<UserLoginInfo> logins = userLoginsRepo.FindByUserId(user.Id);
            if (logins != null)
            {
                return Task.FromResult<IList<UserLoginInfo>>(logins);
            }

            return Task.FromResult<IList<UserLoginInfo>>(null);
        }

        /// <summary>
        /// Deletes a login from UserLoginsTable for a given TUser
        /// </summary>
        /// <param name="user">User to have login removed</param>
        /// <param name="login">Login to be removed</param>
        /// <returns></returns>
        public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (login == null)
            {
                throw new ArgumentNullException("login");
            }

            userLoginsRepo.Delete(user, login);

            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Inserts a entry in the UserRoles table
        /// </summary>
        /// <param name="user">User to have role added</param>
        /// <param name="roleName">Name of the role to be added to user</param>
        /// <returns></returns>
        public Task AddToRoleAsync(TUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Argument cannot be null or empty: roleName.");
            }

            var roleId = rolesRepo.GetRoleId(roleName);
            if (!string.IsNullOrEmpty(roleId ))
            {
                userRolesRepo.Insert(user, roleId);
            }
            //if (!string.IsNullOrEmpty(roleId))
            //{
            //    userRolesRepo.Insert(user, roleId);
            //}

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns the roles for a given TUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            List<string> roles = userRolesRepo.FindByUserId(user.Id);
            {
                if (roles != null)
                {
                    return Task.FromResult<IList<string>>(roles);
                }
            }

            return Task.FromResult<IList<string>>(null);
        }

        /// <summary>
        /// Verifies if a user is in a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<bool> IsInRoleAsync(TUser user, string role)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentNullException("role");
            }

            List<string> roles = userRolesRepo.FindByUserId(user.Id);
            {
                if (roles != null && roles.Contains(role))
                {
                    return Task.FromResult<bool>(true);
                }
            }

            return Task.FromResult<bool>(false);
        }

        /// <summary>
        /// Removes a user from a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task RemoveFromRoleAsync(TUser user, string role)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task DeleteAsync(TUser user)
        {
            if (user != null)
            {
                usersRepo.Delete(user);
            }

            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Returns the PasswordHash for a given TUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetPasswordHashAsync(TUser user)
        {
            string passwordHash = usersRepo.GetPasswordHash(user.Id);

            return Task.FromResult<string>(passwordHash);
        }

        /// <summary>
        /// Verifies if user has password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> HasPasswordAsync(TUser user)
        {
            var hasPassword = !string.IsNullOrEmpty(usersRepo.GetPasswordHash(user.Id));

            return Task.FromResult<bool>(Boolean.Parse(hasPassword.ToString()));
        }

        /// <summary>
        /// Sets the password hash for a given TUser
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;

            return Task.FromResult<Object>(null);
        }

        /// <summary>
        ///  Set security stamp
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stamp"></param>
        /// <returns></returns>
        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            user.SecurityStamp = stamp;

            return Task.FromResult(0);

        }

        /// <summary>
        /// Get security stamp
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetSecurityStampAsync(TUser user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        /// <summary>
        /// Set email on user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        //public Task SetEmailAsync(TUser user, string email)
        //{
        //    user.Email = email;
        //    usersRepo.Update(user);

        //    return Task.FromResult(0);

        //}

        /// <summary>
        /// Get email from user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //public Task<string> GetEmailAsync(TUser user)
        //{
        //    return Task.FromResult(user.Email);
        //}

        /// <summary>
        /// Get if user email is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //public Task<bool> GetEmailConfirmedAsync(TUser user)
        //{
        //    return Task.FromResult(user.EmailConfirmed);
        //}

        /// <summary>
        /// Set when user email is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        //public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        //{
        //    user.EmailConfirmed = confirmed;
        //    usersRepo.Update(user);

        //    return Task.FromResult(0);
        //}

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        //public Task<TUser> FindByEmailAsync(string email)
        //{
        //    if (String.IsNullOrEmpty(email))
        //    {
        //        throw new ArgumentNullException("email");
        //    }

        //    TUser result = usersRepo.GetUserByEmail(email) as TUser;
        //    if (result != null)
        //    {
        //        return Task.FromResult<TUser>(result);
        //    }

        //    return Task.FromResult<TUser>(null);
        //}

        /// <summary>
        /// Set user phone number
        /// </summary>
        /// <param name="user"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        //public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        //{
        //    user.PhoneNumber = phoneNumber;
        //    usersRepo.Update(user);

        //    return Task.FromResult(0);
        //}

        /// <summary>
        /// Get user phone number
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //public Task<string> GetPhoneNumberAsync(TUser user)
        //{
        //    return Task.FromResult(user.PhoneNumber);
        //}

        /// <summary>
        /// Get if user phone number is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        //{
        //    return Task.FromResult(user.PhoneNumberConfirmed);
        //}

        /// <summary>
        /// Set phone number if confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        //public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        //{
        //    user.PhoneNumberConfirmed = confirmed;
        //    usersRepo.Update(user);

        //    return Task.FromResult(0);
        //}

        /// <summary>
        /// Set two factor authentication is enabled on the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        //public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        //{
        //    user.TwoFactorEnabled = enabled;
        //    usersRepo.Update(user);

        //    return Task.FromResult(0);
        //}

        /// <summary>
        /// Get if two factor authentication is enabled on the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        //{
        //    return Task.FromResult(user.TwoFactorEnabled);
        //}

        /// <summary>
        /// Get user lock out end date
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        //{
        //    return
        //        Task.FromResult(user.LockoutEndDateUtc.HasValue
        //            ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
        //            : new DateTimeOffset());
        //}

        /// <summary>
        /// Set user lockout end date
        /// </summary>
        /// <param name="user"></param>
        /// <param name="lockoutEnd"></param>
        /// <returns></returns>
        //public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        //{
        //    user.LockoutEndDateUtc = lockoutEnd.UtcDateTime;
        //    usersRepo.Update(user);

        //    return Task.FromResult(0);
        //}

        /// <summary>
        /// Increment failed access count
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //public Task<int> IncrementAccessFailedCountAsync(TUser user)
        //{
        //    user.AccessFailedCount++;
        //    usersRepo.Update(user);

        //    return Task.FromResult(user.AccessFailedCount);
        //}

        /// <summary>
        /// Reset failed access count
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //public Task ResetAccessFailedCountAsync(TUser user)
        //{
        //    user.AccessFailedCount = 0;
        //    usersRepo.Update(user);

        //    return Task.FromResult(0);
        //}

        /// <summary>
        /// Get failed access count
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //public Task<int> GetAccessFailedCountAsync(TUser user)
        //{
        //    return Task.FromResult(user.AccessFailedCount);
        //}

        /// <summary>
        /// Get if lockout is enabled for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //public Task<bool> GetLockoutEnabledAsync(TUser user)
        //{
        //    return Task.FromResult(user.LockoutEnabled);
        //}

        /// <summary>
        /// Set lockout enabled for user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        //public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        //{
        //    user.LockoutEnabled = enabled;
        //    usersRepo.Update(user);

        //    return Task.FromResult(0);
        //}
    }
}