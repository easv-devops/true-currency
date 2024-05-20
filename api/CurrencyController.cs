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
    
    public CurrencyController(CurrencyService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Route("GetAll")]
    public List<Currency> Get()
    {
        var logType = "Warning";
        
        MonitorService.Log.Warning(logType + ": This method will return all currencies :D");
        return _service.GetAllCurrencies();
    }
    
    [HttpGet]
    [Route("GetAllHistory")]
    public List<HistoryDto> GetHistory()
    {
        return _service.GetAllHistory();
    }
    
    [HttpPost]
    [Route("CreateHistory")]
    public bool PostHistory([FromBody] HistoryDto historyDto)
    {
        Console.Write(historyDto.Result);
        _service.AddHistory(historyDto);
        return true;
    }
}