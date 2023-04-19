# ASSIGNMENT: Currency rates finder

In this exercise you will build a couple of methods (in other words an API) that lets the user find, list and compare currencies over time.

In order to solve this problem you will have to:

- Implement the methods in the class `RateList`
- create a suitable object structure, that helps you solve the problem
  - it is in other words a good idea to read the entire instruction before you start...
- iterate, filter and loop through the collections using LINQ, to solve the requirements

## Specification

- Implement a `GetRateAtDate` method, that

  - Takes a `symbol`-string and a `date`-string
    - You can assume the `date`-string to be in the `YYYY-MM-DD` format
  - Return the currency rate for that symbol at the date
    - The rate should be of type `double`
  - Throws an `ArgumentException` with the message `Symbol 'IDR' not found`, when a symbol (where `IDR` is an example) is not found among the rates for the given date

- Implement a `GetRateDifference` method that

  - Takes a `symbol`-string, a `startDate`-string and an `endDate`-string
  - returns the difference in rate between the rate at `startDate`, and the rate at `endDate`
    - subtract the `endDate - startDate`
    - only subtract the values - no fancy math needed
    - the difference should be returned as a `double`
  - Throws an `ArgumentException` with the message `Symbol 'FAKE' not found`, when a symbol (where `FAKE` is an example) is not found among the rates for the given date

- Implement a `GetRatesForYear` method that

  - Takes a `symbol`-string and a `year`-string
    - The `year`-string is in the `YYYY`-format
  - Returns an array of the rates (as a `string[]`) for that year

    - Each row in the array should have the format `"Date: <date> Rate: <rate>`, where `date` is a string (YYYY-MM-DD) and rate is a `double`
    - Sort the array in ascending order per date, so that the earliest date comes first and the latest date last in the list
    - For example `GetRatesForYear("GBP", "1999")` should return

      ```c#
      [
        "Date: 1999-01-01 Rate: 0.602941",
        "Date: 1999-06-01 Rate: 0.621195",
        "Date: 1999-11-01 Rate: 0.60824",
      ]
      ```

  - Throw an `ArgumentException` with the message `No rates for 'APA' found in year '1999'"`, when a symbol (where `APA` is an example, and `1999` is also an example) is not found in any of the dates for the current year.
    - That is; if all of the files for a year doesn't contain the symbol then throw the error.
    - If only one file doesn't contain the symbol, only return the other two

- Implement a `HighestRateEver` method that:
  - Takes a `symbol`-string
  - returns the highest rate for any date
  - Throws an `ArgumentException` with the message `Symbol 'BANAN' not found`, when a symbol (where `BANAN` is an example) is not found anywhere.

## Some help

### Reading the rate data files

The data is located in files in the `data` directory, and we have supplied you with two helper methods to read that data:

1. `CurrencyRates.Helpers.JSONReader.GetRateFiles` that returns a `List<string>` of all the files in the `data`-directory
2. `CurrencyRates.Helpers.JSONReader.GetRatesInCurrencyFile` that returns the `rates`-values from a file as a `Dictionary<string, double>`

The `CurrencyRates.Helpers.JSONReader` is a static class and you can call the methods like this:

```csharp
List<string> files = JSONReader.GetRatesFiles(dataDirectory);
Dictionary<string, double> rates = JSONReader.GetRatesFiles(dataDirectory, "1999-01-01.json");
```

We have created two tests that shows how these methods can be used.

The `RateList` constructor should take one argument, which is the path to the `data`-directory. We have created a first test for this, to get you started.

### Tackling the problem

The problem has two parts:

1. Read the data from the files into a suitable structure to answer the questions. This should be done in the constructor of `RateList`, using `JSONReader` and the `datadirectory` parameter
2. Implement the methods according to the specifications using Linq-statements

## Get started

Clone this repository:

```bash
git clone git@github.com:saltSthlm/dnfs-winter23-test-currencyRates.git
cd dnfs-winter23-test-currencyRates
```

Then restore the projects dependencies

```bash
dotnet restore
```

And then to run the tests

```bash
dotnet test
```

This will run the first few tests that will help you get started, with the first few requirements.

We have supplied more tests that you can use to verify your code, but left it commented out so that your code will compile. You will need to uncomment these test to implement the rest of the methods for the tests.

You should also consider adding more test cases, than the examples we have added.

## Evaluation

Evaluation will be done by:

- running our own test suite (not supplied, that validates the specification above with more cases) against your code.
- looking through the code and making sure that it is easy to understand and well written
- ensuring that you have used LINQ and Collections to your advantage

## Handing in the solution

You will probably create more than one class, but keep all the classes in the `Solution.cs`-file, to make our automatic correction tool work.

Upload the `Solution.cs`-file in a Google Drive folder called `currencyRates`.

# FAQ

Should we also submit our test files?

> Nope.

Should we catch the errors?

> No... but you need to throw some, according to the specification.

Will I need to write my own tests?

> Yes. The tests we have supplied is just a starter template.

The files are wrong, i want it to be structured different (or correct the exchange rates)

> You should not change the data files... We will use the same data files for correcting the tests. You change - you fail.
