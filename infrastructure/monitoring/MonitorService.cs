using System.Net;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Serilog.ILogger;

namespace infrastructure.monitoring;

public static class MonitorService
{

    public static ILogger Log => Serilog.Log.Logger;
    
    
    static MonitorService()
    {
        Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();

    }
    
    
    
}