public class MaritalStatusCriteria : ICriteria<Person>
{
    private readonly string status;
    public MaritalStatusCriteria(string status)
    {
        this.status = status;
    }
    public IEnumerable<Person> MeetCriteria(IEnumerable<Person> collections)
    {
        return collections.Where(x => x.MaritalStatus == status);
    }
    public Func<Person, bool> Predicate() => x => x.MaritalStatus == status;
}
public class GenderCriteria : ICriteria<Person>
{
    private readonly string gender;
    public GenderCriteria(string gender)
    {
        this.gender = gender;
    }
    public Func<Person, bool> Predicate() => x => x.Gender == gender;
    public IEnumerable<Person> MeetCriteria(IEnumerable<Person> collections)
    {
        return collections.Where(x => x.Gender == gender);
    }
}
public class AndPersonCriteria : ICriteria<Person>
{
    private readonly string gender;
    private readonly string mstatus;

    public AndPersonCriteria(string gender, string mstatus)
    {
        this.gender = gender;
        this.mstatus = mstatus;
    }
    public Func<Person, bool> Predicate() => x => x.Gender == gender && x.MaritalStatus == mstatus;
    public IEnumerable<Person> MeetCriteria(IEnumerable<Person> collections)
    {
        return collections.Where(x => x.Gender == gender && x.MaritalStatus == mstatus);
    }
}
public class ORPersonCriteria : ICriteria<Person>
{
    private readonly string gender;
    private readonly string mstatus;

    public ORPersonCriteria(string gender, string mstatus)
    {
        this.gender = gender;
        this.mstatus = mstatus;
    }
    public Func<Person, bool> Predicate() => x => x.Gender == gender || x.MaritalStatus == mstatus;
    public IEnumerable<Person> MeetCriteria(IEnumerable<Person> collections)
    {
        return collections.Where(x => x.Gender == gender || x.MaritalStatus == mstatus);
    }
}
public class DynamicPersonCriteria<T> : ICriteria<T>
{
    private readonly Func<T, bool> predicate;
    public DynamicPersonCriteria(Func<T, bool> predicate)
    {
        this.predicate = predicate;
    }
    public IEnumerable<T> MeetCriteria(IEnumerable<T> collections)
    {
        return collections.Where(predicate);
    }
}
public class PersonCriteriaHandler : ICriteriaHandler
{
    public IEnumerable<T> Apply<T>(ICriteria<T> criteria, IEnumerable<T> models)
    {
        return criteria.MeetCriteria(models);
    }
}
public class NoCriteriaHandler : ICriteriaHandler
{
    public IEnumerable<T> Apply<T>(ICriteria<T> criteria, IEnumerable<T> models)
    {
        return criteria.MeetCriteria(models).ToList();
    }
}

public class NotCriteria<T> : ICriteria<T>
{
    private Func<T, bool> expression;
    public NotCriteria(Func<T, bool> expression)
    {
        this.expression = expression;
    }
    public IEnumerable<T> MeetCriteria(IEnumerable<T> collections)
    {
        return collections.Where(x => !expression(x));
    }
}
public interface ICriteria<T>
{
    IEnumerable<T> MeetCriteria(IEnumerable<T> collections);
}
public interface ICriteriaHandler
{
    public IEnumerable<T> Apply<T>(ICriteria<T> criteria, IEnumerable<T> models);
}
public class CriteriaFactory
{
    public static ICriteriaHandler Create(string type)
    {
        if (type == "Person")
        {
            return new PersonCriteriaHandler();
        }
        return new NoCriteriaHandler();
    }
}




public class FilterCriteriaDP
{
    public void Run()
    {
        // Sample data
        List<Person> persons = new List<Person>
        {
            new Person("John", "Male", "Single"),
            new Person("Sarah", "Female", "Married"),
            new Person("Mike", "Male", "Single"),
            new Person("Linda", "Female", "Single"),
            new Person("James", "Male", "Married"),
            new Person("James1", "Male", "Married"),
            new Person("James2", "Male", "Married"),
            new Person("James3", "Male", "Married"),
            new Person("James4", "Male", "Married"),
            new Person("James5", "Male", "Married"),
            new Person("James6", "Male", "Married"),
            new Person("James7", "Male", "Married"),
            new Person("James8", "Male", "Married"),
            new Person("James9", "Male", "Married"),
            new Person("James10", "Male", "Married"),
        };

        //filter handler can accept either concrete or generic
        //using  concrete filter
        ICriteriaHandler handler = CriteriaFactory.Create("Person");
        var criteria = new AndPersonCriteria("Female", "Single");

        var notcriteria = new NotCriteria<Person>(x => x.Gender == "Female");
        var result = handler.Apply(notcriteria, persons);

        foreach (Person person in result)
        {
            Console.WriteLine(person);
        }

        //generic
        //using dynamic filter
        var criteriax = new DynamicPersonCriteria<Person>(x => x.MaritalStatus == "Single");
        var data = handler.Apply(criteriax, persons);
        List<Company> companies = new();
        ICriteria<Company> companyFilter = new DynamicPersonCriteria<Company>(x => x.Name == "xx");
        var datax = handler.Apply(companyFilter, companies);
        foreach (var item in data)
        {
            Console.WriteLine(item.Name);
        }


    }
}
public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public string MaritalStatus { get; set; }
    public Person()
    {
    }
    public override string ToString()
    {
        return string.Concat(Name, "/", Gender, "/", MaritalStatus);
    }

    public Person(string name, string gender, string maritalStatus)
    {
        Name = name;
        Gender = gender;
        MaritalStatus = maritalStatus;
    }
}
public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
}