using RESTAPI.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using RESTAPI.Auth;
using RESTAPI.Repositories;
using RESTAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// �����ö�ȡ���ݿ������ַ���
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// ����DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// �����֤����ʹ���Զ���Basic Auth Handler
builder.Services.AddAuthentication("Basic")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

// �����Ȩ(��ѡ)
builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(logBuilder => { 
    logBuilder.AddConsole();
});
// builder.Services.AddScoped<ITestService, TestService>();

// ע��ִ��ͷ����
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ʹ����֤����Ȩ�м��
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
