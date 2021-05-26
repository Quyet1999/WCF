namespace TrasuaService.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BillDetail")]
    public partial class BillDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_Detail { get; set; }

        public int? BillID { get; set; }

        public int? ItemID { get; set; }

        public int? NumberItem { get; set; }

        public int? Price { get; set; }

        public int? TotalMoneyDetail { get; set; }

        public bool? TypeItem { get; set; }

        public virtual Bill Bill { get; set; }
    }
}
