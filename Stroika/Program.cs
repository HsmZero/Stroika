using Microsoft.EntityFrameworkCore;
using Stroika.DAL;
using Stroika.Services;
using Stroika.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string MyAllowSpecificOrigins = "_Stroika";
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region Strapping
builder.Services.AddDbContext<StroikaDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StroikaDBConnection"));

});

builder.Services.AddTransient<ILookupsService, LookupsService>();
builder.Services.AddTransient<IStudentFamilyService, StudentFamilyService>();
builder.Services.AddTransient<IStudentService, StudentService>();
#endregion
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
    builder =>
    {
        builder.WithOrigins("*", "http://localhost:3000/")
                            .AllowAnyHeader()
                    .AllowAnyMethod().SetIsOriginAllowed((hots) => true)
                    .AllowCredentials();
    });
});
var serviceProvider = builder.Services.BuildServiceProvider();
var db = serviceProvider.GetService<StroikaDBContext>();
StroikaDBContext.EnsureDBCreated(db);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
