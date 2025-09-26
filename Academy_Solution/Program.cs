using AcademyWeb.Application;
using AcademyWeb.Authentication;
using AcademyWeb.Infrastructure;
using AcademyWeb.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);


//serilog configuration
Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().
    WriteTo.File("ErrorLogs/log.txt", LogEventLevel.Verbose, rollingInterval: RollingInterval.Day,  //log daily per date one file
    fileSizeLimitBytes: 50 * 1024 * 1024,   //maximum 50mb size
    rollOnFileSizeLimit: true,    //create a new file when size limits exceeds
    retainedFileCountLimit: null).CreateLogger();


// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AcademyCon")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.RegisterJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
