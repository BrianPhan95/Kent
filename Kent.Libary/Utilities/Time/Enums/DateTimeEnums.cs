using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Libary.Utilities.Time.Enums
{
    public class DateTimeEnums
    {
        public enum DateRangeType
        {

            [Description("Today")]
            Today = 0,

            [Description("Yesterday")]
            Yesterday = 1,

            [Description("This Week (Sun - Today)")]
            ThisWeekSunToday = 2,

            [Description("This Week (Mon - Today)")]
            ThisWeekMonToday = 3,

            [Description("This Month")]
            ThisMonth = 4,

            [Description("This Year")]
            ThisYear = 5,

            [Description("All Time")]
            AllTime = 6,

            [Description("Custom")]
            Custom = 7
        }

        public enum TimePeriodType
        {
            Daily = 0,
            Monthly = 1,
            Yearly = 2
        }
    }
}
