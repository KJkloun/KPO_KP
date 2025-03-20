namespace Models
{
    // Сущность категории (доход или расход).
    public class Category
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} ({Type})";
        }
    }
}