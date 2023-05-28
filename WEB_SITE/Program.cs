using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Fabrica de HttpClient
builder.Services.AddHttpClient("Base", client =>
{
    client.BaseAddress = new Uri("https://auroraaerlinesapi.azurewebsites.net/api/");
    client.Timeout = TimeSpan.FromSeconds(120);
}).AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError()
            .RetryAsync(3))
        .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(45)));
//Configuracion Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
