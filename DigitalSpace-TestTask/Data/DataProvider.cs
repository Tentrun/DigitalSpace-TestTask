using System.Text;
using DigitalSpace_TestTask.Entity;
using DigitalSpace_TestTask.Helpers;

namespace DigitalSpace_TestTask.Data;

public class DataProvider
{
    private static readonly Random _random = new();

    private static readonly string[] _maleFirstNames = { "Ivan", "Alexey", "Kirill", "Vladimir", "Vladislav" };

    private static readonly string[] _femaleFirstNames = { "Anastasia", "Olga", "Victoria", "Veronika", "Angelina" };

    private static readonly string[] _lastNames = { "Kryglov", "Potemkin", "Evihin", "Hmelevskii", "Tarov" };

    private static List<int> _ids;
    
    private static int MaxIds = 10000;
    public static int GenerateId()
    {
        if (_ids == null)
        {
            var ids = new int[MaxIds];
            for (int i = 0; i < MaxIds; i++)
            {
                ids[i] = i;
            }

            _ids = ids.ToList();
        }

        var removeElementNumber = _random.Next(0, _ids.Count);
        var value = _ids[removeElementNumber];
        _ids.Remove(removeElementNumber);

        return value;
    }

    public static string GenerateFirstName(GenderEnum gender)
    {
        return gender switch
        {
            GenderEnum.Male => _maleFirstNames[_random.Next(0, _maleFirstNames.Length)],
            GenderEnum.Female => _femaleFirstNames[_random.Next(0, _maleFirstNames.Length)],
            _ => throw new Exception("Настигла проблема 21-го века :)")
        };
    }

    public static string GenerateSecondName(GenderEnum gender)
    {
        return gender switch
        {
            GenderEnum.Male => _lastNames[_random.Next(0, _lastNames.Length)],
            GenderEnum.Female => _lastNames[_random.Next(0, _lastNames.Length)] + "a",
            _ => throw new Exception("Настигла проблема 21-го века :)")
        };
    }

    public static string GenerateChildLastName(PersonEntity parent, ChildEntity child)
    {
        if (parent.Gender != child.Gender)
        {
            return parent.Gender == GenderEnum.Male 
                ? $"{parent.LastName}a" 
                : parent.LastName.Substring(0, parent.LastName.Length - 1);
        }
            
        return parent.LastName;
    } 
    
    public static int GenerateSequenceId()
    {
        return _random.Next(1, 151);
    }
    
    public static string[] GenerateCreditCardNumbers()
    {
        var cardsNumbers = new string[_random.Next(1, 6)];

        for (var i = 0; i < cardsNumbers.Length; i++)
        {
            cardsNumbers[i] = GenerateCardNumber();
        }
        
        return cardsNumbers;
    }
    
    public static string[] GeneratePhoneNumbers()
    {
        var phoneNumbers = new string[_random.Next(1, 6)];
        
        for (var i = 0; i < phoneNumbers.Length; i++)
        {
            phoneNumbers[i] = GeneratePhoneNumber();
        }
            
        return phoneNumbers.ToArray();
    }

    public static long GeneratePersonBirthdate()
    {
        var dateNow = DateTime.UtcNow;
        var dateFrom = dateNow.Subtract(TimeSpan.FromDays(365 * 50));
        var dateTo = dateNow.Subtract(TimeSpan.FromDays(365 * 16));
            
        return UnixConverter.RandomDateBetween(dateFrom, dateTo).Value;
    }
    
    public static long GenerateChildBirthdate(long parentBirthdate)
    {
        var dateFrom = UnixConverter
            .ToDateTime(parentBirthdate)
            .Add(TimeSpan.FromDays(365 * 15));
            
        return UnixConverter.RandomDateBetween(dateFrom, DateTime.UtcNow).Value;
    }
    
    public static double GenerateSalary()
    {
        return Math.Round(_random.NextDouble() * 10000, 2);
    }
    
    public static bool GenerateMarred()
    {
        return _random.Next(0, 2) == 1;
    }
    
    public static GenderEnum GenerateGender()
    {
        return _random.Next(2) == 0
            ? GenderEnum.Male
            : GenderEnum.Female;
    }
    
    private static string GenerateCardNumber()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < 16; i++)
        {
            sb.Append(_random.Next(0, 10));
        }

        return sb.ToString();
    }
    
    private static string GeneratePhoneNumber()
    {
        var sb = new StringBuilder("+79");
        for (var i = 1; i < 10; i++)
        {
            sb.Append(_random.Next(1, 10));
        }

        return sb.ToString();
    }
}