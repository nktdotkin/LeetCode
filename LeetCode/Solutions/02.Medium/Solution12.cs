using System.Text;
using BenchmarkDotNet.Attributes;

// | Method            | Mean     | Error   | StdDev  | Allocated |
// |------------------ |---------:|--------:|--------:|----------:|
// | IntToRomanUpdated | 278.7 ns | 3.79 ns | 3.17 ns |     968 B |
// | IntToRomanSlow    | 363.6 ns | 3.21 ns | 2.85 ns |    1424 B |
[MemoryDiagnoser(false)]
public class Solution12
{
    [Benchmark]
    public string IntToRomanUpdated()
    {
        return IntToRomanUpdated(3749);
    }

    [Benchmark]
    public string IntToRomanSlow()
    {
        return IntToRomanSlow(3749);
    }

    private static readonly Dictionary<int, char> IntegerRomanMap = new()
        {
            {1, 'I'},
            {5, 'V'},
            {10, 'X'},
            {50, 'L'},
            {100, 'C'},
            {500, 'D'},
            {1000, 'M'},
        };

    public string IntToRomanUpdated(int num)
    {
        var stringBuilder = new StringBuilder();

        var placeValue = 1;

        while (num / placeValue >= 10)
        {
            placeValue *= 10;
        }

        while (placeValue > 0)
        {
            var digit = num / placeValue;

            while (digit > 0)
            {
                var decomposedNum = digit * placeValue;

                if (IntegerRomanMap.TryGetValue(decomposedNum, out var value))
                {
                    stringBuilder.Append(value);
                    break;
                }
                else if (digit != 4 && digit != 9)
                {
                    var closestKey = IntegerRomanMap.Keys.Last(k => k - decomposedNum < 0);
                    var remainder = decomposedNum - closestKey;

                    digit = remainder / placeValue;

                    stringBuilder.Append(IntegerRomanMap[closestKey]);
                }
                else
                {
                    var closestKey = IntegerRomanMap.Keys.First(k => k - decomposedNum > 0);
                    var remainder = closestKey - decomposedNum;

                    stringBuilder.Append(IntegerRomanMap[remainder]);
                    stringBuilder.Append(IntegerRomanMap[closestKey]);

                    break;
                }
            }

            num %= placeValue;
            placeValue /= 10;
        }

        var result = stringBuilder.ToString();
        return result;
    }

    public string IntToRomanSlow(int num)
    {
        var integerRomanMap = new Dictionary<int, char>()
        {
            {1, 'I'},
            {5, 'V'},
            {10, 'X'},
            {50, 'L'},
            {100, 'C'},
            {500, 'D'},
            {1000, 'M'},
        };

        void setRomanVariant(int digit, int placeValue, StringBuilder stringBuilder)
        {
            var decomposedNum = digit * placeValue;

            if (integerRomanMap.TryGetValue(decomposedNum, out var value))
            {
                stringBuilder.Append(value);
                return;
            }

            if (digit != 4 && digit != 9)
            {
                var closestKey = integerRomanMap.Keys.Last(k => k - decomposedNum < 0);
                var remainder = decomposedNum - closestKey;

                stringBuilder.Append(integerRomanMap[closestKey]);
                setRomanVariant(remainder / placeValue, placeValue, stringBuilder);
            }
            else
            {
                var closestKey = integerRomanMap.Keys.First(k => k - decomposedNum > 0);
                var remainder = closestKey - decomposedNum;

                setRomanVariant(remainder / placeValue, placeValue, stringBuilder);
                stringBuilder.Append(integerRomanMap[closestKey]);
            }

            return;
        }

        var stringBuilder = new StringBuilder();

        var placeValue = 1;

        while (num / placeValue >= 10)
        {
            placeValue *= 10;
        }

        while (placeValue > 0)
        {
            var digit = num / placeValue;

            if (digit > 0)
            {
                setRomanVariant(digit, placeValue, stringBuilder);
            }

            num %= placeValue;
            placeValue /= 10;
        }

        var result = stringBuilder.ToString();
        return result;
    }
}