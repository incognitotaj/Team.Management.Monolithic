using AutoWrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using Team.Application;
using Team.Application.Responses;
using Team.Domain.Entities.Identity;
using Team.Infrastructure;
using Team.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;

builder.Services.AddDbContext<DataContext>(
    x => x.UseSqlServer(
        config.GetConnectionString("TeamConnection"),
        options =>
        {
            options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        }
    )
);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Team Management API",
        Description = "An ASP.NET Core Web API for managing teams",
        TermsOfService = new Uri("https://www.datumsquare.com/"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://www.datumsquare.com/")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://www.datumsquare.com/")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
    });
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddIdentityServices(config);

var app = builder.Build();

using var scope = app.Services.CreateAsyncScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{

    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();

    var userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
    await IdentityDataSeeder.SeedAsync(userManager);
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex.Message);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Team Management API v1");
        options.RoutePrefix = string.Empty;
        options.DocExpansion(DocExpansion.None);
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();

//app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseApiResponseAndExceptionWrapper<MapApiResponse>(new AutoWrapperOptions
{
    IgnoreNullValue = false,
    ShowApiVersion = true,
    ShowStatusCode = true,
    ShowIsErrorFlagForSuccessfulResponse = true,
});

app.Run();
