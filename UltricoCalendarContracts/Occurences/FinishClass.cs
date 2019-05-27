using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UltricoCalendarContracts.Enums;

namespace UltricoCalendarContracts
{
    public abstract class FinishClass
    {
        public abstract IEnumerable<DateTime> Occur(RepeatPeriod repeatPeriod, DateTime repeatFrom, DateTime repeatTill);

        public abstract FinishEnum GetFinishValue();

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static FinishClass FromJson(string v)
        {
            return JsonConvert.DeserializeObject<FinishClass>(v);
        }
    }
}