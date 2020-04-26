using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace IEXTrading.Models
{
    public class Rec
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string phone_number { get; set; }
        public string activity { get; set; }
        public string recreation_center { get; set; }
        public string address { get; set; }
        public string age_requirements { get; set; }
        public string days_of_week { get; set; }
        public string times { get; set; }
        public List<Equity> Equities { get; set; }

    }
}

