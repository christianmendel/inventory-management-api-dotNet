using InventoryManagement.Repository;
using Npgsql;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(connectionString));
builder.Services.AddTransient<CategoryRepository>();
builder.Services.AddTransient<CustomerRepository>();
builder.Services.AddTransient<InventoryMovementRepository>();
builder.Services.AddTransient<OrderItemRepository>();
builder.Services.AddTransient<OrderRepository>();
builder.Services.AddTransient<ProductRepository>();

// Adiciona os serviços ao contêiner de injeção de dependência
builder.Services.AddControllers();

// Configuração do Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
