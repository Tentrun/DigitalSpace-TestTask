namespace DigitalSpace_TestTask.Entity;

public class PersonEntity
{
    public Int32 Id { get; set; }
    public Guid TransportId { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public Int32 SequenceId { get; set; }
    public String[] CreditCardNumbers { get; set; }
    public Int32 Age { get; set; }
    public String[] Phones { get; set; }
    public Int64 BirthDate { get; set; }
    public Double Salary { get; set; }
    public Boolean IsMarred { get; set; }
    public GenderEnum Gender { get; set; }
    public ChildEntity[] Children { get; set; }
}