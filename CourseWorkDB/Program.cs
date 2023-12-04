using CourseWorkDB.Graphql.Schemas;
using CourseWorkDB.Repositories;
using FileUploadSample;
using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using TimeTracker.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUploadRepository,UploadRepository>();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<IProductRepository,ProductRepository>();

builder.Services.AddSingleton<Schema, ShopSchema>(service =>
{
    return new ShopSchema(new SelfActivatingServiceProvider(service));
});

builder.Services.AddGraphQLUpload();

builder.Services.AddGraphQL(c =>
{
    c.AddSchema<ShopSchema>()
    .AddGraphTypes(typeof(ShopSchema).Assembly)
    .AddErrorInfoProvider(opt => opt.ExposeExceptionDetails = true)
    .AddSystemTextJson();
});

var app = builder.Build();

app.UseStaticFiles();

app.UseGraphQLUpload<ShopSchema>();

app.UseGraphQL<ShopSchema>();

app.UseGraphQLAltair("/");

app.Run();
