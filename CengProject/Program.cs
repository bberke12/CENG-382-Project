using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CengProject.Data;
using CengProject.Models;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Veritabanı bağlantısı
var connectionString = builder.Configuration.GetConnectionString("ReservationDbConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// 🔹 Identity ayarları
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders()
.AddDefaultUI();

// 🔹 Authorization (opsiyonel)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});

// 🔹 Razor Pages + API Controller desteği
builder.Services.AddRazorPages();
builder.Services.AddControllers(); // 🔸 API controller için GEREKLİ

var app = builder.Build();

// 🔹 Seed Data (Rol ve admin)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.Initialize(services);
}

// 🔹 Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers(); // 🔸 API route'larını aktif eder

app.Run();
