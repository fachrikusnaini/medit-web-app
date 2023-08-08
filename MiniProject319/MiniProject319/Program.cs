global using MiniProject319.api.Services.EmailService;
using Microsoft.EntityFrameworkCore;
using MiniProject319.DataModels;
using MiniProject319.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<RoleServices>();
builder.Services.AddScoped<AuthServices>();
builder.Services.AddScoped<BankServices>();
builder.Services.AddScoped<PaymentServices>();
builder.Services.AddScoped<IEmailService, EmailService>();

//Add Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromHours(1);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;

});


//Add connection string
builder.Services.AddDbContext<DB_SpecificationContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//Add session
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
