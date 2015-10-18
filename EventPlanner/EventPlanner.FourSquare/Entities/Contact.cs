namespace EventPlanner.FourSquare.Entities
{
    /// <summary>
    /// Contact informations
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Phone number. E.g. "+16466026263"
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Formated phone number. E.g. "+1 646-602-6263"
        /// </summary>
        public string FormattedPhone { get; set; }

        /// <summary>
        /// Twitter link
        /// </summary>
        public string Twitter { get; set; }
    }
}
