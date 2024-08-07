using BabyCiaoAPI.Models;
using BabyCiaoAPI.Helpers; // �ޤJ�۩w�q�� JsonConverter �R�W�Ŷ�
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// �[���t�m���
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// �K�[�A�Ȩ�e��
builder.Services.AddDbContext<BabyciaoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BabyCiao")));

// �t�m JSON �ǦC�ƿﶵ�H�B�z�`���Ѧ�
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()); // �K�[�۩w�q�� DateOnlyJsonConverter
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// �]�m CORS �F��
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.WithOrigins("https://localhost:7231", "https://localhost:7000")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials(); // ���\���ҡ]Cookie�^
        });
});


// �t�m JWT ����
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]); // �ϥΰt�m�����K�_
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// �t�m Swagger �H��� JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BabyCiaoAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
    {
        new OpenApiSecurityScheme{
            Reference = new OpenApiReference{
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[]{}
    }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BabyCiaoAPI v1");
        //c.RoutePrefix = string.Empty; // �T�OSwagger UI�b�ڸ��|�B��
    });
}

app.UseHttpsRedirection();

// �ϥ� CORS �F��
app.UseCors("AllowAll");

app.UseAuthentication(); // �K�[�������Ҥ�����
app.UseAuthorization();

app.MapControllers();

app.Run();
