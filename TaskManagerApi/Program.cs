using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Services;
using TaskManagerApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
	.AddControllers()
	.AddNewtonsoftJson(options =>
	{
		options.SerializerSettings.ContractResolver = new PatchRequestContractResolver();
	});
// Self referencing loop detected fix
builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// builder.Services.AddControllersWithViews();
//DB
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

// Services
builder.Services.AddScoped<ITopicsService, TopicsService>();


var app = builder.Build();

//DB init and update
/*
using var serviceScope = app.Services.CreateScope();
var context = serviceScope.ServiceProvider.GetService<ApplicationContext>();
// auto migration
context?.Database.Migrate();
*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
