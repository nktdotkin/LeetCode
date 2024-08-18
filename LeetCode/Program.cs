using System.Diagnostics;
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

        //         Solution212.FindWordsAcceptedFast(
        // [['o', 'a', 'b', 'n'], ['o', 't', 'a', 'e'], ['a', 'h', 'k', 'r'], ['a', 'f', 'l', 'v']],
        // ["oa", "oaa"]
        //         );
    }
}