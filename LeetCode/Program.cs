using BenchmarkDotNet.Running;

namespace LeetCode;

public static class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<Solution747>();
        BenchmarkRunner.Run<Solution767>();
        BenchmarkRunner.Run<Solution1405>();
    }
}