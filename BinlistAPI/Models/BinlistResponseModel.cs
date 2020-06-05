﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinlistAPI.Models
{
    public class BinlistResponseModel
    {
        public string scheme { get; set; }
        public string type { get; set; }
        public string brand { get; set; }
        public bool prepaid { get; set; }
        public Country country { get; set; }
        public Bank bank { get; set; }
    }

    public class Bank
    {
        public string name { get; set; }
        public string url { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
    }

    public class Country
    {
        public string name { get; set; }
        public string currency { get; set; }
    }

    public class Number
    {
        public int length { get; set; }
        public bool luhn { get; set; }
    }
}