namespace basic.ntier.architecture.auth.Repositories
{
    using System.Linq;
    using basic.ntier.architecture.auth.Infrastructure;
    using basic.ntier.architecture.auth.Models;
    using Dapper;

    public class ClientRepository
    {
        private DbManager db;

        public ClientRepository(DbManager database)
        {
            db = database;
        }

        public Client GetClientById(string clientId)
        {
            return db.Connection.Query<Client>("SELECT * FROM [Client] WHERE Id=@ClientId", new { ClientId = clientId })
                .FirstOrDefault();
        }

        public void Insert(Client client)
        {

            var id = db.Connection.ExecuteScalar<string>(@"INSERT INTO [Client] (Id, Secret, Name, ApplicationType, Active, RefreshTokenLifeTime, AllowedOrigin)
                VALUES  (@Id, @Secret, @Name, @ApplicationType, @Active, @RefreshTokenLifeTime, @AllowedOrigin)
                SELECT Cast(SCOPE_IDENTITY() as int)",
                new
                {
                    Id = client.Id,
                    Secret = client.Secret,
                    Name = client.Name,
                    ApplicationType = client.ApplicationType,
                    Active = client.Active,
                    RefreshTokenLifeTime = client.RefreshTokenLifeTime,
                    AllowedOrigin = client.AllowedOrigin
                });
        }

        private void Delete(string clientId)
        {
            db.Connection.Execute(@"DELETE FROM [Client] WHERE Id = @Client", new { Client = clientId });
        }

        public void Delete(Client client)
        {
            Delete(client.Id);
        }

        public void Update(Client client)
        {
            db.Connection
              .Execute(@"UPDATE [Client] SET Id = @Id, Secret = @Secret, Name = @Name, ApplicationType = @ApplicationType, Active = @Active, RefreshTokenLifeTime = @RefreshTokenLifeTime, AllowedOrigin = @AllowedOrigin
                WHERE Id = @Id",
                new
                {
                    Id = client.Id,
                    Secret = client.Secret,
                    Name = client.Name,
                    ApplicationType = client.ApplicationType,
                    Active = client.Active,
                    RefreshTokenLifeTime = client.RefreshTokenLifeTime,
                    AllowedOrigin = client.AllowedOrigin
                }
           );
        }
    }
}