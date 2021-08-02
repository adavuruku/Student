using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Student.Services;

namespace Student.MyJWT
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
       // private readonly AppSettings _appSettings;
        
       //this gives us access to the appsettting.json and Configuration service in startup.cs
        private IConfiguration _configuration;

        private TestingService _testingService;
        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            
        }


        //1. checks if token actually Exist in the Authorization header

        public async Task Invoke(HttpContext context, TestingService testingService)
        {
            _testingService = testingService;
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //2. if theres token then validate and attach it to the context
            if (token != null)
                await attachUserToContext(context, token);

            //3. pass the controller to the next method  
            await _next(context);
        }

        private async Task attachUserToContext(HttpContext context,  string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer =  _configuration["JWT:Issuer"],   
                    ValidAudience = _configuration["JWT:Issuer"],
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                //4. attach user to context on successful jwt validation
                context.Items["Student"] = await _testingService.LoadAStudent(userId);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}