namespace DigitalSpace_TestTask.Helpers;

public readonly struct UnixConverter
{
    public long Value { get; }

    public UnixConverter(long posixValue)
    {
        Value = posixValue;
    }
        
    public static UnixConverter Parse(DateTime date)
    {
        return new UnixConverter((long)date.Subtract(DateTime.UnixEpoch).TotalSeconds);
    }
        
    public static DateTime ToDateTime(long posixValue)
    {
        return DateTime.UnixEpoch.AddSeconds(posixValue);
    }
        
    public static DateTime ToDateTime(UnixConverter posix)
    {
        return DateTime.UnixEpoch.AddSeconds(posix.Value);
    }

    public static int YearsBetween(DateTime firstDate, UnixConverter secondDate)
    {
        var secondDateTime = ToDateTime(secondDate);
            
        return Math.Abs((int)firstDate.Subtract(secondDateTime).TotalDays / 365);
    }
        
    public static int YearsBetween(DateTime firstDate, long posixValue)
    {
        return YearsBetween(firstDate, new UnixConverter(posixValue));
    }
        
    public static UnixConverter RandomDateBetween(DateTime firstDate, DateTime secondDate)
    {
        if (firstDate > secondDate)
        {
            (secondDate, firstDate) = (firstDate, secondDate);
        }
            
        var posixFromValue = Parse(firstDate).Value;
        var posixToValue = Parse(secondDate).Value;
            
        var posixValueRange  = Math.Abs(posixToValue - posixFromValue); 
        var maxRandomValueToAdd = posixValueRange > int.MaxValue
            ? int.MaxValue
            : (int)posixValueRange;
                
        var random = new Random();
            
        return new UnixConverter(posixFromValue + random.Next(maxRandomValueToAdd));
    }
}