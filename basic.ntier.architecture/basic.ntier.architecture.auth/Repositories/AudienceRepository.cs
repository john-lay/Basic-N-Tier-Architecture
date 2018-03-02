namespace basic.ntier.architecture.auth.Repositories
{
    using System.Linq;
    using basic.ntier.architecture.auth.Entities;
    using basic.ntier.architecture.auth.Infrastructure;
    using Dapper;

    public class AudienceRepository
    {
        private DbManager db;

        public AudienceRepository(DbManager database)
        {
            db = database;
        }

        public Audience GetAudienceById(string clientId)
        {
            return db.Connection.Query<Audience>("SELECT * FROM [Audience] WHERE ClientId=@ClientId", new { ClientId = clientId })
                .FirstOrDefault();
        }

        public void Insert(Audience audience)
        {

            var id = db.Connection.ExecuteScalar<string>(@"INSERT INTO [Audience] (ClientId, Base64Secret, Name)
                        VALUES  (@ClientId, @Base64Secret, @Name)
                        SELECT Cast(SCOPE_IDENTITY() as int)",
                new
                {
                    ClientId = audience.ClientId,
                    Base64Secret = audience.Base64Secret,
                    Name = audience.Name
                });
        }

        private void Delete(string clientId)
        {
            db.Connection.Execute(@"DELETE FROM [Audience] WHERE ClientId = @ClientId", new { ClientId = clientId });
        }

        public void Delete(Audience audience)
        {
            Delete(audience.ClientId);
        }

        public void Update(Audience audience)
        {
            db.Connection
              .Execute(@"UPDATE [Audience] SET ClientId = @ClientId, Base64Secret = @Base64Secret, Name = @Name
                        WHERE ClientId = @ClientId",
                new
                {
                    ClientId = audience.ClientId,
                    Base64Secret = audience.Base64Secret,
                    Name = audience.Name
                }
           );
        }
    }
}