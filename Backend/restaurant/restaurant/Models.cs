﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace restrauntBooking
{
    public class RestaurantUser
    {
        [JsonIgnore]
        [Key]
        public int userid { get; set; }
        public string usertype { get; set; }

        public string email { get; set; }

        public string password { get; set; }
    }


    public class LoginRequest
    {
        public string usertype { get; set; }

        public string email { get; set; }

        public string password { get; set; }
    }

    public class Table
    {
        [Key]
        public int tableid { get; set; }
        public string tablename { get; set; }

        public string status { get; set; }
    }

    public class TableRequest
    {
        public string tablename { get; set; }
        public string status { get; set; }

    }

    public class TableBooking
    {
        [JsonIgnore]
        [Key]
        public int bookingid { get; set; } // Primary key, auto-increment
        public int tableid { get; set; } 
        public string custname { get; set; }
    }






}