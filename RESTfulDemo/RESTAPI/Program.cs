using RESTAPI.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using RESTAPI.Auth;
using RESTAPI.Repositories;
using RESTAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// 从配置读取数据库连接字符串
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// 配置DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// 添加认证服务并使用自定义Basic Auth Handler
builder.Services.AddAuthentication("Basic")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

// 添加授权(可选)
builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(logBuilder => { 
    logBuilder.AddConsole();
});
// builder.Services.AddScoped<ITestService, TestService>();

// 注册仓储和服务层
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 使用认证和授权中间件
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
