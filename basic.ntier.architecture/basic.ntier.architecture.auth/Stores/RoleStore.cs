namespace basic.ntier.architecture.auth.Stores
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure;
    using Microsoft.AspNet.Identity;
    using Models;
    using Repositories;

    /// <summary>
    /// Class that implements the key ASP.NET Identity role store iterfaces
    /// </summary>
    public class RoleStore<TRole> : IQueryableRoleStore<TRole, int>
        where TRole : IdentityRole
    {
        private RolesRepository rolesRepo;


        /// <summary>
        /// Default constructor that initializes a new database
        /// instance using the Default Connection string
        /// </summary>
        public RoleStore()
        {
            new RoleStore<TRole>(new DbManager());
        }

        /// <summary>
        /// Constructor that takes a dbmanager as argument 
        /// </summary>
        /// <param name="database"></param>
        public RoleStore(DbManager database)
        {
            Database = database;
            rolesRepo = new RolesRepository(database);
        }

        public DbManager Database { get; private set; }

        public IQueryable<TRole> Roles
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Task CreateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            rolesRepo.Insert(role);

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("user");
            }

            rolesRepo.Delete(role.Id);

            return Task.FromResult<Object>(null);
        }

        public Task<TRole> FindByIdAsync(int roleId)
        {
            TRole result = rolesRepo.GetRoleById(roleId.ToString()) as TRole;

            return Task.FromResult<TRole>(result);
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            TRole result = rolesRepo.GetRoleByName(roleName) as TRole;

            return Task.FromResult<TRole>(result);
        }

        public Task UpdateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("user");
            }

            rolesRepo.Update(role);

            return Task.FromResult<Object>(null);
        }

        public void Dispose()
        {
            if (Database != null)
            {
                Database.Dispose();
                Database = null;
            }
        }
    }
}