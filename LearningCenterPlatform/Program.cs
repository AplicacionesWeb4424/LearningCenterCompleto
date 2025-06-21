using LearningCenterPlatform.IAM.Application.Internal.CommandServices;
using LearningCenterPlatform.IAM.Application.Internal.OutboundServices;
using LearningCenterPlatform.IAM.Application.Internal.QueryServices;
using LearningCenterPlatform.IAM.Domain.Repositories;
using LearningCenterPlatform.IAM.Domain.Services;
using LearningCenterPlatform.IAM.Infrastructure.Hashing.BCrypt.Services;
using LearningCenterPlatform.IAM.Infrastructure.Persistence.EFC.Repositories;
using LearningCenterPlatform.IAM.Infrastructure.Tokens.JWT.Configuration;
using LearningCenterPlatform.IAM.Infrastructure.Tokens.JWT.Services;
using LearningCenterPlatform.IAM.Interfaces.ACL.Services;
using LearningCenterPlatform.IAM.Interfaces.ACL;
using LearningCenterPlatform.Profiles.Application.Internal.CommandServices;
using LearningCenterPlatform.Profiles.Application.Internal.QueryServices;
using LearningCenterPlatform.Profiles.Domain.Repositories;
using LearningCenterPlatform.Profiles.Domain.Services;
using LearningCenterPlatform.Profiles.Infrastructure.Persistence.EFC.Repositories;
using LearningCenterPlatform.Publishing.Application.Internal.CommandServices;
using LearningCenterPlatform.Publishing.Application.Internal.QueryServices;
using LearningCenterPlatform.Publishing.Domain.Repositories;
using LearningCenterPlatform.Publishing.Domain.Services;
using LearningCenterPlatform.Publishing.Infrastructure.Persistence.EFC.Repositories;
using LearningCenterPlatform.Shared.Domain.Repositories;
using LearningCenterPlatform.Shared.Infrastructure.Interfaces.ASP.Configuration;
using LearningCenterPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using LearningCenterPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using LearningCenterPlatform.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Cortex.Mediator.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls=true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


if (connectionString == null) throw new InvalidOperationException("Connection string not found.");


builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "ACME.LearningCenterPlatform.API",
            Version = "v1",
            Description = "ACME Learning Center Platform API",
            TermsOfService = new Uri("https://acme-learning.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "ACME Studios",
                Email = "contact@acme.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
    options.EnableAnnotations();
});


// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Publishing Bounded Context
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITutorialRepository, TutorialRepository>();
builder.Services.AddScoped<ICategoryCommandService, CategoryCommandService>();
builder.Services.AddScoped<ICategoryQueryService, CategoryQueryService>();
builder.Services.AddScoped<ITutorialCommandService, TutorialCommandService>();
builder.Services.AddScoped<ITutorialQueryService, TutorialQueryService>();
builder.Services.AddScoped<IAssetsRepository, AssetsRepository>();


// Profiles Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();

// IAM Bounded Context Injection Configuration

// TokenSettings Configuration

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

// Mediator Configuration

// Add Mediator Injection Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

// Add Cortex Mediator for Event Handling
builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: new[] { typeof(Program) }, configure: options =>
    {
        options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>));
        //options.AddDefaultBehaviors();
    });


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();
}   

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty; // Opcional: para que Swagger sea la página raíz
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

    });

}

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

// Add Authorization Middleware to Pipeline
app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
