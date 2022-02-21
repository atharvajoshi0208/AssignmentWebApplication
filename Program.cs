using AssignmentWebApplication.Data;
using Microsoft.EntityFrameworkCore;


var AllowSpecificOrigins = "_AllowSpecificOrigins";
var AllowSpecificOrigins1 = "_AllowSpecificOrigins1";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Enable CORS
//builder.Services.AddCors();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: AllowSpecificOrigins,
//        builder =>
//        {
//            builder.WithOrigins("htpp://localhost:4200")
//            .AllowAnyMethod()
//            .AllowAnyHeader();
//        });
//});


builder.Services.AddCors(options =>
{
    options.AddPolicy(
    name: "AllowOrigin",
    builder => {
        builder.WithOrigins("htpp://localhost:4200").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors(AllowSpecificOrigins);
app.UseCors("AllowOrigin");
//app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod());
app.UseAuthorization();

app.MapControllers();

app.Run();
