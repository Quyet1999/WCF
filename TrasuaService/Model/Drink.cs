namespace TrasuaService.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Drink")]
    public partial class Drink
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int Type { get; set; }

        public bool Status { get; set; }

        public int Price { get; set; }

        public int AccountChange { get; set; }

        public int AccountCreate { get; set; }

        public DateTime TimeCreate { get; set; }

        public DateTime TimeChange { get; set; }

        public virtual TypeDrink TypeDrink { get; set; }
    }
}
