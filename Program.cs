using PhoneNumbers;
using System.Text.RegularExpressions;
public class Program
{
    public static void Main()
    {
        Console.WriteLine(SplitPhoneNumber("+881 999 999 9999"));
        Console.WriteLine(SplitPhoneNumber("+677 1234"));
        Console.WriteLine(SplitPhoneNumber("00966540259572"));
        Console.WriteLine(SplitPhoneNumber("+966540259572"));
        Console.WriteLine(SplitPhoneNumber("966540259572"));
        Console.WriteLine(SplitPhoneNumber("0540259572"));
        Console.WriteLine(SplitPhoneNumber("540259572"));

    }


    public static List<string> SplitPhoneNumber(string phoneNumber)
    {
        phoneNumber = phoneNumber.Replace(" ", "");
        var len = phoneNumber.Length;
        if (phoneNumber.StartsWith("00"))
        {
            phoneNumber = ReplaceDoubleZero(phoneNumber);
        }
        if (len == 12 && !phoneNumber.StartsWith("+"))
        {
            phoneNumber = "+" + phoneNumber;
        }
        if (len == 10 && phoneNumber.StartsWith("0"))
        {
            phoneNumber = ReplaceZeroWithCountryCode(phoneNumber);
        }
        if (len == 9 && !phoneNumber.StartsWith("0"))
        {
            phoneNumber = "0" + phoneNumber;
            phoneNumber = ReplaceZeroWithCountryCode(phoneNumber);
        }
        List<string> result = new List<string>();
        // Create an instance of PhoneNumberUtil
        var phoneUtil = PhoneNumberUtil.GetInstance();

        // Parse the phone number with the country code
        var number = phoneUtil.Parse(phoneNumber, DetectCountryCode(phoneNumber));
        var code = number.CountryCode.ToString();
        var numberX = number.NationalNumber.ToString();
        result.Add(code);
        result.Add(numberX);



        // Format the phone number in E.164 format
        //var formattedNumber = phoneUtil.Format(number, PhoneNumberFormat.E164);

        // Return the formatted number
        // return formattedNumber;
        return result;
    }
    public static string ReplaceZeroWithCountryCode(string phoneNumber)
    {
        // Create a regex pattern to match the zero at the beginning
        var pattern = @"^0";

        // Create a regex object with the pattern
        var regex = new Regex(pattern);

        // Check if the phone number matches the pattern
        if (regex.IsMatch(phoneNumber))
        {
            // Replace the zero with +966 and return the new number
            return regex.Replace(phoneNumber, "+966");
        }

        // Otherwise, return the original number
        else
        {
            return phoneNumber;
        }
    }
    public static string ReplaceDoubleZero(string phoneNumber)
    {
        // Create a regex pattern to match the zero at the beginning
        var pattern = @"^00";

        // Create a regex object with the pattern
        var regex = new Regex(pattern);

        // Check if the phone number matches the pattern
        if (regex.IsMatch(phoneNumber))
        {
            // Replace the zero with +966 and return the new number
            return regex.Replace(phoneNumber, "+");
        }

        // Otherwise, return the original number
        else
        {
            return phoneNumber;
        }
    }

    public static string DetectCountryCode(string phoneNumber)
    {
        // Create a regex pattern to match the country code
        var pattern = @"^\+?(\d{1,3})";

        // Create a regex object with the pattern
        var regex = new Regex(pattern);

        // Try to match the phone number with the pattern
        var match = regex.Match(phoneNumber);

        // If there is a match, return the country code
        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        // Otherwise, return an empty string
        else
        {
            return "";
        }
    }

}