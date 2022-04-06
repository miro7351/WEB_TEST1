using ContosoUniversity.Data;
using Microsoft.EntityFrameworkCore;

/*
 * MH: 05.04.2022
 * zdroj:  https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-6.0
 * publikovane: 26.03.2022
 * POZOR: pouziva este Startup.cs!!!!
 * 
 * 
 * aplikacia pouziva DB, ak este DB neexistuje, potom si ju vygeneruje automaticky :):)
 * "DefaultConnection": "Server=HRABCAK;Database=ContosoUniversity1;Trusted_Connection=True;MultipleActiveResultSets=true"
 * ----------------------------
 * 
 * https://www.sharepointcafe.net/2022/01/create-first-mvc-application-using-net-6-in-visual-studio-2022.html
 * there is no more startup.cs file in .NET 6. 
 * It can be thought of as a major change in .NET 6 framework.
   .NET 6 startup.cs file code is merged in Program.cs file.
*
* -----------------------------
 * POZRI !!!!!: https://docs.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-6.0?view=aspnetcore-6.0
 * je tam link na:
 * ASP.NET Core Blazor file download  !!!!
 * Work with images in ASP.NET Core Blazor  !!!!
 */


var builder = WebApplication.CreateBuilder(args);
//WebApplication.CreateBuilder initializes a new instance of the WebApplicationBuilder class with preconfigured defaults. 

#region --- Adding services in container ----

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

//--toto sa robilo v Startup.cs--
//The name of the connection string is passed in to the context by calling a method on a DbContextOptionsBuilder object
//For local development, the ASP.NET Core configuration system reads the connection string from the appsettings.json file.
var conString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SchoolContext>(options => options.UseSqlServer(conString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();//The AddDatabaseDeveloperPageExceptionFilter provides helpful error information in the development environment.
//-------------------------------

#endregion --- Adding services in container ----


var app = builder.Build();

CreateDbIfNotExists(app);  //musi byt az po: builder.Build(); MH pridane 5.4.2022 Vytvori databazu a naplni tab. testovacimi zaznamami;

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




#region == MH: local function ==

    /// <summary>
    /// VYtvori DB a tabulky naplni testovacimi zaznamami
    /// </summary>
    static void CreateDbIfNotExists(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<SchoolContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }

#endregion == MH: local function ==