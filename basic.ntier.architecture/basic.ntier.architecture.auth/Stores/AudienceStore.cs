namespace basic.ntier.architecture.auth.Stores
{
    using System;
    using System.Security.Cryptography;
    using basic.ntier.architecture.auth.Entities;
    using basic.ntier.architecture.auth.Infrastructure;
    using basic.ntier.architecture.auth.Repositories;
    using Microsoft.Owin.Security.DataHandler.Encoder;

    public class AudienceStore
    {
        private AudienceRepository audienceRepo;

        public AudienceStore()
        {
            audienceRepo = new AudienceRepository(new DbManager());
        }

        //public static ConcurrentDictionary<string, Audience> AudiencesList = new ConcurrentDictionary<string, Audience>();

        //static AudiencesStore()
        //{
        //    AudiencesList.TryAdd("099153c2625149bc8ecb3e85e03f0022",
        //                        new Audience
        //                        {
        //                            ClientId = "099153c2625149bc8ecb3e85e03f0022",
        //                            Base64Secret = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw",
        //                            Name = "ResourceServer.Api 1"
        //                        });
        //}

        public Audience AddAudience(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");

            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);

            Audience newAudience = new Audience { ClientId = clientId, Base64Secret = base64Secret, Name = name };
            audienceRepo.Insert(newAudience);
            //AudiencesList.TryAdd(clientId, newAudience);
            return newAudience;
        }

        public Audience FindAudience(string clientId)
        {
            var result = audienceRepo.GetAudienceById(clientId);

            return result;
            //Audience audience = null;
            //if (AudiencesList.TryGetValue(clientId, out audience))
            //{
            //    return audience;
            //}
            //return null;
        }
    }
}