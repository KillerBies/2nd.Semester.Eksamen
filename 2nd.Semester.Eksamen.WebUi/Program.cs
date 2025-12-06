using _2nd.Semester.Eksamen.Application.Adapters;
using _2nd.Semester.Eksamen.Domain.Entities.Persons;



using _2nd.Semester.Eksamen.Infrastructure.Data;
using _2nd.Semester.Eksamen.WebUi.Components;
using WebUIServices;
using Microsoft.EntityFrameworkCore;

using _2nd.Semester.Eksamen.Application.ApplicationInterfaces;
using _2nd.Semester.Eksamen.Application.Commands.EmployeeCmd;
using _2nd.Semester.Eksamen.Application.Services.BookingServices;
using _2nd.Semester.Eksamen.Application.Services.PersonService;
using _2nd.Semester.Eksamen.Domain.DomainInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Domain.DomainServices.BookingDomainService;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.CustomerInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.PersonInterfaces.EmployeeInterfaces;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces.BookingInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Repositories.ProductRepositories.BookingRepositories;
using _2nd.Semester.Eksamen.Infrastructure.Repositories.PersonRepositories;
using _2nd.Semester.Eksamen.Infrastructure.Repositories.PersonRepositories.EmployeeRepositories;
using _2nd.Semester.Eksamen.Infrastructure.Repositories.PersonRepositories.CustomerRepositories;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.ProductInterfaces;
using _2nd.Semester.Eksamen.Infrastructure.Repositories.ProductRepositories;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.DiscountInterfaces;

using _2nd.Semester.Eksamen.Infrastructure.Repositories.InvoiceRepositories;
using _2nd.Semester.Eksamen.Domain.RepositoryInterfaces.InvoiceInterfaces;
using QuestPDF.Infrastructure;
using _2nd.Semester.Eksamen.Infrastructure.PDFManagement;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
    b => b.MigrationsAssembly("2nd.Semester.Eksamen.Infrastructure")));
builder.Services.AddScoped<ITreatmentRepository,TreatmentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<ITreatmentBookingRepository, TreatmentBookingRepository>();
builder.Services.AddScoped<BookingFormService>();
builder.Services.AddScoped<PrivateCustomerService>();
builder.Services.AddScoped<CompanyCustomerService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<EmployeeSpecialtyService>();
builder.Services.AddScoped<BookingApplicationService>();
builder.Services.AddScoped<BookingQueryService>();
builder.Services.AddScoped<CreateEmployeeCommand>();
builder.Services.AddScoped<ISuggestionService, BookingSuggestionService>();
builder.Services.AddScoped<IBookingDomainService, BookingDomainService>();
builder.Services.AddScoped<ICompanyCustomerRepository, CompanyCustomerRepository>();
builder.Services.AddScoped<IPrivateCustomerRepository, PrivateCustomerRepository>();
builder.Services.AddScoped<ScheduleService>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<DTO_to_Domain>();
builder.Services.AddScoped<Domain_to_DTO>();
builder.Services.AddScoped<UpdateEmployeeCommand>();
builder.Services.AddScoped<ReadEmployeeUserCardsCommand>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IPrivateCustomerService, PrivateCustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

// if your repositories and services are separate:
builder.Services.AddScoped<IPrivateCustomerService, PrivateCustomerService>();
builder.Services.AddScoped<ICompanyCustomerService, CompanyCustomerService>();
builder.Services.AddScoped<IOrderLineRepository, OrderLineRepository>();

// and register a default ICustomerService (pick one or create a composite)
builder.Services.AddScoped<ICustomerService>(sp => sp.GetRequiredService<PrivateCustomerService>()); // or CustomerService


// Then register services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderLineService, OrderLineService>();



builder.Services.AddScoped<ISnapshotRepository, SnapshotRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IGenerateInvoice, GenerateInvoice>();
QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
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
