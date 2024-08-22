using GTPWeb.Models;
using GTPWeb.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GTPContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GTP")));

// Add session services
builder.Services.AddDistributedMemoryCache(); // Optional: Use in-memory cache for session data
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the timeout for session
    options.Cookie.HttpOnly = true; // Cookies are accessible only via HTTP
    options.Cookie.IsEssential = true; // Mark the session cookie as essential
});

// Register services
builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUsuariosPService, UsuariosPService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();
builder.Services.AddScoped<IRolesService, RolesService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable session before authorization
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
