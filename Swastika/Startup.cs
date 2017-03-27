using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swastika.Data;
using Swastika.Models;
using Swastika.Services;
using Newtonsoft.Json.Serialization;
using Swastika.Services.Interfaces;
using Microsoft.AspNetCore.SpaServices.Webpack;

namespace Swastika
{
  public class Startup
  {
    // JPC enable portable dev database
    //private string _contentRootPath = "";
    private string _webRootPath = "";
    /// <summary>
    /// 
    /// </summary>
    /// <param name="env"></param>
    public Startup(IHostingEnvironment env)
    {
      // JPC enable portable dev database
      //_contentRootPath = env.ContentRootPath; // This doesn't work on IIS env
      _webRootPath = env.WebRootPath;

      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

      if (env.IsDevelopment())
      {
        // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
        builder.AddUserSecrets<Startup>();
      }

      builder.AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // JPC enable portable dev database
      string conn = Configuration.GetConnectionString("DefaultConnection");
      if (conn.Contains("%CONTENTROOTPATH%"))
      {
        conn = conn.Replace("%CONTENTROOTPATH%", _webRootPath);
      }

      // Add framework services.
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(conn));

      services.AddIdentity<ApplicationUser, IdentityRole>()
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();

      services
          .AddMvc()
          .AddJsonOptions(options => options.SerializerSettings.ContractResolver =
              new DefaultContractResolver()); // Added for Signalr feature

      // Add SignalR service
      // Source: https://chsakell.com/2016/10/10/real-time-applications-using-asp-net-core-signalr-angular/
      services.AddSignalR(options => options.Hubs.EnableDetailedErrors = true);

      // Add application services.
      services.AddTransient<IEmailSender, AuthMessageSender>();
      services.AddTransient<ISmsSender, AuthMessageSender>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
        app.UseBrowserLink();
        // Setup WebpackDevMidleware for "Hot module replacement" while debugging
        var options = new WebpackDevMiddlewareOptions() { HotModuleReplacement = true };
        app.UseWebpackDevMiddleware(options);
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();

      app.UseIdentity();

      // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
        // Setup additional routing for SPA
        routes.MapSpaFallbackRoute(
          name: "spa-fallback",
          defaults: new { controller = "Home", action = "Index" });
      });

      // Use SignalR service
      app.UseSignalR();
    }
  }
}
