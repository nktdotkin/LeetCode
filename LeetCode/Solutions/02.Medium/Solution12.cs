using System.Text;

public static class Solution12
{
    public static string IntToRoman(int num)
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