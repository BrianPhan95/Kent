﻿using Kent.Libary.Utilities.Time.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kent.Libary.Utilities.Time
{
    public static class DateTimeUtilities
    {
        /// <summary>
        /// Get begin date time of date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ToBeginDate(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 1);
        }

        /// <summary>
        /// Get end date time of date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ToEndDate(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        /// <summary>
        /// Get begin date of a month in year
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DateTime ToBeginMonth(int year, int month)
        {
            return new DateTime(year, month, 1);
        }

        /// <summary>
        /// Get end date of a month in year
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DateTime ToEndMonth(int year, int month)
        {
            return new DateTime(year, month, DateTime.DaysInMonth(year, month));
        }

        /// <summary>
        /// Get start of week
        /// </summary>
        /// <param name="date"></param>
        /// <param name="startOfWeek"></param>
        /// <returns></returns>
        public static DateTime StartOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            int diff = date.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return date.AddDays(-1 * diff).Date;
        }

        #region Date Time parser

        /// <summary>
        /// Get current culture
        /// </summary>
        /// <returns></returns>
        public static string CurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }

        /// <summary>
        /// Convert current time to local time and print with short date format
        /// </summary>
        /// <param name="date"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static string ToDateFormat(this DateTime? date, TimeZoneInfo timeZone = null)
        {
            // Convert to local time
            if (timeZone != null)
                date = date.ToUserDateTime(timeZone);

            if (date.HasValue)
            {
                var format = DateFormat();
                return date.Value.ToString(format);
            }
            return string.Empty;
        }

        /// <summary>
        /// Convert current time to local time and print with short time format
        /// </summary>
        /// <param name="date"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static string ToTimeFormat(this DateTime? date, TimeZoneInfo timeZone = null)
        {
            // Convert to local time
            if (timeZone != null)
                date = date.ToUserDateTime(timeZone);

            if (date.HasValue)
            {
                var format = TimeFormat();
                return date.Value.ToString(format);
            }
            return string.Empty;
        }

        /// <summary>
        /// Convert current time to local time and print with short time format
        /// </summary>
        /// <param name="time"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static string ToTimeFormat(this TimeSpan? time, TimeZoneInfo timeZone = null)
        {
            if (timeZone != null)
                time = time.ToUserTime(timeZone);

            if (time.HasValue)
            {
                var datetime = new DateTime(time.Value.Ticks);
                var format = TimeFormat();
                return datetime.ToString(format);
            }
            return string.Empty;
        }

        /// <summary>
        /// Convert current time to local time and print with date time format
        /// </summary>
        /// <param name="date"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static string ToDateTimeFormat(this DateTime? date, TimeZoneInfo timeZone = null)
        {
            // Convert to local time
            if (timeZone != null)
                date = date.ToUserDateTime(timeZone);

            if (date.HasValue)
            {
                var format = DateTimeFormat();
                return date.Value.ToString(format);
            }
            return string.Empty;
        }

        /// <summary>
        /// Get current thread date format
        /// </summary>
        /// <returns></returns>
        public static string DateFormat()
        {
            var culture = Thread.CurrentThread.CurrentCulture;
            return culture.DateTimeFormat.ShortDatePattern;
        }

        /// <summary>
        /// Get current thread time format
        /// </summary>
        /// <returns></returns>
        public static string TimeFormat()
        {
            var culture = Thread.CurrentThread.CurrentCulture;
            return culture.DateTimeFormat.ShortTimePattern;
        }

        /// <summary>
        /// Get current thread date time format
        /// </summary>
        /// <returns></returns>
        public static string DateTimeFormat()
        {
            return string.Format("{0} {1}", DateFormat(), TimeFormat());
        }

        #endregion

        #region Date range

        /// <summary>
        /// Get the start date of a day range by a date in range
        /// </summary>
        /// <param name="date">The date in the range</param>
        /// <param name="dateRangeType"></param>
        /// <returns></returns>
        public static DateTime GetStartDateOfDateRange(this DateTime date, DateTimeEnums.DateRangeType dateRangeType)
        {
            switch (dateRangeType)
            {
                case DateTimeEnums.DateRangeType.AllTime:
                    return DateTime.MinValue;

                case DateTimeEnums.DateRangeType.Today:
                    return date.Date;

                case DateTimeEnums.DateRangeType.Yesterday:
                    return date.Date.AddDays(-1);

                case DateTimeEnums.DateRangeType.ThisWeekSunToday:
                    return date.StartOfWeek(DayOfWeek.Sunday);

                case DateTimeEnums.DateRangeType.ThisWeekMonToday:
                    return date.StartOfWeek();

                case DateTimeEnums.DateRangeType.ThisMonth:
                    return new DateTime(date.Year, date.Month, 1);

                case DateTimeEnums.DateRangeType.ThisYear:
                    return new DateTime(date.Year, 1, 1);

                default:
                    return date;
            }
        }

        #endregion
    }
}
