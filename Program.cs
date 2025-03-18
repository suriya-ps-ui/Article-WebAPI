using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder=WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>{
    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme{
        In=ParameterLocation.Header,
        Name="Authorization",
        Type=SecuritySchemeType.ApiKey,
        Scheme="Bearer",
        BearerFormat="JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference=new OpenApiReference{
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Register DatabaseCon with dependency injection
builder.Services.AddScoped<WebAPI.Database.DatabaseCon>();
// Register ArticleService with dependency injection
builder.Services.AddScoped<WebAPI.Services.ArticleService>();
builder.Services.AddScoped<WebAPI.Services.UserService>();
builder.Services.AddAuthentication(options=>{
    options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options=>{
    options.TokenValidationParameters=new TokenValidationParameters{
        ValidateIssuer=false,
        ValidateAudience=false,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]??""))
    };
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI(c =>{
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix=string.Empty; // Serve Swagger UI at application's root
    });
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();