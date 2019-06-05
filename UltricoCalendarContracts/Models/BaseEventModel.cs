using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UltricoCalendarContracts.Entities;
using UltricoCalendarContracts.Interfaces;

namespace UltricoCalendarContracts.Models
{
    public abstract class BaseEventModel : ICalendarEvent
    {
        [Required] public string Title { get; set; }

        [MaxLength(500)] public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; }

        [Required]
        [Display(Name = "Duration")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        [RegularExpression(@"((([0-1][0-9])|(2[0-3]))(:[0-5][0-9])(:[0-5][0-9])?)", ErrorMessage =
            "Time must be between 00:00 to 23:59")]
        public string Duration { get; set; }

        [EmailAddress] public List<string> MailAddresses { get; set; }

        public abstract CalendarEvent ToEntity();
    }
}