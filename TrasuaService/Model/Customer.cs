namespace TrasuaService.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        public bool? Sex { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public DateTime? TimeSignUp { get; set; }

        public int? EmployeeAccept { get; set; }

        public int? AccountID { get; set; }
    }
}
