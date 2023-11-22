using SendingEmail.Models;
using SendingEmail.Services;
using static Org.BouncyCastle.Math.EC.ECCurve;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();

// Now you can access the email settings using the emailSettings object
var emailHost = emailSettings.EmailHost;
var emailUserName = emailSettings.EmailUserName;
var emailPassword = emailSettings.EmailPassword;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<EmailService>();

var app = builder.Build();

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
