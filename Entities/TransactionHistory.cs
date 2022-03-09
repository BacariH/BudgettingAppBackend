using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgettingApp.Entities
{
    [Table("TransactionHistories")]
    public class TransactionHistory
    {

        //Remember to always put an Id value
        public int Id { get; set; }
        public DateTime CreatedTransaction { get; set; }

        public int AmountOfTransaction { get; set; }

        public string TransactionDescription { get; set; }

        public string TransactionType { get; set; }

        //Marks that this should be related to the generated user

        public int UserEntityId { get; set; }
        [ForeignKey("UserEntityId")]
        public UserEntity UserEntity { get; set; }

    }
}
