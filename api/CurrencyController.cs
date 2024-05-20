using infrastructure.Models;
using infrastructure.monitoring;
using Microsoft.AspNetCore.Mvc;
using service;

namespace api;

[ApiController]
[Route("[controller]")]
public class CurrencyController: ControllerBase
{

    private readonly CurrencyService _service;
    private readonly FeatureHubService _fhService;
    
    public CurrencyController(CurrencyService service, FeatureHubService fhService)
    {
        _service = service;
        _fhService = fhService;
    }
    
    [HttpGet]
    [Route("GetAll")]
    public List<Currency> Get()
    {
        
        return _service.GetAllCurrencies();
    }
    
    [HttpGet]
    [Route("GetAllHistory")]
    public async Task<List<HistoryDto>> GetHistory()
    {
        var isHistoryEnabled = await _fhService.IsFeatureEnabled("History");
        if (!isHistoryEnabled)
        {
            return new List<HistoryDto>();
        }
        return _service.GetAllHistory();
    }
    
    [HttpPost]
    [Route("CreateHistory")]
    public async Task<bool> PostHistory([FromBody] HistoryDto historyDto)
    {
        var logType = "Warning";
        
        MonitorService.Log.Warning(logType + ": This method will create a history :D");
        
        var isHistoryEnabled = await _fhService.IsFeatureEnabled("History");
        if (!isHistoryEnabled)
        {
            return false;
        }
        Console.Write(historyDto.Result);
        _service.AddHistory(historyDto);
        return true;
    }
}