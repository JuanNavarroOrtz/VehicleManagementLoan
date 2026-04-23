using VehicleManagementLoan.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

/* Agregar servicios de infraestructura */
builder.Services.AddInfrastructure(builder.Configuration);
/* Agregar servicios de controladores */
builder.Services.AddControllers();
/* Agregar servicios para documentación de API */
builder.Services.AddEndpointsApiExplorer();

/* Agregar Swagger */
builder.Services.AddSwaggerGen();

var app = builder.Build();

/* Inicializar BD y datos de desarrollo */
await app.Services.SeedDBAsync();

/* Usar Swagger */
app.UseSwagger();
app.UseSwaggerUI();

/* Mapear controladores */
app.MapControllers();

app.Run();
