using System.Collections.Immutable;
using EventManagement.Configurations;
using EventManagement.Repositories;
using EventManagement.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IEventServices, EventSerivces>();
builder.Services.AddTransient<IAttendeeService, AttendeeService>();
builder.Services.AddTransient<IAttendeeRepository, AttendeeRepository>();
builder.Services.AddTransient(typeof(IEventRepository<>), typeof(EventRepository<>));
builder.Services.AddTransient(typeof(IEventManagementService<>), typeof(EventManagementService<>));
builder.Services.AddScoped(typeof(IUploadHandler<>), typeof(UploadHandler<>));
// builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddDbContext<EventDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDbContext<EventDbContext1>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection1"));
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

app.Run();
