using app.BE;
using app.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<AppDbContext>(provider => {
    var configuration = provider.GetService<IConfiguration>();
    return new AppDbContext(configuration);
});

builder.Services.AddScoped<FabricanteBE>();
builder.Services.AddScoped<ProdutoBE>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
