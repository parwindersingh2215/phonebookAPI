using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PhoneBookAPI.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PhoneBookAPI.Common
{
    public static class CommonMethods
    {
       static string  Jwtkey = "qwienumdjsdnmlpywrdaewufqoujdgbsteovdsvkdywenfjkdsybasdashekfhatrtrysaaawe";

        public static string CreateToken(UserViewModel userViewModel)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("username",userViewModel.UserName)      

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwtkey));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        //private static string VerifyToken(string jwtToken)
        //{
        //    var tokenhandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes(Jwtkey);
        //    tokenhandler.ValidateToken(jwtToken, new TokenValidationParameters
        //    {

        //    }
        //}
    }
}
