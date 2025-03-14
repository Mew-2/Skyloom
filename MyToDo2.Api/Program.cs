using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyToDo2.Api.Context;
using MyToDo2.Api.Extensions;
using MyToDo2.Api.Repository;
using MyToDo2.Api.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<MyToDo2Context>(option =>
{
    var connectionString = builder.Configuration.GetConnectionString("ToDoConnection");
    option.UseSqlite(connectionString);
}).AddUnitOfWork<MyToDo2Context>()
.AddCustomRepository<Memo, MemoRepository>()
.AddCustomRepository<ToDo, ToDoRepository>()
.AddCustomRepository<User, UserRepository>();

//Ìí¼Ó·þÎñ
builder.Services.AddTransient<IToDoService, ToDoService>();
builder.Services.AddTransient<IMemoService, MemoService>();
builder.Services.AddTransient<ILoginService, LoginService>();

// ÅäÖÃAutoMapperÓ³Éä
var automapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});
builder.Services.AddSingleton(automapperConfig.CreateMapper());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();