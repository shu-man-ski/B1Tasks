using System;
using System.Text;

class RandomStringGenerator
{
    private static readonly Random random = new Random();

    private readonly string _latinSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    private readonly string _russianSymbols = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

    private uint _years = 5;

    private uint _linesNumber = 100;

    private uint _symbolsNumber = 10;

    private int _intNumberMinValue = 1;

    private int _intNumberMaxValue = 100_000_000;

    private double _doubleNumberMinValue = 1;

    private double _doubleNumberMaxValue = 20;

    public RandomStringGenerator() { }

    public RandomStringGenerator(uint linesNumber, uint years, uint symbolsNumber)
    {
        _linesNumber = linesNumber;
        _years = years;
        _symbolsNumber = symbolsNumber;
    }

    public RandomStringGenerator SetYears(uint years)
    {
        _years = years;
        return this;
    }

    public RandomStringGenerator SetLinesNumber(uint linesNumber)
    {
        _linesNumber = linesNumber;
        return this;
    }

    public RandomStringGenerator SetSymbolsNumber(uint symbolsNumber)
    {
        _symbolsNumber = symbolsNumber;
        return this;
    }

    public RandomStringGenerator SetIntNumberRange(int min, int max)
    {
        _intNumberMinValue = min;
        _intNumberMaxValue = max;
        return this;
    }

    public RandomStringGenerator SetDoubleNumberRange(double min, double max)
    {
        _doubleNumberMinValue = min;
        _doubleNumberMaxValue = max;
        return this;
    }

    // Метод для генерации строк
    public string GenerateRandomStrings()
    {
        var str = new StringBuilder();
        for (int i = 0; i < _linesNumber; i++)
        {
            str.Append(GenerateRandomString());
            if(i != _linesNumber-1)
            {
                str.Append("\n");
            }
        }
        return str.ToString();
    }

    // Метод для генерации одной строки
    private string GenerateRandomString()
    {
        var randomDate = GenerateRandomDateForLastYears((int)_years);
        var latinSymbols = GenerateRandomSymbolsStringBySet(_latinSymbols);
        var russianSymbols = GenerateRandomSymbolsStringBySet(_russianSymbols);
        var intNumber = GenerateRandomNumber(_intNumberMinValue, _intNumberMaxValue);
        var doubleNumber = GenerateRandomNumber(_doubleNumberMinValue, _doubleNumberMaxValue);
        return string.Format("{0}||{1}||{2}||{3}||{4}||", 
            randomDate.ToString("dd.MM.yyyy"),
            latinSymbols,
            russianSymbols,
            intNumber,
            doubleNumber.ToString("F8"));
    }

    // Метод для генерации случайной даты за определенное количество лет
    private DateTime GenerateRandomDateForLastYears(int years)
    {
        var today = DateTime.Today;
        var fiveYearsAgo = DateTime.Today.AddYears(-years);
        var days = (today - fiveYearsAgo).Days;
        int offset = random.Next(days);
        var randomDate = fiveYearsAgo.AddDays(offset);
        return randomDate;
    }

    // Метод для генерации случайных символов из множества
    private string GenerateRandomSymbolsStringBySet(string set)
    {
        var str = new StringBuilder();
        for (var i = 0; i < _symbolsNumber; i++)
        {
            char randomChar = set[random.Next(set.Length)];
            str.Append(randomChar);
        }
        return str.ToString();
    }

    // Метод для генерации случайного целого числа в диапазоне от min до max
    private int GenerateRandomNumber(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    // Метод для генерации случайного числа с плавающей точкой в диапазоне от min до max
    private double GenerateRandomNumber(double min, double max)
    {
        return random.NextDouble() * (max - min) + min; ;
    }
}