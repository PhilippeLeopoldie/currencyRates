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
      return jsonFileFormat.Rates;// key<string> , value<double>
    }

    public static List<string> GetRatesFiles(string dataDirectory)//list of only files names
    {
      var files =
        from file in Directory.GetFiles(dataDirectory)//files full path name
        select Path.GetFileName(file);//  only file name
      return files.ToList<string>();
    }
  }
}