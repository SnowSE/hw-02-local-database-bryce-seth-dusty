using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoTests;

public class MyWebAppFactory : WebApplicationFactory<Program>
{
    public string DataBaseName { get; set; } = "";
    private static string DatabaseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        DataBaseName = Guid.NewGuid().ToString();
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DatabaseInitialize));
            services.AddScoped<DatabaseInitialize>(x => new DatabaseInitialize(DataBaseName));
        });
    }

    public override ValueTask DisposeAsync()
    {
        File.Delete(Path.Combine(DatabaseDirectory, DataBaseName));
        return base.DisposeAsync(); 
    }
}
