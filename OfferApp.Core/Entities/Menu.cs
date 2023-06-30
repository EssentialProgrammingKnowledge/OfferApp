namespace OfferApp.Core.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; } = "";

        public Menu(int id, string name)
            : base(id)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id}. {Name}";
        }
    }
}
