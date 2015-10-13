namespace EventPlanner.FourSquare.Entities
{
    public class Category
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string PluralName { get; set; }

        public string ShortName { get; set; }

        public Icon Icon { get; set; }

        public bool Primary { get; set; }
    }
}