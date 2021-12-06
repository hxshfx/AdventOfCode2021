﻿using AdventOfCode.Utils;

namespace AdventOfCode.Problems.Y2020
{
    internal class P5 : Problem
    {
        public override (Part, Part) Parts { get; set; }


        public P5(string inputPath) : base(inputPath)
            => Parts = (new P5_1(), new P5_2());


        internal class P5_1 : Part
        {
            public override Result Compute(IEnumerable<string> lines)
                => new(ComputeRecursive(lines.GetEnumerator(), 0).ToString(), Sw.ElapsedMilliseconds);


            private static int ComputeRecursive(IEnumerator<string> iter, int result)
            {
                if (!iter.MoveNext()) return result;

                int newResult = BinarySearch(iter.Current[0..^3], true, 1, 0, 127) * 8 + BinarySearch(iter.Current[^3..], false, 1, 0, 7);

                if (newResult > result) result = newResult;

                return ComputeRecursive(iter, result);
            }

            private static int BinarySearch(string line, bool rows, int step, int lowerBound, int upperBound)
            {
                if (lowerBound == upperBound) return lowerBound;

                int newBound = rows ? 64 / (1 << (step - 1)) : 4 / (1 << (step - 1));
                bool upperHalf = rows ? line[step - 1] == 'B' : line[step - 1] == 'R';

                if (upperHalf) lowerBound += newBound;
                else upperBound -= newBound;

                return BinarySearch(line, rows, ++step, lowerBound, upperBound);
            }
        }

        internal class P5_2 : Part
        {
            public override Result Compute(IEnumerable<string> lines)
                => new(ComputeRecursive(lines.GetEnumerator(), new List<int>()).ToString(), Sw.ElapsedMilliseconds);


            private static int ComputeRecursive(IEnumerator<string> iter, IList<int> ids)
            {
                if (!iter.MoveNext()) return FindMissing(ids.OrderByDescending(i => i).GetEnumerator(), -1, -1);

                ids.Add(BinarySearch(iter.Current[0..^3], true, 1, 0, 127) * 8 + BinarySearch(iter.Current[^3..], false, 1, 0, 7));

                return ComputeRecursive(iter, ids);
            }

            private static int BinarySearch(string line, bool rows, int step, int lowerBound, int upperBound)
            {
                if (lowerBound == upperBound) return lowerBound;

                int newBound = rows ? 64 / (1 << (step - 1)) : 4 / (1 << (step - 1));
                bool upperHalf = rows ? line[step - 1] == 'B' : line[step - 1] == 'R';

                if (upperHalf) lowerBound += newBound;
                else upperBound -= newBound;

                return BinarySearch(line, rows, ++step, lowerBound, upperBound);
            }

            private static int FindMissing(IEnumerator<int> iter, int missing, int previous)
            {
                if (previous == -1 && iter.MoveNext()) previous = iter.Current;

                if (!iter.MoveNext() || missing != -1) return missing;

                if (previous - 1 != iter.Current) missing = previous - 1;

                return FindMissing(iter, missing, iter.Current);
            }
        }
    }
}
