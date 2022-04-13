using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Services;
using TaskManagerApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var adminRole = new Role("admin");
var userRole = new Role("user");

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

// JwtBearer tocken
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // ���������, ����� �� �������������� �������� ��� ��������� ������
            ValidateIssuer = true,
            // ������, �������������� ��������
            ValidIssuer = JwtConfigurations.Issuer,
            // ����� �� �������������� ����������� ������
            ValidateAudience = true,
            // ��������� ����������� ������
            ValidAudience = JwtConfigurations.Audience,
            // ����� �� �������������� ����� �������������
            ValidateLifetime = true,
            // ��������� ����� ������������
            IssuerSigningKey = JwtConfigurations.GetSymmetricSecurityKey(),
            // ��������� ����� ������������
            ValidateIssuerSigningKey = true,

        };
    });


var app = builder.Build();

//DB init and update
/*
using var serviceScope = app.Services.CreateScope();
var context = serviceScope.ServiceProvider.GetService<ApplicationContext>();
// auto migration
context?.Database.Migrate();
*/

// Auth
app.UseAuthentication();
app.UseAuthorization();


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
