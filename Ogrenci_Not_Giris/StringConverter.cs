using System;

public class StringConverter
{
    public static object ConvertToIntOrString(string input)
    {
        if (int.TryParse(input, out int result))
        {
            return result;
        }
        return input;
    }
} 