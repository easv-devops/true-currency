using api;
using FluentAssertions;
using FluentAssertions.Execution;
using infrastructure;
using infrastructure.Models;
using service;

namespace tests;

public class GetAllHistory
{
    
    private CurrencyRepo _currencyRepo;
    private CurrencyService _currencyService;
    
    [SetUp]
    public void Setup()
    {
        // Arrange
        _currencyRepo = new CurrencyRepo(Utilities.MySqlConnectionString);
        _currencyService = new CurrencyService(_currencyRepo);
    }
    
    [Test]
    public void GetAllHistories_ShouldReturnListOfHistory()
    {
        // Act
        List<HistoryDto> responseList = _currencyService.GetAllHistory();


        // Assert
        using (new AssertionScope())
        {
            responseList.Should().NotBeNull("because a list of currencies is expected");
            responseList[0].Date.Should().NotBe(null, "because the last currency should be USD");
        }
    }
    
}
