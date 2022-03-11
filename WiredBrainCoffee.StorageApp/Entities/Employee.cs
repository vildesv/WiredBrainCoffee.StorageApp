namespace WiredBrainCoffee.StorageApp.Entities
{
    public class Employee : Entitybase
    {
        public string? FirstName { get; set; }

        public override string ToString() => $"Id: {Id}, FirstName: {FirstName}";
    }
}
