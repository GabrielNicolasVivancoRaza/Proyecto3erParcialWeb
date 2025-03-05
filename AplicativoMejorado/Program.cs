var builder = WebApplication.CreateBuilder(args);

// Agregar servicios necesarios para sesiones
builder.Services.AddDistributedMemoryCache(); // Usar memoria para almacenar las sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de espera para la sesi�n
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Necesario para la sesi�n
});

// Agregar servicios de MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Usar la sesi�n y la autorizaci�n
app.UseSession(); // Habilitar el uso de sesiones

// Configurar el pipeline de la aplicaci�n
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// Configurar la ruta predeterminada
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
