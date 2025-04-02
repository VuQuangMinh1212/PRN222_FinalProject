using MedicalSearchingPlatform.Business.Hubs;
using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Business.Services;
using MedicalSearchingPlatform.Data;
using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;
using MedicalSearchingPlatform.Data.Repositories;
using MedicalSearchingPlatform.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();



var configuration = new ConfigurationBuilder()
    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MedicalSearchingPlatform.Data")) // Trỏ đến thư mục Data
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
/*var configuration = builder.Configuration;*/


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddAuthentication()
//    .AddGoogle(options =>
//    {
//        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
//        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
//        options.SignInScheme = IdentityConstants.ExternalScheme;
//        options.CallbackPath = "/signin-google";
//    });


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMedicalFacilityRepository, MedicalFacilityRepository>();
builder.Services.AddScoped<IMedicalServiceRepository, MedicalServiceRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
builder.Services.AddScoped<IWorkingScheduleRepository, WorkingScheduleRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
builder.Services.AddScoped<IAppoimentMedicalServiceRepository, AppoimentMedicalServiceRepository>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMedicalFacilityService, MedicalSearchingPlatform.Business.Services.MedicalFacilityService>();
builder.Services.AddScoped<IMedicalServiceService, MedicalServiceService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
builder.Services.AddScoped<IWorkingScheduleService, WorkingScheduleService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
builder.Services.AddScoped<IAppoimentMedicalService, AppoimentMedicalService>();

//SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Ensure the database is migrated and seeded with initial data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate(); // Ensures latest migration is applied
    DbInitializer.Initialize(context);
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Error", "?errorMessage=Error {0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapHub<SignalRServer>("/signalRServer");
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.UseSession();
app.Run();

