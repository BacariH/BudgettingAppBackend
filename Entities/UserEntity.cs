using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgettingApp.Entities
{
    public class UserEntity
    {
        //Using this naming convention for the entity framework
        public int Id { get; set; }

        public string UserName { get; set; }

        public int AmountStart { get; set; }

        //Making a table of the transaction history of the user
        public ICollection<TransactionHistory> TransactionHistories { get; set; }
    }
}
