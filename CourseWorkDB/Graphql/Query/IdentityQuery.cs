using CourseWork_DB.Helpers;
using CourseWorkDB.Graphql.Mutation.User;
using CourseWorkDB.Graphql.Query.User;
using CourseWorkDB.Repositories;
using CourseWorkDB.ViewModel.User;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using TimeTracker.Services;

namespace CourseWorkDB.Graphql.Query
{
    public class IdentityQuery : ObjectGraphType
    {
        public IdentityQuery(IUserRepository userRepository, IConfiguration configuration)
        {

            Field<UserSelfDataGraphType>("login")
                 .Argument<NonNullGraphType<UserInputLoginGraphType>>("data")
                 .ResolveAsync(async context =>
                 {
                     var user = context.GetArgument<LoginUser>("data");
                     var fullUser = await userRepository.GetUser(user.TelephoneNumber, user.Password, configuration.GetIteration());

                     if(fullUser is null)
                     {
                         return null;
                     }

                     var claims = new List<Claim>
                     {
                        new Claim("UserId", fullUser.Id.ToString()),
                        new Claim("ProductManage", fullUser.Permissions.ProductManage.ToString()),
                        new Claim("UserManage", fullUser.Permissions.UserManage.ToString()),
                     };

                     var claimsIdentity = new ClaimsIdentity(
                         claims, CookieAuthenticationDefaults.AuthenticationScheme);

                     var httpContext = context.RequestServices!.GetService<IHttpContextAccessor>()!.HttpContext!;

                     await httpContext.SignInAsync(
                     CookieAuthenticationDefaults.AuthenticationScheme,
                     new ClaimsPrincipal(claimsIdentity));

                     return new UserSelfData { FullName = fullUser.FullName, TelephoneNumber = fullUser.TelephoneNumber, Id = fullUser.Id };
                 });

            Field<NonNullGraphType<StringGraphType>>("logout")
                 .ResolveAsync(async context =>
                 {
                     var httpContext = context.RequestServices!.GetService<IHttpContextAccessor>()!.HttpContext!;
                     await httpContext.SignOutAsync();
                     return "OK";
                });
        }
    }
}
