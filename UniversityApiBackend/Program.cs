//1.Using to work with EntityFramework
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Services;

var builder = WebApplication.CreateBuilder(args);

//2. Connection with SQL Server Express
const string CONNECTIONNAME = "UniversityDB";//mismo name q en el arch .json
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

//3. Add services to the container. --> LA agrego YO !!
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

//4. Add Custom Services --> (folder Services) --> los creo YO
builder.Services.AddScoped<IStudentsService, StudentsService>();
builder.Services.AddScoped<ICoursesService, CoursesService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//5. CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
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

app.UseAuthorization();

app.MapControllers();

//6. uso de CORS
app.UseCors("CorsPolicy");

app.Run();
