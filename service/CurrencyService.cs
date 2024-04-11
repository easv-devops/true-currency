using Infrastructure;
using infrastructure.Models;

namespace Service;

public class CurrencyService
{

    private readonly CurrencyRepo _repo;
    
    public CurrencyService(CurrencyRepo repo)
    {
        _repo = repo;
    }
    
    
    public List<Currency> GetAllCurrencies()
    {
        return _repo.GetAllCurrencies();
    }
    
}