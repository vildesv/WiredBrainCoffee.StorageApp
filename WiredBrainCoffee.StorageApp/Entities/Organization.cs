namespace WiredBrainCoffee.StorageApp.Entities
{
    public class Organization : Entitybase
    {
        public string? Name { get; set; }

        public override string ToString() => $"Id: {Id}, Name: {Name}";
    }
}
