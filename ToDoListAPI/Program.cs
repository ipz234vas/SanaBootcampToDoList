using GraphQL;
using GraphQL.Types;
using ToDoList.Factories;
using ToDoList.Models.Contextes;
using ToDoList.Repositories;
using ToDoListAPI.Middlewares;
using ToDoListAPI.Mutations;
using ToDoListAPI.Queries;
using ToDoListAPI.Schemas;
using ToDoListAPI.Types;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<ToDoListDBContext>();
builder.Services.AddSingleton<ToDoListXMLContext>();

builder.Services.AddSingleton<DBRepository>();
builder.Services.AddSingleton<XMLRepository>();

builder.Services.AddSingleton<RepositoryFactory>();

builder.Services.AddTransient<CategoryType>();
builder.Services.AddTransient<TaskType>();
builder.Services.AddTransient<InputTaskType>();

builder.Services.AddTransient<MainQuery>();

builder.Services.AddTransient<MainMutation>();

builder.Services.AddTransient<ISchema, MainSchema>();
builder.Services.AddGraphQL(options =>
{
	options.AddAutoSchema<ISchema>().AddSystemTextJson();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<RepositoryMiddleware>();

app.UseGraphQLAltair("/graphql");

app.UseGraphQL<ISchema>();

app.UseHttpsRedirection();

app.Run();
