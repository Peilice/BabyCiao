using BabyCiaoAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<BabyciaoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Babyciao")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();
var myPolicy = "andy";
builder.Services.AddCors(o => {
    o.AddPolicy(name: myPolicy, policy => {
        policy.WithOrigins("*").WithMethods("*").WithHeaders("*");
    });
});

builder.Services.AddSwaggerGen(options =>
{
    //定義安全方案，使用了OAuth 2.0的Bearer方案。指定標頭中的授權信息，並描述方案的用途。
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "test",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    //操作過濾器，它確保每個端點都需要 “Bearer” 授權。這意味著只有帶有有效 Bearer 標記的請求才能訪問這些端點。
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//使用JWT Bearer 身份驗證。
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

// 提供自定義目錄中的靜態文件
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "StaticFiles")),
    RequestPath = "/StaticFiles"
});
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
