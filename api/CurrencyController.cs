using infrastructure.Models;
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
            return [];
        }
        return _service.GetAllHistory();
    }
    
    [HttpPost]
    [Route("CreateHistory")]
    public async Task<bool> PostHistory([FromBody] HistoryDto historyDto)
    {
        var isHistoryEnabled = await _fhService.IsFeatureEnabled("History");
        if (!isHistoryEnabled)
        {
            return false; //TODO: Errorhandling
        }
        Console.Write(historyDto.Result);
        _service.AddHistory(historyDto);
        return true;
    }
}