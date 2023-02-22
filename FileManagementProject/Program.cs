using FileManagementProject.Entities;
using FileManagementProject.Repository;
using FileManagementProject.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(
        "MyDb")));

// Add scoped to create a new instance for each request
builder.Services.AddScoped<IFileRepository, FileService>();
builder.Services.AddScoped<IFolderRepository, FolderService>();

// custom json serializer
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
    options.SerializerSettings.Formatting = Formatting.Indented;
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificUrl", builder =>
    {
        builder.WithOrigins("https://localhost:5500")
            .AllowAnyMethod()
            .AllowAnyHeader().AllowCredentials();
    });
});

// Add Azure AD Authentication, Open Id Connect, and custom Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = $"https://login.microsoftonline.com/{builder.Configuration["AzureAd:TenantId"]}";
    options.Audience = builder.Configuration["AzureAd:ClientId"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true
    };
}).AddOpenIdConnect("OpenIdConnect", options =>
{
    options.Authority = $"https://login.microsoftonline.com/{builder.Configuration["AzureAd:TenantId"]}";
    options.ClientId = builder.Configuration["AzureAd:ClientId"];
    options.ClientSecret = builder.Configuration["AzureAd:ClientSecret"];
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
}).AddCookie(options =>
{
    options.Cookie.Name = "FileManagementProject";
    options.LoginPath = "/api/Auth/signin";
    options.LogoutPath = "/api/Auth/signout";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.None;
    // options.AccessDeniedPath = "/account/access-denied";
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("AllowSpecificUrl");
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();