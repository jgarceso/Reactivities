using API.Extensions;
using API.Middleware;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy)); //Every controller will need authentication
});

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

//Exception handling should be the first middleware to configure in case we are useing a custom one like this to catch errors in other used services.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var context = services.GetRequiredService<ReactivitiesDbContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
await context.Database.MigrateAsync();
await Seed.SeedData(context, userManager);
//try
//{
//    var context = services.GetRequiredService<DataContext>();
//    await context.Database.MigrateAsync();
//    await Seed.SeedData(context);
//}
//catch (Exception ex)
//{
//    var logger = services.GetRequiredService<ILogger<Program>>();
//    logger.LogError(ex, "An error has ocurred during the migration");
//}
app.Run();
