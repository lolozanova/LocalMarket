﻿
namespace LocalMarket.Data
{
    public class DataConstants
    {

        public class Product
        {
            public const int NameMaxLength = 25;

            public const int DescriptionMaxLength = 1000;

            public const int DescriptionMinLength = 25;

            public const string PriceMinValue = "0.01";

            public const string PriceMaxValue = "79228162514264337593543950335";

        }

        public class Producer
        {
            public const int NameMaxLength = 25;

            public const int NameMinLength = 3;

            public const string PhoneNumberRegex = @"^+359[0-9]{9}$";

        }

        public class Common
        {
            public const int NameMaxLength = 10;

        }
        
    }
}
