using EnglishNowDemo.Repositories;
using EnglishNowDemo.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/login";
        option.AccessDeniedPath = new PathString("/login");
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

var connectionString = builder.Configuration.GetConnectionString("EnglishNowConnectionString");

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ITurmaService, TurmaService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(c => new UsuarioRepository(connectionString!));
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>(c => new ProfessorRepository(connectionString!));
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>(c => new AlunoRepository(connectionString!));
builder.Services.AddScoped<ITurmaRepository, TurmaRepository>(c => new TurmaRepository(connectionString!));
builder.Services.AddScoped<IAlunoTurmaBoletimRepository, AlunoTurmaBoletimRepository>(c => new AlunoTurmaBoletimRepository(connectionString!));

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
