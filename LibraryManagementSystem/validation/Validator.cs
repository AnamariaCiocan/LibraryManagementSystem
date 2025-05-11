namespace LibraryManagementSystem.validation;

public class Validator
{
    public static void checkEmptyString(string data)
    {
        if(data == null || data.Trim().Length == 0)
            throw new Exception("Information should not be empty");
    }

    public static void checkNumber(string data)
    {
        if (!data.All(char.IsDigit))
        {
            throw new Exception("The data should be a number");
        }
    }

    public static void checkPhoneNumber(string data)
    {
        if (data.Length != 10)
        {
            throw new Exception("Phone number can have only 10 digits");
        }

        if (!data.All(char.IsDigit))
        {
            throw new Exception("Phone number can have only digits");
        }
        
    }

    public static void checkFloat(string data)
    {
        if (!float.TryParse(data, out float result))
        {
            throw new Exception("The data should be a real number");
        }
    }

    public static void checkDate(string data)
    {
        if (!DateTime.TryParse(data, out DateTime result))
        {
            throw new Exception("The data should be following the specified format");
        }
    }
    
}