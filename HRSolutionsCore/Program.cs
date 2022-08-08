using HRSolutions.BusinessLayer;
using HRSolutionsCore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Add services to the container.
var conn = builder.Configuration.GetValue("conn", builder.Configuration.GetConnectionString("Conn"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<MasterBusiness>();
builder.Services.AddCors();
builder.Services.AddDbContext<HRManagementDbContext>(options =>
{
    options.UseSqlServer(conn);
#if (DEBUG)
    options.EnableSensitiveDataLogging();
#endif
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

app.UseAuthorization();
app.UseCors(
       options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
   );
app.MapControllers();

app.Run();
