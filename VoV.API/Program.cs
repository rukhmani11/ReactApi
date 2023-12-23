using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json.Serialization;
using VoV.API.Extensions;
using VoV.Data.Helpers;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VoV.Data.AutoMappers;
using VoV.Data.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VoV.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

builder.Services.AddMvc(config =>
{
    //Make Authorize all controller by default
    var policy = new AuthorizationPolicyBuilder()
          .RequireAuthenticatedUser()
          .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
})
    //.SetCompatibilityVersion(CompatibilityVersion.Latest)
    .ConfigureApiBehaviorOptions(options =>
    {
        //InvalidModelStateResponseFactory is a Func delegate  
        // and used to customize the error response.    
        //It is exposed as property of ApiBehaviorOptions class  
        // that is used to configure api behaviour.    
        options.InvalidModelStateResponseFactory = actionContext =>
        {
            //CustomErrorResponse is method that gets model validation errors     
            //using ActionContext creates customized response,  
            // and converts invalid model state dictionary    
            return ErrorHelper.CustomErrorResponse(actionContext);
        };
    })
    .AddJsonOptions(jsonOptions =>
    {
        jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
    }); //For default Case.. removing camelCasing

//code first DB conn string
builder.Services.AddDbContext<VoVDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString") ?? throw new InvalidOperationException("Connection string 'VoVDbContext' not found.")));

//Custom Context for SP etc
builder.Services.AddDbContext<VoVCustomDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")));

//Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => //Add Jwt Bearer
{
    options.SaveToken = false;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["ApplicationSettings:JwtValidIssuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["ApplicationSettings:JwtValidAudience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["ApplicationSettings:JwtSecret"]))
    };
});

//AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

#region Inject Interfaces            
builder.Services.AddHttpContextAccessor();

//Inject ApplicationSettings from appsetting.json
builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));
ServiceExtension.RegisterServices(builder.Services); //configure DI for application services

#endregion

//serilog
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
///////////////added - global cors, static files/////////////
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseStaticFiles();

app.UseAuthorization();
app.UseAuthentication();

/////////////////added these - exception handlers, serilog///////////////////////
app.UseGlobalExceptionHandler();
app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();
