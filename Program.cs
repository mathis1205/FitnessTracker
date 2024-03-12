using Microsoft.EntityFrameworkCore;
using PeopleManager.Ui.Mvc.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FitnessTrackerDbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(FitnessTrackerDbContext));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    using var scope = app.Services.CreateScope();
    var peopleManagerDbContext = scope.ServiceProvider.GetRequiredService<FitnessTrackerDbContext>();
    peopleManagerDbContext.Seed();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
