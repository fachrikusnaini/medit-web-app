using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject319.DataModels
{
    [Table("t_customer_wallet")]
    public partial class TCustomerWallet
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("customer_id")]
        public long? CustomerId { get; set; }
        [Column("pin")]
        [StringLength(6)]
        [Unicode(false)]
        public string? Pin { get; set; }
        [Column("balance", TypeName = "decimal(18, 0)")]
        public decimal? Balance { get; set; }
        [Column("barcode")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Barcode { get; set; }
        [Column("points", TypeName = "decimal(18, 0)")]
        public decimal? Points { get; set; }
        [Column("created_by")]
        public long CreatedBy { get; set; }
        [Column("created_on", TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column("modified_by")]
        public long? ModifiedBy { get; set; }
        [Column("modified_on", TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        [Column("deleted_by")]
        public long? DeletedBy { get; set; }
        [Column("deleted_on", TypeName = "datetime")]
        public DateTime? DeletedOn { get; set; }
        [Column("is_delete")]
        public bool IsDelete { get; set; }
    }
}
