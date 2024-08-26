using System.Diagnostics;
using BenchmarkDotNet.Running;

namespace LeetCode;

public static class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<Solution12>();
    }
}