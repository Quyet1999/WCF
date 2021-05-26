namespace TrasuaService.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderOnline")]
    public partial class OrderOnline
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderOnline()
        {
            OrderOnlineDetails = new HashSet<OrderOnlineDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? CustomerID { get; set; }

        public DateTime? TimeOrder { get; set; }

        public int? EmployeeAccept { get; set; }

        public DateTime? TimeAccept { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(12)]
        public string PhoneNumber { get; set; }

        public int? TotalMoney { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderOnlineDetail> OrderOnlineDetails { get; set; }
    }
}
