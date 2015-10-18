namespace EventPlanner.FourSquare.Entities
{
    /// <summary>
    /// Category that have been applied to venue
    /// </summary>
    public class Category
    {
        /// <summary>
        /// ID of category
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Plural name of category
        /// </summary>
        public string PluralName { get; set; }

        /// <summary>
        /// Short name of category
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Icon of category
        /// </summary>
        public Icon Icon { get; set; }

        /// <summary>
        /// Flag indicating ff this is the primary category for parent venue object
        /// </summary>
        public bool Primary { get; set; }
    }
}