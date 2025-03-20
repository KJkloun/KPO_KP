using System.Globalization;

namespace Models
{
    // Сущность банковского счёта.
    public class BankAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} (Баланс: {Balance.ToString("C", CultureInfo.CurrentCulture)})";
        }
    }
}