//namespace basic.ntier.architecture.auth.Providers
//{
//    using System;
//    using System.Threading.Tasks;
//    using basic.ntier.architecture.auth.Infrastructure;
//    using basic.ntier.architecture.auth.Models;
//    using basic.ntier.architecture.auth.Repositories;
//    using Microsoft.Owin.Security.Infrastructure;

//    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
//    {
//        private AuthRepository authRepo;

//        public SimpleRefreshTokenProvider()
//        {
//            authRepo = new AuthRepository(new DbManager());
//        }

//        public void Create(AuthenticationTokenCreateContext context)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task CreateAsync(AuthenticationTokenCreateContext context)
//        {
//            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

//            if (string.IsNullOrEmpty(clientid))
//            {
//                return;
//            }

//            var refreshTokenId = Guid.NewGuid().ToString("n");

//            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

//            var token = new RefreshToken()
//            {
//                Id = Helper.GetHash(refreshTokenId),
//                ClientId = clientid,
//                Subject = context.Ticket.Identity.Name,
//                IssuedUtc = DateTime.UtcNow,
//                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
//            };

//            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
//            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

//            token.ProtectedTicket = context.SerializeTicket();

//            var result = await authRepo.AddRefreshToken(token);

//            if (result)
//            {
//                context.SetToken(refreshTokenId);
//            }
//        }

//        public void Receive(AuthenticationTokenReceiveContext context)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
//        {
//            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
//            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

//            string hashedTokenId = Helper.GetHash(context.Token);

//            var refreshToken = await authRepo.FindRefreshToken(hashedTokenId);

//            if (refreshToken != null)
//            {
//                //Get protectedTicket from refreshToken class
//                context.DeserializeTicket(refreshToken.ProtectedTicket);
//                var result = await authRepo.RemoveRefreshToken(hashedTokenId);
//            }
//        }
//    }
//}