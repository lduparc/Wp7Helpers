using System;
using System.Windows.Data;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;


namespace com.wp.helpers {
    
    public class Validators
    {
        public enum ValidationPatterns
        { 
            email,
            icq,
            creditcard,
            english,
            url,
            IPv4,
            IPv6,
            name
        }

        public static bool Validate(string inputStr, ValidationPatterns pattern)
        {
            if (!string.IsNullOrEmpty(inputStr))
            {
                Regex reg = new Regex(GetPatternString(pattern));
                return reg.IsMatch(inputStr);
            }
            else
                return false;
        }

        static string GetPatternString(ValidationPatterns pattern)
        {
            switch (pattern)
            {
                case ValidationPatterns.icq:
                    {
                        return @"([1-9])+(?:-?\d){4,}";
                    }
                case ValidationPatterns.creditcard:
                    {
                        return @"[0-9]{13,16}";
                    }
                case ValidationPatterns.english:
                    {
                        return @"^[a-zA-Z0-9]+$";                    
                    }
                case ValidationPatterns.url:
                    {
                       return @"^([a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,6}$"; 
                    }
                case ValidationPatterns.IPv4:
                    {
                        return  @"((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)";
                    }
                case ValidationPatterns.IPv6:
                    {
                        return @"((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)";
                    }
                case ValidationPatterns.name:
                    {                        
                        return @"^[a-zA-Z0-9-_\.]+$";                           
                    }
                case ValidationPatterns.email:
                    {
                        return @"^[a-zA-Z0-9-_\.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$";
                    }
                default:
                    return null;
            }
        
        }
    }
}