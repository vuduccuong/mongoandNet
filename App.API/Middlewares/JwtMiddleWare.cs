using App.Domain.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.API.Middlewares
{
    public class JwtMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        public JwtMiddleWare(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                await AttackAcc(context, token, userService);
            }

            await _next.Invoke(context);
        }

        private async Task AttackAcc(HttpContext context, string token, IUserService userService)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new();

                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //Set ClockKew = 0 để thông báo token đã hết hạn
                    //ClockSkew = TimeSpan.Zero
                }, out SecurityToken validateToken);

                var jwtToken = (JwtSecurityToken)validateToken;
                var userEmailInToken = jwtToken.Claims.FirstOrDefault(jwt => jwt.Type == JwtRegisteredClaimNames.Sub).Value;
                context.Items["Email"] = userEmailInToken;
            }
            catch
            {
                //do nothing
            }
        }
    }
}
