using Infrastructure;
using infrastructure.Models;

namespace Service;

public class CurrencyService
{

    private CurrencyRepo _repo;
    
    public CurrencyService(CurrencyRepo repo)
    {
        _repo = repo;
    }
    
    
    public List<Currency> GetAllCurrencies()
    {
        return _repo.GetAllCurrencies();
    }
    
}