using MoneyManagement.Domain.Commons;
using MoneyManagement.Domain.Enums;

namespace MoneyManagement.Domain.Entities
{
    public class Expense : Auditable
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public long Amount { get; set; }
        public long Price { get; set; }
        public string Description { get; set; }
        public PaymentType Type { get; set; }
    }
}
