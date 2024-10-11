var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

Configure(app);

app.Run();

void ConfigureServices(IServiceCollection services)
{
    // Add services to the container.

    services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddCors();
}
void Configure(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(x =>
        x.SetIsOriginAllowed(o => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}