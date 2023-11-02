using DigitalSpace_TestTask.Entity;
using DigitalSpace_TestTask.Helpers;

namespace DigitalSpace_TestTask.Data;

public static class FakeBackend
{
    private static readonly Random _random = new();

    #region ChildGeneratorRegion

    private static ChildEntity GenerateChild(PersonEntity parent)
    {
        var child = new ChildEntity()
        {
            Id = DataProvider.GenerateId(),
            Gender = DataProvider.GenerateGender(),
            BirthDate = DataProvider.GenerateChildBirthdate(parent.BirthDate)
        };
            
        child.FirstName = DataProvider.GenerateFirstName(child.Gender);
        child.LastName = DataProvider.GenerateChildLastName(parent, child);
            
        return child;
    }

    private static ChildEntity[] GenerateChildArray(PersonEntity parent, int count)
    {
        var children = new ChildEntity[count];
            
        for (var i = 0; i < children.Length; i++)
        {
            children[i] = GenerateChild(parent);
        }
            
        return children;
    }

    #endregion

    #region PersonGeneratorRegion

    private static PersonEntity GeneratePerson()
    {
        var person = new PersonEntity
        {
            Id = DataProvider.GenerateId(),
            TransportId = Guid.NewGuid(),
            SequenceId = DataProvider.GenerateSequenceId(),
            CreditCardNumbers = DataProvider.GenerateCreditCardNumbers(),
            Phones = DataProvider.GeneratePhoneNumbers(),
            BirthDate = DataProvider.GeneratePersonBirthdate(),
            Salary = DataProvider.GenerateSalary(),
            IsMarred = DataProvider.GenerateMarred(),
            Gender = DataProvider.GenerateGender()
        };
            
        person.FirstName = DataProvider.GenerateFirstName(person.Gender);
        person.LastName = DataProvider.GenerateSecondName(person.Gender);
        person.Age = UnixConverter.YearsBetween(DateTime.UtcNow, person.BirthDate);
        person.Children = GenerateChildArray(person, _random.Next(6));

        return person;
    }
        
    public static PersonEntity[] GeneratePersonList(int count)
    {
        var persons = new PersonEntity[count];
            
        for (var i = 0; i < persons.Length; i++)
        {
            persons[i] = GeneratePerson();
        }
            
        return persons;
    }

    #endregion
}
