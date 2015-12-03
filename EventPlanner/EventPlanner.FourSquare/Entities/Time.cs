namespace EventPlanner.FourSquare.Entities
{
    public class Time
    {
        /// <summary>
        /// Opening time.
        /// </summary>
        public string RenderedTime { get; set; }

        public override string ToString()
        {
            return RenderedTime;
        }
    }
}
