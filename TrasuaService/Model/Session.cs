namespace TrasuaService.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Session")]
    public partial class Session
    {
        [StringLength(10)]
        public string SessionID { get; set; }

        public int AccountID { get; set; }

        public DateTime Timecreate { get; set; }

        public DateTime TimeEnd { get; set; }

        public bool Status { get; set; }
    }
}
