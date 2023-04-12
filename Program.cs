using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.DependencyResolver;
using ServiceManagerApi;
using ServiceManagerApi.Data;
using ServiceManagerApi.Helpers;
using ServiceManagerApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var SMconnectionString = builder.Configuration.GetConnectionString("ServiceManagerConnection");
builder.Services.AddDbContext<ServiceManagerContext>(options =>
    options.UseSqlServer(SMconnectionString));


var EnPconnectionString = builder.Configuration.GetConnectionString("EnpConnectionString");

builder.Services.AddDbContext<EnpDBContext>(options =>  options.UseSqlServer(EnPconnectionString));
 
 
                                    //this will allow for patch request
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt =>  opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMappingProfiles).Assembly);
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("MyAllowedOrigins",
//        policy => 
//        {
//            policy.WithOrigins("http://localhost:3000") // note the port is included 
//                .AllowAnyHeader()
//                .AllowAnyMethod();
//        });
//});
builder.Services.AddCors();



var app = builder.Build();

//cors configuration for 
app.UseCors(
    options => {
        string frontendUrl = "http://localhost:3000";
        string serverUrl = "http://208.117.44.15/";
        options.WithOrigins(frontendUrl, serverUrl )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/SmWebApi/swagger/v1/swagger.json", "SipPay API V1");
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();