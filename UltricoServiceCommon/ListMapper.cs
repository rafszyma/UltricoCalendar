using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UltricoCalendarCommon
{
    public static class ListMapper
    {
        public static string ToJson(List<string> list)
        {
            return JsonConvert.SerializeObject(list);
        }

        public static List<string> FromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<string>>(json);
        }

        public static string ToJsonDateTime(List<DateTime> list)
        {
            return JsonConvert.SerializeObject(list);
        }

        public static List<DateTime> FromJsonDateTime(string json)
        {
            return JsonConvert.DeserializeObject<List<DateTime>>(json);
        }
    }
}