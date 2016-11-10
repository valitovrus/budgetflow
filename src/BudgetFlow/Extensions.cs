using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetFlow
{
    public static class Extensions
    {
        public static bool Between(this DateTime date, DateTime from, DateTime to)
        {
            if (from == null)
                throw new ArgumentNullException("from");
            if (to == null)
                throw new ArgumentNullException("to");

            return date >= from && date <= to;
        }
    }
}
