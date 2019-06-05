using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UltricoCalendarContracts.Models
{
    public class EventMetadata
    {
        public int Id { get; set; }

        [Required] public string Title { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public IEnumerable<DateTime> Start { get; set; }

        [Required]
        [Display(Name = "Duration")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        [RegularExpression(@"((([0-1][0-9])|(2[0-3]))(:[0-5][0-9])(:[0-5][0-9])?)", ErrorMessage =
            "Time must be between 00:00 to 23:59")]
        public TimeSpan Duration { get; set; }
    }
}