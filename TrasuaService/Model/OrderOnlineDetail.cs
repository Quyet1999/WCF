namespace TrasuaService.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderOnlineDetail")]
    public partial class OrderOnlineDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_Detail { get; set; }

        public int? OrderOnlineID { get; set; }

        public int? ItemOrderID { get; set; }

        public int? NumberOrder { get; set; }

        public int? Price { get; set; }

        public int? TotalMoneyDetail { get; set; }

        public int? TypeItem { get; set; }

        public virtual OrderOnline OrderOnline { get; set; }
    }
}
