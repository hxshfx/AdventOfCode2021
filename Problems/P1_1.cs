﻿
namespace AoC21.Problems
{
    internal class P1_1 : Problem
    {
        public P1_1(string inputPath) : base(inputPath) { }

        public override string Compute()
            => ComputeRecursive(Lines.GetEnumerator(), 0, null).ToString();


        private static int ComputeRecursive(IEnumerator<string> iter, int result, string? currentLine)
        {
            if (currentLine == null && iter.MoveNext()) currentLine = iter.Current;
            
            else if (!iter.MoveNext()) return result;

            if (Convert.ToInt32(currentLine) < Convert.ToInt32(iter.Current)) result++;

            return ComputeRecursive(iter, result, iter.Current);
        }
    }
}