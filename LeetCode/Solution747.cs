using BenchmarkDotNet.Attributes;

namespace LeetCode;

// | Method               | Mean      | Error     | StdDev    |
// |--------------------- |----------:|----------:|----------:|
// | DominantIndexFastest |  6.284 ns | 0.0838 ns | 0.0700 ns |
// | DominantIndexFast    | 35.177 ns | 0.2808 ns | 0.2627 ns |
// | DominantIndexSlow    | 39.939 ns | 0.2486 ns | 0.2203 ns |
public class Solution747
{
    private readonly int[] _nums = new[] { 3, 6, 1, 0 };

    [Benchmark]
    public int DominantIndexFastest()
    {
        var largest = -1;
        var largestIndex = -1;

        for (var i = 0; i < _nums.Length; i++)
        {
            if (_nums[i] <= largest)
            {
                continue;
            }

            largest = _nums[i];
            largestIndex = i;
        }

        for (var i = 0; i < _nums.Length; i++)
        {
            if (_nums[i] == largest)
            {
                continue;
            }

            if (_nums[i] * 2 > largest)
            {
                return -1;
            }
        }

        return largestIndex;
    }

    [Benchmark]
    public int DominantIndexFast()
    {
        var largest = -1;
        var largestIndex = -1;

        for (var i = 0; i < _nums.Length; i++)
        {
            if (_nums[i] <= largest)
            {
                continue;
            }

            largest = _nums[i];
            largestIndex = i;
        }

        if (_nums.Where(n => n != largest).All(n => n * 2 <= largest))
        {
            return largestIndex;
        }

        return -1;
    }

    [Benchmark]
    public int DominantIndexSlow()
    {
        var largest = _nums.Max();

        if (_nums.Where(n => n != largest).All(n => n * 2 <= largest))
        {
            var index = Array.FindIndex(_nums, n => n == largest);
            return index;
        }

        return -1;
    }
}