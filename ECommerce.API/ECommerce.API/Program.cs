using Autofac;
using Autofac.Extensions.DependencyInjection;
using ECommerce.API.Filters;
using ECommerce.API.Helpers;
using ECommerce.API.Middlewares;
using ECommerce.API.Modules;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Concrete;
using ECommerce.BusinessLayer.Exceptions;
using ECommerce.BusinessLayer.Validations;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.Concrete;
using ECommerce.DataAccessLayer.EntityFrameWork;
using ECommerce.DataAccessLayer.Repositories;
using ECommerce.DataAccessLayer.UnitOfWork;
using ECommerce.EntityLayer.Concrete;
using ECommerce.SharedLibrary.Configuration;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);











builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));//<- DI container

var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();

builder.Services.Configure<List<Client>>(builder.Configuration.GetSection("Clients"));//<- DI container

builder.Services.AddDbContext<ECommerceContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(ECommerceContext)).GetName().Name);
    });
});

builder.Services.AddAuthentication(Options =>
{
    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;    
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, Options =>
{
    Options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

        ValidateIssuerSigningKey =true,
        ValidateAudience = true,
        ValidateIssuer=true,
        ValidateLifetime=true,
        ClockSkew=TimeSpan.Zero,
    };
});

// Add services to the container.
builder.Services.AddControllers();
//builder.Services.AddControllers(options => { options.Filters.Add(new ValidateFilterAttribute()); }).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//builder.Services.AddMemoryCache();

//builder.Services.AddScoped(typeof(NotFoundFilter<>));


builder.Services.AddAutoMapper(typeof(Program));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder=>containerBuilder.RegisterModule(new RepoServiceModule()));

builder.Services.AddIdentity<User, IdentityRole>(Opt=>{
    Opt.User.RequireUniqueEmail = true;
    Opt.Password.RequireNonAlphanumeric = false;

}).AddEntityFrameworkStores<ECommerceContext>().AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
