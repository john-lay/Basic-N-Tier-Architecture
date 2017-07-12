//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using AspNet.Identity.Dapper;
//using basic.ntier.architecture.auth.Models;

//namespace basic.ntier.architecture.auth.Repository
//{
//    using System.Configuration;
//    using System.Data;
//    using System.Data.SqlClient;
//    using System.Threading.Tasks;
//    using AspNet.Identity.Dapper;
//    using Dapper;
//    using Microsoft.AspNet.Identity;
//    using Models;
//    using Stores;

//    public class AuthRepository
//    {
//        private UserManager<IdentityUser, string> userManager = null;

//        public AuthRepository()
//        {
//            var userStore = new UserStore<IdentityUser>();
//            userManager = new UserManager<IdentityUser, string>(userStore);
//        }

//        public async Task<IdentityResult> RegisterUser(UserModel userModel)
//        {
//            IdentityUser user = new IdentityUser
//            {
//                UserName = userModel.UserName
//            };

//            var result = await userManager.CreateAsync(user, userModel.Password);

//            return result;
//        }

//        //public async Task<IEnumerable<IdentityUser>> FindUser(UserModel userModel)
//        //{
//        //    string sql = "SELECT * FROM [AspNetUsers] WHERE [UserName] = '" + userModel.UserName + "' AND [PasswordHash] = '" + userModel.Password + "'";

//        //    using (var conn = this.connection)
//        //    {
//        //        return await conn.QueryAsync<IdentityUser>(sql);
//        //    }
//        //}
//    }
//}