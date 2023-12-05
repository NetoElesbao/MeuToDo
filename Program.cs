var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello World!");
    });

    endpoints.MapGet("/rotateste", async context =>
    {
        var configuration = context.RequestServices.GetRequiredService<IConfiguration>();
        var mensagem = configuration["mensagem"];
        await context.Response.WriteAsync(mensagem);
    });
});


app.MapGet("/", () => "Hello World!");

app.MapGet("/rotateste", (IConfiguration configuration) =>
{
    return Results.Ok(configuration["mensagem"]);
});

app.Run();
