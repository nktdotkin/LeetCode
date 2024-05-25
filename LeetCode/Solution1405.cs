using System.Text;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace LeetCode;

// | Method                        | Mean       | Error     | StdDev    |
// |------------------------------ |-----------:|----------:|----------:|
// | Benchmark_Short_Accepted_Slow | 1,479.7 ns |  29.15 ns |  32.40 ns |
// | Benchmark_Long_Accepted_Slow  | 9,444.6 ns | 143.32 ns | 134.06 ns |
// | Benchmark_Short_Accepted_Fast |   633.0 ns |   4.18 ns |   3.91 ns |
// | Benchmark_Long_Accepted_Fast  | 2,479.6 ns |  17.03 ns |  14.22 ns |
// | Benchmark_Short_ChatGtp       |   617.8 ns |   2.47 ns |   2.31 ns |
// | Benchmark_Long_ChatGpt        | 2,564.0 ns |   5.15 ns |   4.57 ns |
public class Solution1405
{
    [Benchmark]
    public string Benchmark_Short_Accepted_Slow()
    {
        return LongestDiverseString_Accepted_Slow(1, 1, 7);
    }

    [Benchmark]
    public string Benchmark_Long_Accepted_Slow()
    {
        return LongestDiverseString_Accepted_Slow(23, 17, 28);
    }

    [Benchmark]
    public string Benchmark_Short_Accepted_Fast()
    {
        return LongestDiverseString_Accepted_Fast(1, 1, 7);
    }

    [Benchmark]
    public string Benchmark_Long_Accepted_Fast()
    {
        return LongestDiverseString_Accepted_Fast(23, 17, 28);
    }

    [Benchmark]
    public string Benchmark_Short_ChatGtp()
    {
        return LongestDiverseString_Accepted_Fast(1, 1, 7);
    }

    [Benchmark]
    public string Benchmark_Long_ChatGpt()
    {
        return LongestDiverseString_Accepted_Fast(23, 17, 28);
    }

    public string LongestDiverseString_ChatGpt(int a, int b, int c)
    {
        const int maxContinuousSequenceCount = 2;

        var items = new List<(char Char, int Count)>
    {
        ('a', a), ('b', b), ('c', c)
    };

        var sb = new StringBuilder();

        while (true)
        {
            items.Sort((x, y) => y.Count.CompareTo(x.Count));
            bool appended = false;

            for (int i = 0; i < items.Count; i++)
            {
                var (charToAdd, count) = items[i];
                if (count == 0)
                {
                    break;
                }

                var len = sb.Length;
                if (len >= maxContinuousSequenceCount &&
                    sb[len - 1] == charToAdd &&
                    sb[len - 2] == charToAdd)
                {
                    continue;
                }

                sb.Append(charToAdd);
                items[i] = (charToAdd, count - 1);
                appended = true;
                break;
            }

            if (!appended)
            {
                break;
            }
        }

        return sb.ToString();
    }

    public string LongestDiverseString_Accepted_Fast(int a, int b, int c)
    {
        const int maxContinuousSequenceCount = 2;

        var items = new Dictionary<char, int>()
        {
            {'a', a}, {'b', b}, {'c', c}
        };

        var sb = new StringBuilder();

        while (items.Any(i => i.Value > 0))
        {
            var item = items.MaxBy(i => i.Value);

            var isItemOutOfSequenceCount =
            sb.Length >= maxContinuousSequenceCount &&
            sb[^maxContinuousSequenceCount] == item.Key &&
            sb[sb.Length - maxContinuousSequenceCount + 1] == item.Key;

            if (!isItemOutOfSequenceCount)
            {
                Append(item.Key, item.Value, maxContinuousSequenceCount);
                continue;
            }

            var nextItems = items.Where(i => i.Key != item.Key && i.Value > 0);

            if (isItemOutOfSequenceCount && nextItems.Any())
            {
                var nextItem = nextItems.First();
                Append(nextItem.Key, nextItem.Value, 1);
                continue;
            }

            break;
        }

        void Append(char key, int value, int toAppend)
        {
            if (value <= 0)
            {
                return;
            }

            toAppend = value < toAppend ? value : toAppend;

            sb.Append(new string(key, toAppend));
            items[key] = value - toAppend;
        }

        return sb.ToString();
    }

    public string LongestDiverseString_Accepted_Slow(int a, int b, int c)
    {
        const int maxContiniousSequenceCount = 2;

        var items = new Dictionary<char, int>()
        {
            {'a', a}, {'b', b}, {'c', c}
        };

        var sb = new StringBuilder();

        while (items.Any(i => i.Value > 0))
        {
            foreach (var item in items.OrderByDescending(i => i.Value))
            {
                if (items.All(i => i.Value == 0))
                {
                    break;
                }

                // We want to stop adding item if it's out of max sequence count and there are other items which can be added
                var isItemOutOfSequenceCount =
                sb.Length >= maxContiniousSequenceCount &&
                sb[^maxContiniousSequenceCount] == item.Key &&
                sb[sb.Length - maxContiniousSequenceCount + 1] == item.Key;

                var otherItems = items.Where(i => i.Key != item.Key);

                if (isItemOutOfSequenceCount && otherItems.Any(i => i.Value > 0))
                {
                    var nextItem = otherItems.Where(i => i.Value > 0).First();
                    Append(nextItem.Key, nextItem.Value, 1);
                }

                Append(item.Key, item.Value, maxContiniousSequenceCount);
                break;
            }
        }

        void Append(char key, int value, int toAppend)
        {
            if (value <= 0)
            {
                return;
            }

            toAppend = value < toAppend ? value : toAppend;

            sb.Append(new string(key, toAppend));
            items[key] = value - toAppend;
        }

        var result = sb.ToString();

        return Regex.Replace(result, @"(.)\1\1+", "$1$1");
    }
}