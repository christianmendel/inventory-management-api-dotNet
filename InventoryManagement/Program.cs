using InventoryManagement.Repository;
using InventoryManagement.Service;
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

builder.Services.AddTransient<CategoryService>();
builder.Services.AddTransient<CustomerService>();
builder.Services.AddTransient<InventoryMovementService>();
builder.Services.AddTransient<OrderService>();
builder.Services.AddTransient<OrderItemService>();
builder.Services.AddTransient<ProductService>();

// Adiciona os servi�os ao cont�iner de inje��o de depend�ncia
builder.Services.AddControllers();

// Configura��o do Swagger para documenta��o da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o pipeline de requisi��o HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
