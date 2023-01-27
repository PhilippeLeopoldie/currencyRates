using System;
using CurrencyRates.Helpers;
using FluentAssertions;
using Xunit;

namespace CurrencyRates.Tests
{
  public class JSONReaderTests
  {
    private string _dataDirectory = $"{Environment.CurrentDirectory}../../../../../data";

    [Fact]
    public void should_read_first_file()
    {
      // act
      var rates = JSONReader.GetRatesInCurrencyFile(_dataDirectory, "1999-01-01.json");

      // assert
      rates.Count.Should().Be(3);
      rates["EUR"].Should().Be(0.853515);
      rates["GBP"].Should().Be(0.602941);
      rates["SEK"].Should().Be(8.117873);
    }

    [Fact]
    public void should_get_rates_files()
    {
      // act
      var rateFiles = JSONReader.GetRatesFiles(_dataDirectory);

      // assert
      rateFiles.Count.Should().Be(54);
    }
  }
}
