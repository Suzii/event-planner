namespace EventPlanner.FourSquare.Entities
{
    /// <summary>
    /// Basic statistics of venue
    /// </summary>
    public class Stats
    {
        /// <summary>
        /// Total checkins ever here
        /// </summary>
        public int CheckinsCount { get; set; }

        /// <summary>
        /// Total users who have ever checked in here
        /// </summary>
        public int UsersCount { get; set; }

        /// <summary>
        /// Number of tips here
        /// </summary>
        public int TipCount { get; set; }

        /// <summary>
        /// Number of visits here
        /// </summary>
        public int VisitsCount { get; set; }
    }
}
