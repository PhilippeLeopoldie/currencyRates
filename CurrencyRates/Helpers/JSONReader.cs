using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace CurrencyRates.Helpers
{
  public static class JSONReader
  {

    private class RatesFormat
    {
      public Dictionary<string, double> Rates { get; set; }
    }

    public static Dictionary<string, double> GetRatesInCurrencyFile(string dataDirectory, string fileName)
    {
      var path = Path.Join(dataDirectory, fileName);
      var json = File.ReadAllText(path);
      var jsonFileFormat = JsonConvert.DeserializeObject<RatesFormat>(json);
      return jsonFileFormat.Rates;
    }

    public static List<string> GetRatesFiles(string dataDirectory)
    {
      var files =
        from file in Directory.GetFiles(dataDirectory)
        select Path.GetFileName(file);
      return files.ToList<string>();
    }
  }
}