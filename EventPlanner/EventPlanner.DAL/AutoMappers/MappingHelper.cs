using System;
using System.Collections.Generic;
using System.Linq;
using EventPlanner.Models.Domain;
using EventPlanner.Models.Enums;
using EventPlanner.Models.Models.CreateAndEdit;
using EventPlanner.Models.Models.Vote;

namespace EventPlanner.DAL.AutoMappers
{
    /// <summary>
    ///     Static class with static methods of all mappers for models
    /// </summary>
    public static class MappingHelper
    {
        /// <summary>
        ///     Mapping list of longitude and latitude values of Venue to Geometry model for map
        /// </summary>
        /// <param name="lng">Longitude value from Venue</param>
        /// <param name="lat">Latitude value from Venue</param>
        /// <returns>Geometry model</returns>

        public static Geometry MapToGeometry(double lng, double lat)
        {
            return new Geometry() { type = "Point", coordinates = new double[] { lng, lat } };
        }

        /// <summary>
        ///     Mapping list of name and address of Venue to Properties model for map
        /// </summary>
        /// <param name="name">Name of Venue</param>
        /// <param name="address">Address of Venue</param>
        /// <returns>LProperties model</returns>
        public static Properties MapToProperties(string name, string address)
        {
            return new Properties() { name = name, address = address };
        }
                
        /// <summary>
        ///     Mapping list of date models into list of timeslot models
        /// </summary>
        /// <param name="dates">List of date models</param>
        /// <returns>List of timeslot models</returns>

        public static IList<TimeSlot> MapToTimeSlot(IList<EventModel.DatesModel> dates)
        {
            return dates.SelectMany(d => d.Times.Select(time => new TimeSlot()
            {
                Id = time.Id,
                DateTime = d.Date.Add(TimeSpan.Parse(time.Time))
            }).ToList()).ToList();
        }

        /// <summary>
        ///     Mapping list of timeslot models into list of date models
        /// </summary>
        /// <param name="timeSlots">List of timeslot models</param>
        /// <returns>List of date models</returns>
        public static IList<EventModel.DatesModel> MapToDatesModel(IList<TimeSlot> timeSlots)
        {
            return timeSlots.GroupBy(ts => ts.DateTime.Date, ts => ts)
                .Select(
                    tsGrp =>
                        new EventModel.DatesModel()
                        {
                            Date = tsGrp.Key.Date,
                            Times = tsGrp.Select(ts => new EventModel.TimeModel(ts.Id, ts.DateTime.ToString("HH:mm:ss"))).ToList()
                        }).ToList();
        }

        /// <summary>
        ///     Mapping new option view model based on timeslot model and voter id
        /// </summary>
        /// <param name="timeSlot">Timeslot view model</param>
        /// <param name="userId">Id of a voter</param>
        /// <returns>New option view model</returns>
        public static OptionViewModel MapToOptionViewModel(TimeSlot timeSlot, string userId)
        {
            return new OptionViewModel()
            {
                Id = timeSlot.Id,
                Title = timeSlot.DateTime.ToShortDateString(),
                Desc = timeSlot.DateTime.ToLongTimeString(),
                UsersVote = MapToUsersVoteModel(timeSlot.VotesForDate, userId),
                Votes = MapToVotesViewModel(timeSlot.VotesForDate)
            };
        }

        /// <summary>
        ///     Mapping new option view model based on place model and voter id
        /// </summary>
        /// <param name="place">Place view model</param>
        /// <param name="userId">Id of a voter</param>
        /// <returns>New option view model</returns>
        public static OptionViewModel MapToOptionViewModel(PlaceViewModel place, string userId)
        {
            return new OptionViewModel()
            {
                Id = place.Id,
                Title = place.Venue.Name,
                Desc = place.Venue.AddressInfo,
                UsersVote = MapToUsersVoteModel(place.VotesForPlace, userId),
                Votes = MapToVotesViewModel(place.VotesForPlace)
            };
        }

        /// <summary>
        ///     Mapping new votes view model based on given list of date votes
        /// </summary>
        /// <param name="votes">List of date votes</param>
        /// <returns>New votes view model</returns>
        public static VotesViewModel MapToVotesViewModel(IList<VoteForDate> votes)
        {
            return new VotesViewModel()
            {
                Yes =
                    votes?.Where(vote => vote.WillAttend == WillAttend.Yes).Select(vote => vote.User?.Name ?? string.Empty).ToArray() ??
                    new string[] {},
                Maybe =
                    votes?.Where(vote => vote.WillAttend == WillAttend.Maybe).Select(vote => vote.User?.Name ?? string.Empty).ToArray() ??
                    new string[] {},
                No =
                    votes?.Where(vote => vote.WillAttend == WillAttend.No).Select(vote => vote.User?.Name ?? string.Empty).ToArray() ??
                    new string[] {}
            };
        }

        /// <summary>
        ///     Mapping new votes view model based on given list of place votes
        /// </summary>
        /// <param name="votes">List of place votes</param>
        /// <returns>New votes view model</returns>
        public static VotesViewModel MapToVotesViewModel(IList<VoteForPlace> votes)
        {
            return new VotesViewModel()
            {
                Yes =
                    votes?.Where(vote => vote.WillAttend == WillAttend.Yes).Select(vote => vote.User?.Name ?? string.Empty).ToArray() ??
                    new string[] { },
                Maybe =
                    votes?.Where(vote => vote.WillAttend == WillAttend.Maybe).Select(vote => vote.User?.Name ?? string.Empty).ToArray() ??
                    new string[] { },
                No =
                    votes?.Where(vote => vote.WillAttend == WillAttend.No).Select(vote => vote.User?.Name ?? string.Empty).ToArray() ??
                    new string[] { }
            };
        }

        /// <summary>
        ///     Mapping new user vote model based on given date votes and voter id
        /// </summary>
        /// <param name="votes">List of date votes</param>
        /// <param name="userId">Id of a voter</param>
        /// <returns>New user vote model</returns>
        public static UsersVoteModel MapToUsersVoteModel(IList<VoteForDate> votes, string userId)
        {
            return new UsersVoteModel()
            {
                Id = votes.SingleOrDefault(v => v.UserId == userId)?.Id ?? Guid.Empty,
                WillAttend = votes.SingleOrDefault(v => v.UserId == userId)?.WillAttend.ToString() ?? null
            };
        }
        
        /// <summary>
        ///     Mapping new user vote model based on given place votes and voter id
        /// </summary>
        /// <param name="votes">List of place votes</param>
        /// <param name="userId">Id of a voter</param>
        /// <returns>New user vote model</returns>
        public static UsersVoteModel MapToUsersVoteModel(IList<VoteForPlace> votes, string userId)
        {
            return new UsersVoteModel()
            {
                Id = votes.SingleOrDefault(v => v.UserId == userId)?.Id ?? Guid.Empty,
                WillAttend = votes.SingleOrDefault(v => v.UserId == userId)?.WillAttend.ToString() ?? null
            };
        }
    }
}
