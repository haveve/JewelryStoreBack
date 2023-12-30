using CourseWorkDB;
using CourseWorkDB.Graphql.Schemas;
using CourseWorkDB.Repositories;
using FileUploadSample;
using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using GraphQL.Validation.Rules;
using Microsoft.AspNetCore.Authentication.Cookies;
using TimeTracker.Repositories;
using WebSocketGraphql.GraphQl.ValidationRules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUploadRepository,UploadRepository>();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<IProductRepository,ProductRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserProductRelation, UserProductRelation>();

builder.Services.AddSingleton<Schema, ShopSchema>(service =>
{
    return new ShopSchema(new SelfActivatingServiceProvider(service));
});

builder.Services.AddSingleton<Schema, IdentitySchema>(service =>
{
    return new IdentitySchema(new SelfActivatingServiceProvider(service));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


builder.Services.AddGraphQLUpload();

builder.Services.AddGraphQL(c =>
{
    c.AddSchema<ShopSchema>()
    .AddGraphTypes(typeof(ShopSchema).Assembly)
    .AddSchema<IdentitySchema>()
    .AddGraphTypes(typeof(IdentitySchema).Assembly)
                                      .AddAuthorization(setting =>
                                      {
                                          setting.AddPolicy("ProductManage", p => p.RequireClaim("ProductManage", "True"));
                                          setting.AddPolicy("UserManage", p => p.RequireClaim("UserManage", "True"));
                                          setting.AddPolicy("Authorized", p => p.RequireAuthenticatedUser());   
                                      })
    .AddErrorInfoProvider(opt => opt.ExposeExceptionDetails = true)
    .AddValidationRule<InputAndArgumentEmailValidationRule>()
    .AddValidationRule<InputAndArgumentNumberValidationRule>()
    .AddValidationRule<InputFieldsAndArgumentsOfCorrectLength>()
    .AddSystemTextJson();
});

var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseGraphQLUpload<ShopSchema>();

app.UseGraphQL<ShopSchema>("/graphql");
app.UseGraphQL<IdentitySchema>("/graphql-auth");


app.UseGraphQLAltair("/");

app.Run();
