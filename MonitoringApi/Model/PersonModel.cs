namespace MonitoringApi.Model;

public record PersonModel(string? Name, string LastName, DateTime UpdateTime);

//public class PersonModel
//{
//    public PersonModel(string? name, string lastName)
//    {
//        Name = name;
//        LastName = lastName;
//    }

//    public string? Name { get; set; }
//    public string LastName { get; set; }
//}