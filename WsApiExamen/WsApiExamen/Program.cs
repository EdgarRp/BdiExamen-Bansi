

using Microsoft.EntityFrameworkCore;
using WsApiExamen.Data;
using WsApiExamen.Data.Repositories.Interface;
using WsApiExamen.Data.Services;
using WsApiExamen.Data.Services.Helper;

var builder = WebApplication.CreateBuilder(args);



#region Builder Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WsApiExamenDbContext>( options =>
    options.UseSqlServer( builder.Configuration.GetConnectionString( "DefaultConnection" ) ) );

builder.Services.AddScoped<IExamManager, ExamManager>();
builder.Services.AddScoped<TransactionRepository>();
#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
