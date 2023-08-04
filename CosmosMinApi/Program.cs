using AutoMapper;
using CosmosMinApi.Data;
using CosmosMinApi.Domains;
using CosmosMinApi.Dtos;
using CosmosMinApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseCosmos(
        builder.Configuration["CosmosSettings:AccountUri"],
        builder.Configuration["CosmosSettings:AccountKey"],
        builder.Configuration["CosmosSettings:DatabaseName"]
    ));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("api/products", async (IProductRepository repository, IMapper mapper) =>
{
    var products = await repository.GetAllAsync();
    return Results.Ok(mapper.Map<IEnumerable<ProductReadDto>>(products));
});

app.MapGet("api/products/{id:guid}", async (IProductRepository repository, IMapper mapper, Guid id) =>
{
    var command = await repository.GetByIdAsync(id);
    if (command != null)
    {
        return Results.Ok(mapper.Map<ProductReadDto>(command));
    }

    return Results.NotFound();
});

app.MapPost("api/products", async (IProductRepository repository, IMapper mapper, ProductCreateDto productCreateDto) =>
{
    var productModel = mapper.Map<Product>(productCreateDto);

    await repository.AddAsync(productModel);

    var productReadDto = mapper.Map<ProductReadDto>(productModel);

    return Results.Created($"api/products/{productReadDto.Id}", productReadDto);
});

app.MapPut("api/products/{id:guid}",
    async (IProductRepository repository, IMapper mapper, Guid id, ProductUpdateDto cmdUpdateDto) =>
    {
        var command = await repository.GetByIdAsync(id);
        if (command == null)
        {
            return Results.NotFound();
        }

        mapper.Map(cmdUpdateDto, command);

        await repository.UpdateAsync(command);

        return Results.Ok();
    });

app.MapDelete("api/products/{id:guid}", async (IProductRepository repository, IMapper mapper, Guid id) =>
{
    var product = await repository.GetByIdAsync(id);
    if (product == null)
    {
        return Results.NotFound();
    }

    await repository.DeleteAsync(product.Id);

    return Results.NoContent();
});

app.Run();