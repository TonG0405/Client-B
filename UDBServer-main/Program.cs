using System.Net;
using UDPForward.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin() // Cho phép yêu cầu từ tất cả các nguồn
               .AllowAnyMethod()  // Cho phép tất cả phương thức HTTP
               .AllowAnyHeader(); // Cho phép tất cả các header
    });
});


// Add services to the container.
builder.Services.AddSingleton<ConnectionService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll"); // Áp dụng chính sách CORS cho toàn bộ ứng dụng


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
