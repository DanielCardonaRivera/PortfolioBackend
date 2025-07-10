var builder = WebApplication.CreateBuilder(args);

// Controladores
builder.Services.AddControllers();

// Configurar CORS para Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // puerto de Angular
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
