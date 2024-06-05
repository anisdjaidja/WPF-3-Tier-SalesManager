using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_N_Tier_Test.Modules.Helpers
{
    public static class StringInterpolation
    {
        public static string ToFormattedString(this TimeSpan ts)
        {
            const string separator = ", ";

            if (ts.TotalSeconds < 1) { return "Now"; }

            if (ts.TotalDays > 0)
                return string.Join(separator, new string[]
                {
                ts.Days > 0 ? ts.Days + (ts.Days > 1 ? $" {Current.TryFindResource("days")}" : $" {Current.TryFindResource("day")}") : null,
                }.Where(t => t != null));
            else
            {
                if (ts.TotalHours > 0)
                    return string.Join(separator, new string[]
                    {
                    ts.Hours > 0 ? ts.Hours + (ts.Hours > 1 ? $" {Current.TryFindResource("hours")}" : $" {Current.TryFindResource("hour")}") : null,
                    ts.Minutes > 0 ? ts.Minutes + (ts.Minutes > 1 ? $" {Current.TryFindResource(" minutes")}" : $" {Current.TryFindResource("minute")}") : null,
                    }.Where(t => t != null));
                else
                {
                    if(ts.TotalMinutes > 0)
                        return string.Join(separator, new string[]
                        {
                        ts.Minutes > 0 ? ts.Minutes + (ts.Minutes > 1 ? $" {Current.TryFindResource(" minutes")}" : $" {Current.TryFindResource("minute")}") : null,
                        }.Where(t => t != null));
                    else
                    {
                        return string.Join(separator, new string[]
                        {
                                        ts.Seconds > 0 ? ts.Seconds + (ts.Seconds > 1 ? $" {Current.TryFindResource("seconds")}" : $" {Current.TryFindResource("second")}") : null,
                        }.Where(t => t != null));
                    }
                }
            }
        }
    }
}
