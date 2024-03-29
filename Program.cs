using Microsoft.EntityFrameworkCore;
using Sample_Api.Data;
using Sample_Api.Services.CharacterService;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json").Build();
builder.Services.AddDbContext<DataContext>(optionsAction:
options =>
{
    options.UseSqlServer(connectionString: configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ICharacterService,CharacterService>();
builder.Services.AddScoped<IAuthRepository,AuthRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options=>{
options.TokenValidationParameters=new TokenValidationParameters{
    ValidateIssuerSigningKey=true,
    IssuerSigningKey=new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection(key:"AppSettings:Token").Value)),
    ValidateIssuer=false,
    ValidateAudience=false
};
});

builder.Services.AddSwaggerGen(
    c=>{
        c.SwaggerDoc(name:"v1",info:new OpenApiInfo{Title="dotnet_rpg",Version="v1"});
        c.AddSecurityDefinition("oauth2",new OpenApiSecurityScheme{
            Description="Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
            In =ParameterLocation.Header,
            Name="Authorization",
            Type=SecuritySchemeType.ApiKey
        });
        c.OperationFilter<SecurityRequirementsOperationFilter>();
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
