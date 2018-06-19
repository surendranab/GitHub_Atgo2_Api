using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;
using NodaTime.Extensions;
using NodaTime.TimeZones;

namespace Atgo2.Api.CrossCuttingLayer.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly IDateTimeZoneProvider TzSource;

        static DateTimeExtensions()
        {
            TzSource = new DateTimeZoneCache(TzdbDateTimeZoneSource.Default);
        }

        public static DateTime ConvertToLocalTime(this DateTime utcDateTime, string timeZoneId)
        {
            DateTime dUtc;
            switch (utcDateTime.Kind)
            {
                case DateTimeKind.Utc:
                    dUtc = utcDateTime;
                    break;
                case DateTimeKind.Local:
                    dUtc = utcDateTime.ToUniversalTime();
                    break;
                default: //DateTimeKind.Unspecified
                    dUtc = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);
                    break;
            }

            var timeZone = TzSource.GetZoneOrNull(timeZoneId);
            if (timeZone == null)
            {
                return utcDateTime;
            }

            var instant = Instant.FromDateTimeUtc(dUtc);
            var zoned = new ZonedDateTime(instant, timeZone);

            return new DateTime(
                zoned.Year,
                zoned.Month,
                zoned.Day,
                zoned.Hour,
                zoned.Minute,
                zoned.Second,
                zoned.Millisecond,
                DateTimeKind.Unspecified);

        }

        public static DateTime ConvertToUtc(this DateTime localDateTime, string timeZoneId, ZoneLocalMappingResolver resolver = null)
        {
            if (localDateTime.Kind == DateTimeKind.Utc) return localDateTime;

            if (resolver == null) resolver = Resolvers.LenientResolver;
            var timeZone = TzSource.GetZoneOrNull(timeZoneId);
            if (timeZone == null)
            {
                return localDateTime;
            }

            var local = LocalDateTime.FromDateTime(localDateTime);
            var zoned = timeZone.ResolveLocal(local, resolver);
            return zoned.ToDateTimeUtc();
        }

        public static IReadOnlyCollection<string> GetTimeZoneList()
        {
            return TzSource.Ids;
        }

        public static DateTimeZone GetTimeZone(string id)
        {
            return TzSource.GetZoneOrNull(id);
        }

        public static Dictionary<string, string> GetAllTimeZone()
        {
            var usTimeZones = from x in TzSource.GetAllZones() where x.Id.StartsWith("US/") select x;
            Dictionary<string, string> timezones = new Dictionary<string, string>();
            foreach (var timeZone in usTimeZones)
            {
                var zonedDateTime = SystemClock.Instance.GetCurrentInstant().InZone(timeZone);
                TimeSpan zoneOffset = zonedDateTime.ToDateTimeOffset().Offset;
                string sTimeDisplay = $"{zonedDateTime.Zone.GetZoneInterval(zonedDateTime.ToInstant()).Name} (UTC{(zoneOffset < TimeSpan.Zero ? "-" : "+")}{zoneOffset:hh\\:mm} {zonedDateTime.Zone.Id})";
                timezones.Add(timeZone.Id, sTimeDisplay);
            }

            return timezones;
        }

        public static List<string> GetZoneDateTimeName()
        {
            var usTimeZones = from x in TzSource.GetAllZones() where x.Id.StartsWith("US/") select x;
            List<string> timezones = new List<string>();
            foreach (var timeZone in usTimeZones)
            {
                var zonedDateTime = SystemClock.Instance.GetCurrentInstant().InZone(timeZone);
                string sTimeDisplay = zonedDateTime.Zone.GetZoneInterval(zonedDateTime.ToInstant()).Name;
                timezones.Add(sTimeDisplay);
            }
            return timezones;
        }

        public static Dictionary<string, string> GetTimeZoneAbbreviations()
        {
            Dictionary<string, string> timezonesObj = new Dictionary<string, string>();

            var timeZones = GetAllTimeZone();
            var timeZonesValues = timeZones.Select(a => a.Key);

            var stimeZone = GetZoneDateTimeName();
            int n = 0;
            foreach (var item in timeZonesValues)
            {
                timezonesObj.Add(item, stimeZone[n]);
                n++;
            }
            return timezonesObj;
        }

        public static string GetOffSetFromTime(this DateTime currentDateTime, string timeZoneId)
        {
            var timeZone = TzSource.GetZoneOrNull(timeZoneId);
            DateTime utc;
            utc = DateTime.SpecifyKind(currentDateTime, DateTimeKind.Utc);
            var instant = Instant.FromDateTimeUtc(utc);
            var zoned = new ZonedDateTime(instant, timeZone);
            return zoned.Offset.ToString();
        }
    }
}
