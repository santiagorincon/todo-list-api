using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using todo_list.DataAccess;
using todo_list.DataAccess.Models;
using Xunit;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace todo_list_api_test
{
    public class ApiTests
    {
        
    }
}

internal class EnpointsTests : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection> _serviceOverride;

    public EnpointsTests(Action<IServiceCollection> serviceOverride) {
        _serviceOverride = serviceOverride;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(_serviceOverride);
        return base.CreateHost(builder);
    }
}