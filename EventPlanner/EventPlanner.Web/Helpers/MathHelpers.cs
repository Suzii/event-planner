using System;

namespace EventPlanner.Web.Helpers
{
    public static class MathHelpers
    {
        public static int RoundedPercentage(double items, double total)
        {
            return (int)Math.Round((double)(100 * items) / total, 0);
        }
    }
}
