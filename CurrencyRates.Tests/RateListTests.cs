using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace CurrencyRates.Tests
{

  public class RateListTests
  {
    private readonly string _dataDirectory = $"{Environment.CurrentDirectory}../../../../../data";
    private readonly RateList _rateList;

    public RateListTests()
    {
      _rateList = new RateList(_dataDirectory);
    }

    private bool methodExists(string methodName) =>
      typeof(RateList).GetMethod(methodName) != null;

    [Fact]
    public void solution_has_all_methods()
    {
      methodExists("GetRateAtDate").Should().BeTrue();
      methodExists("GetRateDifference").Should().BeTrue();
      methodExists("GetRatesForYear").Should().BeTrue();
      methodExists("HighestRateEver").Should().BeTrue();
    }

    [Fact]
    public void RateList_should_accept_dataDirectory_in_constructor()
    {
      // act
      var rateList = new RateList(_dataDirectory);

      // assert
      rateList.Should().NotBe(null);
    }

    [Theory]
    [InlineData("EUR", "1999-01-01", 0.853515)]
    [InlineData("GBP", "1999-01-01", 0.602941)]
    public void GetRateAtDate_should_get_rate_for_a_date(string symbol, string date, double expected)
    {
      // act
      var result = _rateList.GetRateAtDate(symbol, date);

      // assert
      result.Should().Be(expected);
    }

    [Fact]
    public void GetRateAtDate_should_throw_for_nonexisting_symbol()
    {
      // Act
      Action act = () => _rateList.GetRateAtDate("APA", "1999-01-01");

      // Assert
      act
        .Should()
        .Throw<ArgumentException>()
        .WithMessage("Symbol 'APA' not found");
    }

    [Fact]
    public void GetRateAtDate_should_throw_for_nonexisting_symbol_at_date()
    {
      // Act
      Action act = () => _rateList.GetRateAtDate("IDR", "1999-01-01");

      // Assert
      act
        .Should()
        .Throw<ArgumentException>()
        .WithMessage("Symbol 'IDR' not found");
    }
    
    [Fact]
    public void GetRateAtDate_should_throw_for_nonexisting_date()
    {
      // Act
      Action act = () => _rateList.GetRateAtDate("EUR", "VeryWierdDate");

      // Assert
      act
        .Should()
        .Throw<ArgumentException>()
        .WithMessage("Symbol 'EUR' not found");
    }

    // Uncomment the tests belows to get a starting point for implementing the rest of the code
    // Note that you should test with more data than the examples supplied

    [Theory]
    [InlineData("EUR", "1999-01-01", "1999-06-01", 0.10388599999999992)]
    public void GetRateDifference_should_get_rate_difference(string symbol, string startDate, string endDate, double expected)
    {
      // Act
      var changeForEur = _rateList.GetRateDifference(symbol, startDate, endDate);

      // Assert
      changeForEur.Should().Be(expected);
    }

    [Fact]
    public void GetRateDifference_should_throw_for_non_existing_symbol()
    {
      // Act
      Action act = () => _rateList.GetRateDifference("APA", "1999-01-01", "2021-02-01");

      // Assert
      act
        .Should()
        .Throw<ArgumentException>()
        .WithMessage("Symbol 'APA' not found");
    }

    [Fact]
    public void GetRateDifference_should_throw_for_non_existing_symbol_at_date()
    {
      // Act
      Action act = () => _rateList.GetRateDifference("IDR", "1999-01-01", "2021-02-01");

      // Assert
      act
        .Should()
        .Throw<ArgumentException>()
        .WithMessage("Symbol 'IDR' not found");
    }

    [Theory]
    [InlineData("EUR", "1999", 3, 0.853515, 0.951173)]
    public void RatesForYear_should_get_rates_for_year(string symbol, string year, int expectedCount, double expectedFirstRate, double expectedLastRate)
    {
      // Act
      var ratesForYear = _rateList.GetRatesForYear(symbol, year);

      // Assert
      ratesForYear.Length.Should().Be(expectedCount);
      ratesForYear.Any(r =>
        r.Contains($"Rate: {expectedFirstRate}") &&
        r.Contains($"Date: {year}"))
        .Should().BeTrue();
      ratesForYear.Any(r =>
        r.Contains($"Rate: {expectedLastRate}") &&
        r.Contains($"Date: {year}"))
        .Should().BeTrue();
    }
    [Theory]
    [InlineData("EUR", "2016", 3, 0.918241, 0.93775)]
    [InlineData("IDR", "1999", 2, 8084.497809, 6758.724973  )]
    public void RatesForYear_should_get_rates_for_year_sorted(string symbol, string year, int expectedCount, double expectedFirstRate, double expectedLastRate)
    {
      // Act
      var ratesForYear = _rateList.GetRatesForYear(symbol, year);

      // Assert
      ratesForYear.Length.Should().Be(expectedCount);
      ratesForYear[0].Should().Contain($"Rate: {expectedFirstRate}");
      ratesForYear[ratesForYear.Length - 1].Should().Contain($"Rate: {expectedLastRate}");
    }

    [Fact]
    public void GetRatesForYear_should_throw_for_non_existing_symbol()
    {
      // Act
      Action act = () => _rateList.GetRatesForYear("APA", "1999");

      // Assert
      act
        .Should()
        .Throw<ArgumentException>()
        .WithMessage("No rates for 'APA' found in year '1999'");
    }

    [Theory]
    [InlineData("EUR", 1.178323)]
    [InlineData("GBP", 0.807676)]
    [InlineData("IDR", 16030.4)]
    [InlineData("SEK", 10.866684)]
    public void HighestRateEver_should_return_highest_rate(string symbol, double expectedHighestRate)
    {
      // Act
      var highestEver = _rateList.HighestRateEver(symbol);

      // Assert
      highestEver.Should().Be(expectedHighestRate);
    }

    [Fact]
    public void HighestRateEver_should_throw_for_non_existing_symbol()
    {
      // Act
      Action act = () => _rateList.HighestRateEver("APA");

      // Assert
      act
        .Should()
        .Throw<ArgumentException>()
        .WithMessage("Symbol 'APA' not found");
    }
  }

}