using Blog;
using Blog.Data;
using Blog.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateActor = false
    };
});

builder.Services.AddControllers().ConfigureApiBehaviorOptions(
    options
        => options.SuppressModelStateInvalidFilter = true
);


builder.Services.AddDbContext<BlogDataContext>();


builder.Services.AddTransient<TokenService>(); //SEMPRE cria uma nova inst�ncia;
//builder.Services.AddScoped();// A inst�ncia dura POR TRANSA��O;
//builder.Services.AddSingleton();// Padr�o Singleton -> Uma nova inst�ncia POR APLICA��O;
var app = builder.Build();

//SEMPRE NESSA ORDEM:
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();


app.Run();
