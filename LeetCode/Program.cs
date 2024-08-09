using BenchmarkDotNet.Running;

namespace LeetCode;

public static class Program
{
    public static void Main()
    {
        // BenchmarkRunner.Run<Solution747>();
        // BenchmarkRunner.Run<Solution767>();
        // BenchmarkRunner.Run<Solution1405>();
        BenchmarkRunner.Run<Solution212>();
        // Solution212.FindWordsSlow(
        //     [['o', 'a', 'a', 'n'], ['e', 't', 'a', 'e'], ['i', 'h', 'k', 'r'], ['i', 'f', 'l', 'v']],
        //      ["oath", "pea", "eat", "rain"]);
    }
}