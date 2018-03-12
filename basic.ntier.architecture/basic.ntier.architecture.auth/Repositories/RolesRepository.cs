namespace basic.ntier.architecture.auth.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dapper;
    using Infrastructure;
    using Models;

    /// <summary>
    /// Class that represents the Role table in the Database
    /// </summary>
    public class RolesRepository
    {
        private DbManager db;

        /// <summary>
        /// Constructor that takes a DbManager instance 
        /// </summary>
        /// <param name="database"></param>
        public RolesRepository(DbManager database)
        {
            db = database;
        }

        /// <summary>
        /// Deltes a role from the Roles table
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns></returns>
        public void Delete(string roleId)
        {
            db.Connection.Execute(@"DELETE FROM [AspNetRoles] WHERE Id = @Id", new { Id = roleId });
        }

        /// <summary>
        /// Inserts a new Role in the Roles table
        /// </summary>
        /// <param name="role">The role</param>
        /// <returns></returns>
        public void Insert(IdentityRole role)
        {
            db.Connection.Execute(@"INSERT INTO [AspNetRoles] (Name) VALUES (@Name)",
                new { Name = role.Name });
        }

        /// <summary>
        /// Returns a role name given the roleId
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns>Role name</returns>
        public string GetRoleName(string roleId)
        {
            return db.Connection.ExecuteScalar<string>("SELECT Name FROM [AspNetRoles] WHERE Id=@Id", new { Id = roleId });
        }

        /// <summary>
        /// Returns the role Id given a role name
        /// </summary>
        /// <param name="roleName">Role's name</param>
        /// <returns>Role's Id</returns>
        public string GetRoleId(string roleName)
        {
            return db.Connection.ExecuteScalar<string>("SELECT Id FROM [AspNetRoles] WHERE Name=@Name", new { Name = roleName });
        }

        /// <summary>
        /// Gets the IdentityRole given the role Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IdentityRole GetRoleById(string roleId)
        {
            var roleName = GetRoleName(roleId);
            IdentityRole role = null;

            if (roleName != null)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        /// <summary>
        /// Gets the IdentityRole given the role name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public IdentityRole GetRoleByName(string roleName)
        {
            var roleId = GetRoleId(roleName);
            IdentityRole role = null;

            if (string.IsNullOrEmpty(roleId))
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        public List<IdentityRole> GetRoles()
        {
            return db.Connection.Query<IdentityRole>("SELECT * FROM [AspNetRoles]")
                .ToList();
        }

        public void Update(IdentityRole role)
        {
            db.Connection
            .Execute(@"
                    UPDATE [AspNetRoles]
                    SET
                        Name = @Name
                    WHERE
                        Id = @Id",
                    new
                    {
                        Name = role.Name,
                        Id = role.Id
                    });
        }
    }
}