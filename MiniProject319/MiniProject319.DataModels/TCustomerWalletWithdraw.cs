using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MiniProject319.DataModels
{
    [Table("t_customer_wallet_withdraw")]
    public partial class TCustomerWalletWithdraw
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("customer_id")]
        public long CustomerId { get; set; }
        [Column("wallet_default_nominal_id")]
        public long? WalletDefaultNominalId { get; set; }
        [Column("amount")]
        public int Amount { get; set; }
        [Column("bank_name")]
        [StringLength(50)]
        [Unicode(false)]
        public string BankName { get; set; } = null!;
        [Column("account_number")]
        [StringLength(50)]
        [Unicode(false)]
        public string AccountNumber { get; set; } = null!;
        [Column("account_name")]
        [StringLength(255)]
        [Unicode(false)]
        public string AccountName { get; set; } = null!;
        [Column("otp")]
        public int Otp { get; set; }
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
