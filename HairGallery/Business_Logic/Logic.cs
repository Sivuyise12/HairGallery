using HairGallery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace HairGallery.Business_Logic
{
    public class Logic
    {
        private static readonly ApplicationDbContext _db = new ApplicationDbContext();

        public static bool CheckBooking(BookAppointment requestService)
        {
            bool result = false;
            var dbRecords = _db.BookAppointments.Where(x => x.DateRequestingFor == requestService.DateRequestingFor).ToList();
            foreach (var item in dbRecords)
            {
                if (requestService.TimeSlot == item.TimeSlot && requestService.SalonName == item.SalonName)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        public static bool CheckDate(DateTime subDate)
        {
            bool result = false;
            if (subDate < DateTime.Now.Date)
            {
                result = true;
            }
            return result;
        }

        public static string GetServiceType(int id)
        {
            var type = (from h in _db.Hairstyles
                         where h.HairstyleId == id
                         select h.ServiceTypes.Type).FirstOrDefault();
            return type;
        }
        public static string GetOwnerEmail(int id)
        {
            var email = (from h in _db.Hairstyles
                        where h.HairstyleId == id
                        select h.CreatedBy).FirstOrDefault();
            return email;
        }
        public static decimal GetHairstylePrice(int id)
        {
            var price = (from h in _db.Hairstyles
                         where h.HairstyleId == id
                         select h.HairstylePrice).FirstOrDefault();
            return price;
        }
        public static string GetDescription(int id)
        {
            var hairstyleDescription = (from h in _db.Hairstyles
                         where h.HairstyleId == id
                         select h.HairstyleDescription).FirstOrDefault();
            return hairstyleDescription;
        }
        public static string GetSalonName(int id)
        {
            var salonName = (from h in _db.Hairstyles
                         where h.HairstyleId == id
                         select h.SalonBuildings.SalonName).FirstOrDefault();
            return salonName;
        }
        public static string GetHairstyleName(int id)
        {
            var hairstyleName = (from h in _db.Hairstyles
                         where h.HairstyleId == id
                         select h.HairstyleName).FirstOrDefault();
            return hairstyleName;
        }
    }
}