﻿using BookingApp.Domain.Model.Rates;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.RateServices
{
    public class RenovationRecommendationService
    {
        private readonly IRenovationRecommendationRepository RenovationRecommendationRepository;

        public RenovationRecommendationService(IRenovationRecommendationRepository renovationRecommendationRepository)
        {
            this.RenovationRecommendationRepository = renovationRecommendationRepository;
        }

        public RenovationRecommendation Add(RenovationRecommendation recommendation)
        {
            return RenovationRecommendationRepository.Add(recommendation);
        }

        public List<RenovationRecommendation> GetAll()
        {
            return RenovationRecommendationRepository.GetAll();
        }

        public RenovationRecommendation GetById(int id)
        {
            return RenovationRecommendationRepository.GetById(id);
        }

        public int GetNumOfRecommendationsByYear(int accId, int year)
        {
            int num = 0;
            foreach (RenovationRecommendation ar in RenovationRecommendationRepository.GetAll())
            {
                
                if (ar.AccommodationId == accId && ar.Date.Year == year)
                {
                    num++;
                }
            }

            return num;
        }

        public int GetNumOfRecommendationsByMonth(int accId, int month, int year)
        {
            int num = 0;
            foreach (RenovationRecommendation ar in RenovationRecommendationRepository.GetAll())
            {
                
                if (ar.AccommodationId == accId && ar.Date.Month == month && ar.Date.Year == year)
                {
                    num++;
                }
            }

            return num;
        }

        public List<int> GetAllYearsForAcc(int accId)
        {
            HashSet<int> uniqueYears = new HashSet<int>(); 

            foreach (RenovationRecommendation ar in RenovationRecommendationRepository.GetAll())
            {
                if (ar.AccommodationId == accId)
                {
                    uniqueYears.Add(ar.Date.Year); 
                }
            }
            return uniqueYears.ToList();
        }

        public List<int> GetAllMonthsForAcc(int accId, int year)
        {
            HashSet<int> uniqueMonths = new HashSet<int>(); // Using HashSet to store unique years

            foreach (RenovationRecommendation ar in RenovationRecommendationRepository.GetAll())
            {
                if (ar.AccommodationId == accId && ar.Date.Year == year)
                {
                    uniqueMonths.Add(ar.Date.Month); // Add the year to the HashSet
                }
            }


            return uniqueMonths.ToList();
        }

        public List<int> GetAllRecommendationsForYears(int accId)
        {
            List<int> list = new List<int>();
            foreach (int year in GetAllYearsForAcc(accId))
            {
                list.Add(GetNumOfRecommendationsByYear(accId, year));
            }
            return list;
        }

        public List<int> GetAllRecommendationForMonths(int accId, int year)
        {
            List<int> list = new List<int>();
            foreach (int month in GetAllMonthsForAcc(accId, year))
            {
                list.Add(GetNumOfRecommendationsByMonth(accId, month, year));
            }
            return list;
        }

    }
}
