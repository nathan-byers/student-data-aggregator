using System;
using System.Collections.Generic;
using System.Linq;

namespace student_data_aggregator
{
    public static class Util
    {
        public static decimal GetAvg(IEnumerable<decimal> grades)
        {
            return Math.Round(grades.Where(g => g >= 1)
                    .Average(g => decimal.Truncate(g)), 1);
        }
    }
}