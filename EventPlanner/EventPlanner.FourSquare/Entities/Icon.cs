namespace EventPlanner.FourSquare.Entities
{
    /// <summary>
    /// Pieces needed to construct category icons at various sizes. Combine prefix with a size (32, 44, 64, and 88 are available) and suffix
    /// </summary>
    public class Icon
    {
        /// <summary>
        /// Prefix of icon. E.g. "https://foursquare.com/img/categories/food/default_"
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Suffix of icon. E.g. ".png"
        /// </summary>
        public string Suffix { get; set; }
    }
}