using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyRates.Helpers;

namespace CurrencyRates
{
  public class RateList
  {
    private readonly string _dataDirectory;
    private readonly List<string> _ratesFilesList;
    
    private void SymbolNotFoundError(string symbol) => throw new ArgumentException($"Symbol '{symbol}' not found");
    
    private Dictionary<string,double> Reader(string filesName) => JSONReader.GetRatesInCurrencyFile(_dataDirectory, filesName);
    
    public RateList(string dataDirectory)
    {
      _dataDirectory = dataDirectory;
      _ratesFilesList = JSONReader.GetRatesFiles(dataDirectory);
    }

    public double GetRateAtDate(string symbol, string date)
    {
      var filesNameOnDate =
        from fileName in _ratesFilesList
        where fileName.Contains(date)
        where Reader(fileName).ContainsKey(symbol)
        select Reader(fileName)[symbol];
      
      if (!filesNameOnDate.Any()) SymbolNotFoundError(symbol);
      
      return filesNameOnDate.First();
    }

    public double GetRateDifference(string symbol, string startDate, string endDate)
    {
      return GetRateAtDate(symbol, endDate) - GetRateAtDate(symbol, startDate);
    }
    
    public string[] GetRatesForYear(string symbol, string year)
    {
      var ratesForYearInSymbol =
        from fileName in _ratesFilesList
        where fileName.Contains(year)
        where Reader(fileName).ContainsKey(symbol)
        orderby fileName ascending
        let rates = "Date: " + fileName.Split(".json")[0] + " Rate: " +Reader(fileName)[symbol]
        select rates;
      
      if(!ratesForYearInSymbol.Any()) throw new ArgumentException($"No rates for '{symbol}' found in year '{year}'");
      
      return ratesForYearInSymbol.ToArray();;
    }

    public double HighestRateEver(string symbol)
    {
      var highestRateEver =
        from fileName in _ratesFilesList
        where Reader(fileName).ContainsKey(symbol)
        orderby Reader(fileName)[symbol] descending
        select Reader(fileName)[symbol];

      if (!highestRateEver.Any()) SymbolNotFoundError(symbol);
      
      return highestRateEver.First();
    }
  }
}
