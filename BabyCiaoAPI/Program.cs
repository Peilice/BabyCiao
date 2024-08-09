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
    //�w�q�w����סA�ϥΤFOAuth 2.0��Bearer��סC���w���Y�������v�H���A�ôy�z��ת��γ~�C
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "test",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    //�ާ@�L�o���A���T�O�C�Ӻ��I���ݭn ��Bearer�� ���v�C�o�N���ۥu���a������ Bearer �аO���ШD�~��X�ݳo�Ǻ��I�C
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//�ϥ�JWT Bearer �������ҡC
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

// ���Ѧ۩w�q�ؿ������R�A���
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
