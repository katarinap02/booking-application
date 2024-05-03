using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Repository;
using BookingApp.Repository.FeatureRepository;
using BookingApp.Repository.ReservationRepository;
using BookingApp.Serializer;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.ReservationServices
{
    public class TourParticipantService
    {
        private readonly ITourParticipantRepository _tourParticipantRepository;
        private static readonly TourReservationService _tourReservationService
                = new TourReservationService(Injector.Injector.CreateInstance<ITourReservationRepository>());
        public static readonly TouristNotificationService _touristNotificationService 
                = new TouristNotificationService(Injector.Injector.CreateInstance<ITouristNotificationRepository>());
        public static readonly UserService _userService 
                = new UserService(Injector.Injector.CreateInstance<IUserRepository>());
        public static readonly TourService _tourService
                = new TourService(Injector.Injector.CreateInstance<ITourRepository>());

        public TourParticipantService(ITourParticipantRepository tourParticipantRepository)
        {
            _tourParticipantRepository = tourParticipantRepository;
        }

        public TourParticipantViewModel saveParticipantToDTO(string name, string lastName, int age)
        {
            TourParticipantViewModel tourParticipantViewModel = new TourParticipantViewModel(_tourParticipantRepository.SaveParticipant(name, lastName, age));
            return tourParticipantViewModel;
        }

        public void saveParticipant(TourParticipantViewModel tourParticipantViewModel, int reservationId)
        {
            _tourParticipantRepository.SaveParticipant(tourParticipantViewModel.ToTourParticipant(), reservationId);
        }

        public bool IsUserJoined(int reservationId, string touristName, string touristLastName)
        {
            return _tourParticipantRepository.IsUserJoined(reservationId, touristName, touristLastName);
        }

        public List<TourParticipant> GetAllJoinedParticipantsByReservation(int reservationId)
        {
            List<TourParticipant> tourParticipants = _tourParticipantRepository.GetAllParticipantsByReservation(reservationId);
            foreach (TourParticipant tp in tourParticipants.ToList())
            {
                if (!tp.HasJoinedTour)
                {
                    tourParticipants.Remove(tp);
                }
            }

            return tourParticipants;
        }

        public TourParticipant GetById(int id)
        {
            return _tourParticipantRepository.GetById(id);
        }


        public int getjoinedCheckpoint(int tour_id)
        {
            TourParticipant participant = GetById(_tourReservationService.getTouristParticipantID(tour_id));
            if (!participant.HasJoinedTour)
            {
                return -1;
            }
            return participant.JoinedCheckpointIndex;
        }

        public void JoinTour(int participant_id, int current_checkpoint_index) // prebaciti
        {
            TourParticipant tourParticipant = GetById(participant_id);
            tourParticipant.HasJoinedTour = true;
            tourParticipant.JoinedCheckpointIndex = current_checkpoint_index;
            SendNotification(tourParticipant.ReservationId, current_checkpoint_index);
            _tourParticipantRepository.Update(tourParticipant);
        }

        private void SendNotification(int reservation_id, int checkpoint) 
        {
            TourReservation tourReservation = _tourReservationService.GetById(reservation_id);
            Tour tour = _tourService.GetTourById(tourReservation.TourId);
            User user = _userService.GetById(tour.GuideId);
            TouristNotification notification = new TouristNotification(0, tourReservation.TouristId, tourReservation.TourId, NotificationType.JoinedTour, tour.Name, user.Username, checkpoint);
            _touristNotificationService.Add(notification);
        }
    }
}
