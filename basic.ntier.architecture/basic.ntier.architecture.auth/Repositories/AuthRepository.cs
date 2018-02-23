namespace basic.ntier.architecture.auth.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using basic.ntier.architecture.auth.Infrastructure;
    using basic.ntier.architecture.auth.Models;
    using Dapper;

    public class AuthRepository
    {
        private DbManager db;

        public AuthRepository(DbManager database)
        {
            db = database;
        }

        public Client FindClient(string clientId)
        {
            var client = db.Connection.Query<Client>("SELECT * FROM [Clients] WHERE Id=@Id", new { Id = clientId }).SingleOrDefault();

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            var existingToken = db.Connection.Query<RefreshToken>("SELECT * FROM [RefreshToken] WHERE Subject=@Subject AND ClientId = @ClientId", new { Subject = token.Subject, ClientId = token.ClientId }).SingleOrDefault();

            if (existingToken != null)
            {
                await RemoveRefreshToken(existingToken);
            }

            var result = db.Connection.Execute(@"INSERT INTO [RefreshToken] (Id, Subject, ClientId, IssuedUtc, ExpiresUtc, ProtectedTicket) VALUES (@Id, @Subject, @ClientId, @IssuedUtc, @ExpiresUtc, @ProtectedTicket)",
                new { Id = token.Id,
                    Subject = token.Subject,
                    ClientId = token.ClientId,
                    IssuedUtc = token.IssuedUtc,
                    ExpiresUtc = token.ExpiresUtc,
                    ProtectedTicket = token.ProtectedTicket });            

            return result > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = db.Connection.Query<RefreshToken>("SELECT * FROM [RefreshToken] WHERE Id=@Id", new { Id = refreshTokenId }).SingleOrDefault();

            if (refreshToken != null)
            {
                var result = db.Connection.Execute(@"DELETE FROM [RefreshToken] WHERE Id = @Id", new { Id = refreshTokenId });

                return result > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            var token = db.Connection.Query<RefreshToken>("SELECT * FROM [RefreshToken] WHERE Id=@Id", new { Id = refreshToken.Id }).SingleOrDefault();

            if (token != null)
            {
                var result = db.Connection.Execute(@"DELETE FROM [RefreshToken] WHERE Id = @Id", new { Id = refreshToken.Id });

                return result > 0;
            }

            return false;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = db.Connection.Query<RefreshToken>("SELECT * FROM [RefreshToken] WHERE Id=@Id", new { Id = refreshTokenId }).SingleOrDefault();

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            var refreshTokens = db.Connection.Query<RefreshToken>("SELECT * FROM [RefreshToken]").ToList();

            return refreshTokens;
        }
    }
}