using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    public class Calculator
    {
        private readonly List<string> _defaultDelimiters = new List<string> { ",", "\n" };
        private const int StartIndexOfNumbersWithCustomDelimiter = 3;
        private const int MaxNumber = 1000;
        private const string StartDelimiter = "//";
        public int Add(string numbers)
        {

            if (string.IsNullOrEmpty(numbers)) return 0;


            if (numbers.StartsWith(StartDelimiter))
            {
                numbers = GetNumbersWithoutDelimiters(numbers);
            }

            var sumOfNumbers = GetSumOfNumbers(numbers);
            return sumOfNumbers;
        }
            
          
        private int GetSumOfNumbers(string numbers)
        {
            var parsedNumbers = numbers.Split(_defaultDelimiters.ToArray(), StringSplitOptions.None).Select(int.Parse).ToList();
            CheckForNegativeNumbers(parsedNumbers);
            var sumOfNumbers = parsedNumbers.Where(n => n < MaxNumber).Sum();
            return sumOfNumbers;
        }

        private void CheckForNegativeNumbers(List<int> parsedNumbers)
        {
            if (!parsedNumbers.Any(n => n < 0)) return;
            var negativeNumbers = string.Join(',', parsedNumbers.Where(p => p < 0).Select(p=>p.ToString()).ToArray());
            throw new ArgumentException($"negatives not allowed {negativeNumbers}");
        }

        private string GetNumbersWithoutDelimiters(string numbers)
        {
            var numbersStringStartIndex = GetStartIndexOfNumbersString(numbers);
            var numbersWithoutDelimiters = numbers.Substring(numbersStringStartIndex);
            return numbersWithoutDelimiters;
        }

        private int GetStartIndexOfNumbersString(string numbers)
        {
            var customDelimiters = GetCustomDelimiters(numbers);
            _defaultDelimiters.AddRange(customDelimiters);
            var hasMultipleDelimiters = customDelimiters.Count > 1 || numbers.Contains('[');
            var multipleDelimiterLength = hasMultipleDelimiters ? (customDelimiters.Count * 2) : 0;

            return StartIndexOfNumbersWithCustomDelimiter + customDelimiters.Sum(x => x.Length) + multipleDelimiterLength;
           
        }

        private List<string> GetCustomDelimiters(string numbers)
        {
            var allDelimiters = numbers.Substring(StartDelimiter.Length, numbers.IndexOf('\n') - StartDelimiter.Length);
            var splitedDelimiters = allDelimiters.Split('[').Select(d => d.TrimEnd(']')).ToList();
            if (splitedDelimiters.Contains(string.Empty))
            {
                splitedDelimiters.Remove(string.Empty);
            }
            return splitedDelimiters;
        }
    }
}
