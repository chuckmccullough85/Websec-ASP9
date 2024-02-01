using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcmeLib
{
    public enum TransactionType { Debit, Credit }
    /// <summary>
    /// Represents a transaction.  This is a double entry accounting system.  
    /// One transaction object for the credit,
    /// and one transaction for the debit account.
    /// </summary>
    [Table("Transactions")]
    public class Transaction
    {
        public int Id { get; set; }
        [ForeignKey("AcctId")]
        public virtual Account Account { get; set; }
        public decimal Amount { get; set; }
        /// <summary>
        /// This field is only used in bill pay scenarios.  
        /// Leave blank for internal account transfers.
        /// </summary>
        public string Payee { get; set; }
        [Column("type")]
        public TransactionType Type { get; set; }

        public string TypeString
        {
            get { return Type.ToString(); }
        }
        [Column("TransDate")]
        public DateTime Date { get; set; }

    }
}
