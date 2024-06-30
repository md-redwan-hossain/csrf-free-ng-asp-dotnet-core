using CsrfFree.HttpApi;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.TryAddTransient<CsrfCookieMiddleware>();
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = "XSRF-TOKEN";
    options.HeaderName = "X-XSRF-TOKEN";
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSites", x => x
        .WithOrigins("http://localhost:4200")
        .WithHeaders("X-XSRF-TOKEN")
        .WithExposedHeaders("X-XSRF-TOKEN")
        .AllowAnyMethod()
        .AllowCredentials()
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSites");
app.UseMiddleware<CsrfCookieMiddleware>();
app.MapControllers();
app.Run();