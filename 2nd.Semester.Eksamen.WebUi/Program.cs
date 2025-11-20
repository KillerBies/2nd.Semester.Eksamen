using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.RepositoryInterfaces;
using _2nd.Semester.Eksamen.Application.Services;
using _2nd.Semester.Eksamen.Infrastructure.Data;
using _2nd.Semester.Eksamen.Infrastructure.Repositories;
using _2nd.Semester.Eksamen.WebUi.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//TODO FJERN LIGE DISSE TO SERVICES EFTER BRUG"!!""
builder.Services.AddScoped<ICompanyCustomerService, CompanyCustomerService>();
builder.Services.AddScoped<ICompanyCustomerRepository, CompanyCustomerRepository>();
builder.Services.AddScoped<IPrivateCustomerService, PrivateCustomerService>();
builder.Services.AddScoped<IPrivateCustomerRepository, PrivateCustomerRepository>();


builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
    b => b.MigrationsAssembly("2nd.Semester.Eksamen.Infrastructure")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
