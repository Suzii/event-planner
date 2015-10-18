
using System;
using System.Collections.Generic;
using EventPlanner.Models.Domain;

namespace EventPlanner.Services
{
    public interface IEvent
    {
        /// <summary>
        ///  This method create new event in the database.
        ///  User needs to write name, description, duration, times and dates.
        /// </summary>
        /// <returns>Return new event on success</returns>
        Event CreateNewEvent();

        /// <summary>
        ///  This method delete event in the database accourding id.
        /// </summary>
        /// <returns>Return true on success</returns>
        bool DeleteEvent(Guid id);

        /// <summary>
        ///  This method update event in the database.
        /// </summary>
        /// <returns>Return updated event on success</returns>
        Event UpdateEvent();

        /// <summary>
        ///  This method return list of all active (people can still vote) events, that user created.
        /// </summary>
        /// <returns>Return list of all active event</returns>
        IEnumerable<Event> ListOfMyActiveEvents();

        /// <summary>
        ///  This method return list of all inctive (people can not vote anymore) events, that user created.
        /// </summary>
        /// <returns>Return list of all inactive event</returns>
        IEnumerable<Event> ListOfMyIntactiveEvents();

        /// <summary>
        ///  This method show all dates and times that users can vote for in particular event.
        /// </summary>
        /// <returns>Return list of all dates</returns>
        IEnumerable<TimeSlot> ListOfAllDates();

        /// <summary>
        ///  This method show all places that users can vote for in particular event.
        /// </summary>
        /// <returns>Return list of all plates</returns>
        IEnumerable<Place> ListOfAllPlaces();

        /// <summary>
        ///  This method will save to databes user's votes dates..
        /// </summary>
        /// <returns>Returns true o success</returns>
        bool VoteForDates();

        /// <summary>
        ///  This method will save to databes user's votes places..
        /// </summary>
        /// <returns>Returns true o success</returns>
        bool VoteForPlaces();

        /// <summary>
        ///  This method will return all of votes that particular user had for specific event..
        /// </summary>
        /// <returns>Returns list of votes</returns>
        /// Co přesně vrátí?
        bool UserVote();
    }
}