using GraphQL.Types;
using GraphQL;
using ToDoList.Factories;
using ToDoList.GraphQl.Mutations;
using ToDoList.GraphQl.Queries;
using ToDoList.GraphQl.Schemas;
using ToDoList.GraphQl.Types;
using ToDoList.Models.Contextes;
using ToDoList.Repositories;
using ToDoList.GraphQl.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseMiddleware<RepositoryMiddleware>();
app.UseGraphQLAltair("/graphql");
app.UseGraphQL<ISchema>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
