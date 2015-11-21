using System;
using EventPlanner.Models.Enums;

namespace EventPlanner.Models.Domain
{
    /// <summary>
    /// Interface for all Vote models.
    /// </summary>
    interface IVoteModel
    {
        /// <summary>
        /// Id of Vote object.
        /// </summary>
        Guid Id { set; get; }

        /// <summary>
        /// Id of User the Vote was submitted by.
        /// </summary>
        string UserId { set; get; }


        /// <summary>
        /// Id of option this Vote belongs to.
        /// </summary>
        Guid OptionId { set; get; }

        /// <summary>
        /// Decision made by User.
        /// </summary>
        WillAttend? WillAttend { get; set; }
    }
}
