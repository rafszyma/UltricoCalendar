using System;

namespace UltricoCalendarContracts.Enums
{
    public enum FinishEnum
    {
        AfterDate,
        AfterOccurs,
        NeverFinish
    }

    public static class FinishEnumExtension
    {
        public static FinishClass GetOccurenceClass(this FinishEnum finishEnum)
        {
            if (finishEnum == FinishEnum.NeverFinish)
            {
                return new NeverFinish();
            }
            
            throw new InvalidCastException("Incorrect call, use other extension");
        }
        
        public static FinishClass GetOccurenceClass(this FinishEnum finishEnum, DateTime endDate)
        {
            if (finishEnum == FinishEnum.AfterDate)
            {
                return new FinishAfterDate(endDate);
            }
            
            throw new InvalidCastException("Incorrect call, use other extension");
        }
        
        public static FinishClass GetOccurenceClass(this FinishEnum finishEnum, int occurs)
        {
            if (finishEnum == FinishEnum.AfterOccurs)
            {
                return new FinishAfterOccurs(occurs);
            }
            
            throw new InvalidCastException("Incorrect call, use other extension");
        }
    }
}