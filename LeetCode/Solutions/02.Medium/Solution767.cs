using System.Text;
using BenchmarkDotNet.Attributes;

namespace LeetCode;

// | Method                            | Mean       | Error    | StdDev   |
// |---------------------------------- |-----------:|---------:|---------:|
// | ReorganizeStringBenchMarkSlow1    |   536.3 ns |  4.93 ns |  4.61 ns |
// | ReorganizeStringBenchMarkSlow2    | 1,391.5 ns | 11.34 ns | 10.61 ns |
// | ReorganizeStringSortedSet1        |   258.0 ns |  3.20 ns |  2.83 ns |
// | ReorganizeStringSortedSet2        |   877.2 ns |  1.09 ns |  0.91 ns |
// | ReorganizeStringSortedDictionary1 |   254.6 ns |  1.74 ns |  1.63 ns |
// | ReorganizeStringSortedDictionary2 |   854.2 ns |  7.16 ns |  6.70 ns |
public class Solution767
{
    [Benchmark]
    public string ReorganizeStringBenchMarkSlow1()
    {
        return ReorganizeStringSlow("aaabcbc");
    }

    [Benchmark]
    public string ReorganizeStringBenchMarkSlow2()
    {
        return ReorganizeStringSlow("aaabvsalksjvmcbc");
    }

    [Benchmark]
    public string ReorganizeStringSortedSet1()
    {
        return ReorganizeStringSortedSet("aaabcbc");
    }

    [Benchmark]
    public string ReorganizeStringSortedSet2()
    {
        return ReorganizeStringSortedSet("aaabvsalksjvmcbc");
    }

    [Benchmark]
    public string ReorganizeStringSortedDictionary1()
    {
        return ReorganizeStringSortedSet("aaabcbc");
    }

    [Benchmark]
    public string ReorganizeStringSortedDictionary2()
    {
        return ReorganizeStringSortedSet("aaabvsalksjvmcbc");
    }

    public static string ReorganizeStringSortedDictionary(string s)
    {
        var charDictionary = new SortedDictionary<char, int>();

        foreach (var c in s)
        {
            charDictionary.TryAdd(c, 0);

            charDictionary[c]++;
        }

        var sb = new StringBuilder();

        while (charDictionary.Count > 1)
        {
            var first = charDictionary.Max();
            charDictionary.Remove(first.Key);

            var next = charDictionary.Max();
            charDictionary.Remove(next.Key);

            sb.Append(first.Key);
            sb.Append(next.Key);

            if (first.Value - 1 > 0)
            {
                charDictionary.Add(first.Key, first.Value - 1);
            }

            if (next.Value - 1 > 0)
            {
                charDictionary.Add(next.Key, next.Value - 1);
            }
        }

        if (charDictionary.Count > 0)
        {
            var item = charDictionary.Max();

            if (item.Value > 1)
            {
                return string.Empty;
            }

            sb.Append(item.Key);
        }

        return sb.ToString();
    }

    public static string ReorganizeStringSortedSet(string s)
    {
        var charDictionary = new Dictionary<char, int>();

        foreach (var c in s)
        {
            charDictionary.TryAdd(c, 0);

            charDictionary[c]++;
        }

        var sorted = new SortedSet<(int, char)>();

        foreach (var kvp in charDictionary)
        {
            sorted.Add((kvp.Value, kvp.Key));
        }

        var sb = new StringBuilder();

        while (sorted.Count > 1)
        {
            var first = sorted.Max;
            sorted.Remove(first);

            var next = sorted.Max;
            sorted.Remove(next);

            sb.Append(first.Item2);
            sb.Append(next.Item2);

            if (--first.Item1 > 0)
            {
                sorted.Add(first);
            }

            if (--next.Item1 > 0)
            {
                sorted.Add(next);
            }
        }

        if (sorted.Count > 0)
        {
            var item = sorted.Max;

            if (item.Item1 > 1)
            {
                return string.Empty;
            }

            sb.Append(item.Item2);
        }

        return sb.ToString();
    }

    public static string ReorganizeStringSlow(string s)
    {
        var asSpan = s.AsSpan();
        var charDictionary = new Dictionary<char, int>();

        for (var i = 0; i < asSpan.Length; i++)
        {
            if (charDictionary.ContainsKey(asSpan[i]))
            {
                charDictionary[asSpan[i]]++;
                continue;
            }

            charDictionary.Add(asSpan[i], 1);
        }

        var sorted = charDictionary
            .OrderByDescending(kvp => kvp.Value)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        var sb = new StringBuilder();

        var index = 0;

        while (sorted.Values.Sum(v => v) > 0)
        {
            if (sorted.Count == 1 && sorted.First().Value > 1)
            {
                return string.Empty;
            }

            var fist = sorted.First().Key;

            sb.Append(fist);
            Decrease(fist);

            if (sorted.Count <= 1)
            {
                continue;
            }

            if (index + 1 >= sorted.Count)
            {
                index = 0;
            }

            var next = sorted.ElementAt(++index).Key;

            sb.Append(next);
            Decrease(next);
        }

        return sb.ToString();

        void Decrease(char key)
        {
            if (!sorted.TryGetValue(key, out var value))
            {
                return;
            }

            if (value - 1 == 0)
            {
                sorted.Remove(key);
                return;
            }

            sorted[key] = value - 1;
        }
    }
}