﻿using api;
using FluentAssertions;
using FluentAssertions.Execution;
using infrastructure;
using infrastructure.Models;
using service;

namespace tests;

public class CreateHistory
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
         _currencyService.AddHistory(new HistoryDto
         {
             Date = DateTime.Now,
             Source = "EUR",
             Target = "EUR",
             Value = 50,
             Result = 1000
         });

         var responseList = _currencyService.GetAllHistory();


        // Assert
        using (new AssertionScope())
        {
            responseList.Should().NotBeNull("because a list of currencies is expected");
            responseList[0].Date.Should().NotBe(null, "It was added just above");
            responseList[responseList.Count - 1].Result.Should().Be(1000, "Because the latest result is set to 1000");
        }
    }
}