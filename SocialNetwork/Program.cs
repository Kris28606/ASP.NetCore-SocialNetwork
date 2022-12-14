using BusinesLogicLayer.Implementation;
using BusinesLogicLayer.Interfaces;
using DataAccessLayer.UnitOfWork;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.HubConfig;
using System.Text;
using WebApi2.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<JwtAuthentification>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IFollowNotificationService, FollowNotificationService>();
builder.Services.AddScoped<ILikeNotificationService, LikeNotificationService>();
builder.Services.AddScoped<ICommentNotificationService, CommentNotificationService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IReactionService, ReactionService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IAuthentificationService, AuthentificationService>();
builder.Services.AddScoped<JwtAuthentification>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<UserContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("baza"));
});
builder.Services.AddIdentity<User, IdentityRole<int>>().AddEntityFrameworkStores<UserContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();
builder.Services.AddAuthentication(options=> {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            // def kako da se enkoduje
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("NapredneNetTehnologijeTokenZaAuthentifikaciju"))

        };
    });
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
        builder =>
        {
            builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllHeaders");
//app.UseCors(p =>
//{
//    p.AllowAnyHeader();
//    p.AllowAnyMethod();
//    p.AllowAnyOrigin();
//    //p.AllowCredentials();
//});

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<MyHub>("/hub");

app.Run();
