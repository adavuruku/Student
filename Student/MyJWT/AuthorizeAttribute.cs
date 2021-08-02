using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Student.Models;

namespace Student.MyJWT
{
    
    //https://docs.microsoft.com/en-us/dotnet/api/system.attributetargets?view=net-5.0
    //https://docs.microsoft.com/en-us/dotnet/api/system.attribute?view=net-5.0
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public AuthorizeAttribute(string permissions, string[] allPermission)
        {
            _permissions = permissions;
            _allPermission = allPermission;
        }

        public string _permissions { get; set; }
        public string[] _allPermission { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Console.WriteLine("Na ThE Permission = " + _permissions);
            foreach(string et in _allPermission)
            {
                Console.WriteLine("Na ThE Permission = " + et);
            }
            var user = (Models.Student)context.HttpContext.Items["Student"];
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            
           
        }
    }
}