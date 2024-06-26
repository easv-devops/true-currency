﻿using api;
using FluentAssertions;
using FluentAssertions.Execution;
using infrastructure;
using infrastructure.Models;
using service;

namespace tests
{
    public class GetAllCurrencies
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
        public void GetAllCurrencies_ShouldReturnListOfCurrencies()
        {
            // Act
            List<Currency> responseList = _currencyService.GetAllCurrencies();

            // Print out the full list
            Console.WriteLine("Full list of currencies:");
            foreach (var currency in responseList)
            {
                Console.WriteLine($"ISO: {currency.Iso}, Rate to USD: {currency.RateToUsd}");
            }

            // Assert
            using (new AssertionScope())
            {
                responseList.Should().NotBeNull("because a list of currencies is expected");
                responseList.Should().HaveCount(5, "because there should be 5 currencies returned");
                responseList[4].Iso.Should().Be("USD", "because the last currency should be USD");
                responseList[4].RateToUsd.Should().Be(1, "because USD rate should be 1");
            }
        }
    }
}