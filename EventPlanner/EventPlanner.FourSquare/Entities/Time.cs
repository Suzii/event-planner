namespace EventPlanner.FourSquare.Entities
{
    /// <summary>
    /// Class representing opening time
    /// </summary>
    public class Time
    {
        /// <summary>
        /// Opening time.
        /// </summary>
        public string RenderedTime { get; set; }
        
        /// <summary>
        /// String representation of opening time
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return RenderedTime;
        }
    }
}
