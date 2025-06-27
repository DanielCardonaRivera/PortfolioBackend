var builder = WebApplication.CreateBuilder(args);

// Habilitar controladores
builder.Services.AddControllers();

// Habilitar CORS para permitir peticiones desde Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Habilitar HTTPS y CORS
app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");

// Usar controladores
app.MapControllers();

app.Run();
