using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ss_api.Controllers {
    [Route ("api/[controller]/[action]")]
    public class SSController : Controller {
        private Models.SSAPIClient client;
        private static string baseUrl = "http://api.seatseller.travel";
        private static string consumerKey = "a";
        private static string consumerSecret = "a";
        public SSController () {
            client = new Models.SSAPIClient (baseUrl, consumerKey, consumerSecret);
        }
        // GET api/values
        [HttpGet]
        [ActionName ("GetAllSources")]
        public object GetAllSources () {
            var cities = @"[{""id"":""102"",""name"":""Chennai""},
            {""id"":""212"",""name"":""Attur""},
             {""id"":""132"",""name"":""Namakkal""},
              {""id"":""439"",""name"":""Namagiripettai""},
               {""id"":""247"",""name"":""Rasipuram""},
                {""id"":""1839"",""name"":""Salem""},
                  {""id"":""5690"",""name"":""Chinnasalem""},
                   {""id"":""1845"",""name"":""Senthamangalam""},
                    {""id"":""438"",""name"":""Belukurichi""},
                     {""id"":""438"",""name"":""Kalappanaickenpatti""},
                      {""id"":""450"",""name"":""Mohanur""}]";
            return cities;
        }

        [HttpGet]
        [ActionName ("GetAllDestinations")]
        public object GetAllDestinations (string cityId) {
            if (cityId == "102")
                return @"[{""id"":""212"",""name"":""Attur""},
             {""id"":""132"",""name"":""Namakkal""},
              {""id"":""439"",""name"":""Namagiripettai""},
               {""id"":""247"",""name"":""Rasipuram""},
                {""id"":""1839"",""name"":""Salem""},
                  {""id"":""5690"",""name"":""Chinnasalem""},
                   {""id"":""1845"",""name"":""Senthamangalam""},
                    {""id"":""438"",""name"":""Belukurichi""},
                     {""id"":""438"",""name"":""Kalappanaickenpatti""},
                      {""id"":""450"",""name"":""Mohanur""}]";
            else
                return @"[{""id"":""102"",""name"":""Chennai""}]";
        }

        [HttpGet]
        [ActionName ("GetAvailableTrips")]
        public object GetAvailableTrips (string sourceId, string destinationId, string doj) {

            return client.getAvailableTrips (sourceId, destinationId, doj);
        }

        [HttpGet]
        [ActionName ("GetTripDetails")]
        public object GetTripDetails (string tripId) {

            return client.getTripDetails (tripId);
        }

        [HttpGet]
        [ActionName ("BlockTicket")]
        public object BlockTicket (string ticket) {
            ticket=@"{""availableTripId"":""2000037026430089386"",""boardingPointId"":""218786"",""destination"":""439"",""inventoryItems"":[{""fare"":""735.00"",""ladiesSeat"":""false"",""passenger"":{""address"":""someaddress"",""age"":""28"",""email"":""san.pblr.gct@gmail.com"",""gender"":""MALE"",""idNumber"":""ID123"",""idType"":""PAN_CARD"",""mobile"":""8792346980"",""name"":""Santhosh"",""primary"":""true"",""title"":""Mr""},""seatName"":""VIP1""},{""fare"":""735.00"",""ladiesSeat"":""false"",""passenger"":{""age"":""28"",""email"":""san.pblr.gct@gmail.com"",""gender"":""MALE"",""mobile"":""8792346980"",""name"":""Santhosh"",""primary"":""false"",""title"":""Mr""},""seatName"":""VIP2""},{""fare"":""735.00"",""ladiesSeat"":""false"",""passenger"":{""age"":""28"",""email"":""san.pblr.gct@gmail.com"",""gender"":""MALE"",""mobile"":""8792346980"",""name"":""Santhosh"",""primary"":""false"",""title"":""Mr""},""seatName"":""VVIP""}],""source"":""102""}";
            return client.blockTicket (ticket);
        }
       
    }
}