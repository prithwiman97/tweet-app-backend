using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace com.tweetapp.Utilities
{
    class Validation
    {
        public static bool Email(string email)
        {
            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            return Regex.IsMatch(email, pattern);
        }
        public static bool Username(string username)
        {
            string pattern = @"^[a-zA-Z0-9.]{8,20}$";
            return Regex.IsMatch(username, pattern);
        }
        public static bool Password(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$";
            return Regex.IsMatch(password, pattern);
        }
        public static bool ValidateGender(string gender)
        {
            if (gender.Equals(Gender.Male + "") || gender.Equals(Gender.Female + "") || gender.Equals(Gender.Other + ""))
                return true;
            return false;
        }
        public static string PasswordErrorMessage()
        {
            return "Your password must meet the following:\n*At least one digit [0-9]\n*At least one lowercase character[a - z]\n*At least one uppercase character[A - Z]\n*At least 8 characters in length";
        }
    }
}
