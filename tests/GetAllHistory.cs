using api;
using FluentAssertions;
using FluentAssertions.Execution;
using infrastructure;
using infrastructure.Models;
using service;

public class GetAllHistory
{
    
    private CurrencyRepo _currencyRepo;
    private CurrencyService _currencyService;
    private CurrencyController _currencyController;
    
    [SetUp]
    public void Setup()
    {
        // Arrange
        _currencyRepo = new CurrencyRepo(Utilities.MySqlConnectionString);
        _currencyService = new CurrencyService(_currencyRepo);
        _currencyController = new CurrencyController(_currencyService);
    }
    
    [Test]
    public void GetAllHistories_ShouldReturnListOfHistory()
    {
        // Act
        List<HistoryDto> responseList = _currencyController.GetHistory();


        // Assert
        using (new AssertionScope())
        {
            responseList.Should().NotBeNull("because a list of currencies is expected");
            responseList[0].Date.Should().NotBe(null, "because the last currency should be USD");
        }
    }
    
}
