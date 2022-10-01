using Formel.API;
using Formel.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FormulaContext>(opt => opt.UseInMemoryDatabase("Formel"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapPost("v1/formulas", async (FormulaContext context, PostFormula model) =>
{
   if(!context.Formulas.Any(f => f.Name == model.Name))
    {
        var formula = new Formula
        {
            Definition = model.Definition,
            Parameters = string.Join(",", model.Parameters.ToArray()),
            Name = model.Name,
            Category = model.Category,
            Description = model.Description
        };

        context.Formulas.Add(formula);

        await context.SaveChangesAsync();

        return Results.Created($"/formulas/{formula.Id}", formula.Id);
    }

    return Results.BadRequest();

});

app.MapGet("v1/formulas/{id}", async (FormulaContext context, Guid id) =>
{
    var formula = await context.Formulas.FindAsync(id);

    var response = new ResponseFormulaDetails
    {
        Id = formula.Id,
        Definition = formula.Definition,
        Parameters = formula.Parameters.Split(',').ToList(),
        Name = formula.Name,
        Category = formula.Category,
        Description = formula.Description
    };

    return Results.Ok(response);

});

app.MapGet("v1/formulas/", async (FormulaContext context) =>
    await context.Formulas.ToListAsync());

app.MapPost("v1/formulas/execute", async (FormulaContext context, ExecuteFormula model) =>
{
    var formula = await context.Formulas.FindAsync(model.Id);

    if (formula is not Formula || formula is null)
    {
        Results.NotFound();
    }

    return Formula.Run(formula.Definition, model.Parameters);

});

app.MapPut("v1/formulas/{id}", async (FormulaContext context, Guid id, PutFormula model) =>
{
    if (await context.Formulas.FindAsync(id) is Formula formula)
    {
        formula.Definition = model.Definition;
        formula.Parameters = string.Join(",", model.Parameters.ToArray());
        formula.Name = model.Name;
        formula.Category = model.Category;
        formula.Description = model.Description;

        await context.SaveChangesAsync();

        return Results.Ok();
    }

    return Results.NotFound();

});

app.MapDelete("v1/formulas/{id}", async (FormulaContext context, Guid id) =>
{
    if (await context.Formulas.FindAsync(id) is Formula formula)
    {
        context.Formulas.Remove(formula);
        await context.SaveChangesAsync();
        return Results.Ok();
    }

    return Results.NotFound();

});

app.Run();
