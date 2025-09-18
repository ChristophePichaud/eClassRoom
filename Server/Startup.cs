using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using EFModel;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<EClassRoomDbContext>(options =>
            options.UseNpgsql("Your_PostgreSQL_Connection_String_Here"));

        services.AddControllers();
        services.AddScoped<Server.Services.AuthService>();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();  
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            c.RoutePrefix = string.Empty;
            c.DocumentTitle = "My Custom API Docs";
            c.InjectStylesheet("/swagger-custom.css");
            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None); // or .List, .Full
            c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}