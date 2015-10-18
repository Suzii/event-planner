namespace EventPlanner.FourSquare.Entities
{
    /// <summary>
    /// Meta informations about recieved response
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// Status code of response
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Requester ID
        /// </summary>
        public string RequestId { get; set; }
    }
}