using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.IdentityModel.Tokens;
using My_site.DAL;
using My_site.DAL.Repositories;
using My_site.Domain.Attributes;
using My_site.Domain.Models;
using My_site.Services.Authentication;
using My_site.Services.Data;
using My_site.Services.Implementations;
using My_site.Services.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationContext>();

builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddTransient<IComputerRepository, ComputerRepository>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();   
builder.Services.AddTransient<IComputerService, ComputerService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<IUserActionLogRepository, UserActionLogRepository>();
builder.Services.AddScoped<IUserActionLogger, UserActionLogger>();
builder.Services.AddScoped<LogUserActionAttribute>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<My_site.DAL.Repositories.AuthorizationOptions>(builder.Configuration.GetSection(nameof(My_site.DAL.Repositories.AuthorizationOptions)));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();
        if (jwtOptions == null || string.IsNullOrEmpty(jwtOptions.SecretKey))
        {
            throw new InvalidOperationException("JwtSettings.Secret is missing or invalid in appsettings.json.");
        }
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["token"];
                return Task.CompletedTask;
            },
        };
    });

builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.Requirements.Add(new PermissionRequirement(new[] { Permission.Read, Permission.Update, Permission.Create, Permission.Delete }));
    });
    options.AddPolicy("UserPolicy", policy =>
    {
        policy.Requirements.Add(new PermissionRequirement(new[] { Permission.Read }));
    });
});

builder.Services.AddControllers(options =>
{
    var serviceProvider = builder.Services.BuildServiceProvider();
    var logger = serviceProvider.GetService<IUserActionLogger>();
    options.Filters.Add(new LogUserActionAttribute(logger));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "my_api"));

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<AuthMiddleware>();

app.UseRouting();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();