using infrastructure;
using infrastructure.Models;

namespace service;

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

    public List<HistoryDto> GetAllHistory()
    {
        return _repo.GetAllHistory();
    }


}