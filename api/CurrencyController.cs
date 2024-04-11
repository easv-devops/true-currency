using infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api;

[ApiController]
[Route("[controller]")]
public class CurrencyController: ControllerBase
{

    private CurrencyService _service;
    
    public CurrencyController(CurrencyService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Route("currency")]
    public List<Currency> Get()
    {
        return _service.GetAllCurrencies();
    }
}