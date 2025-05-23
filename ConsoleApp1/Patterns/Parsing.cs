using System.Xml.Linq;
using System.Xml.Serialization;

namespace ConsoleApp1.Patterns;


public class Parsing
{
    public Parsing()
    {
        TestClass obj = new TestClass { Name = "Alice", Id = 25 };
        obj.Address.Zip = 6000;
        obj.Address.City = "Cebu";

        // Create an XmlSerializer instance
        XmlSerializer serializer = new XmlSerializer(typeof(TestClass));

        using (StreamWriter writer = new StreamWriter("output.xml"))
        {
            serializer.Serialize(writer, obj);
        }


        string filePath = "output.xml"; // The XML file previously created

        // Create an XmlSerializer for the same object type

        using (StreamReader reader = new StreamReader(filePath))
        {
            // Deserialize the XML back into a TestClass object
            obj = (TestClass)serializer.Deserialize(reader);
            Console.WriteLine($"Name: {obj.Name}, Age: {obj.Id}");
        }


        XDocument xmlDoc = XDocument.Load(filePath);

        // Query data using LINQ
        obj = xmlDoc.Descendants("TestClass")
                      .Select(x => new TestClass
                      {
                          Name = x.Element("Name").Value,
                          Id = int.Parse(x.Element("Id").Value),
                          Address = new Address
                          {
                              Zip = int.Parse(x.Element("Address")?.Element("Zip")?.Value),
                              City = x.Element("Address")?.Element("City")?.Value
                          }

                      })
                      .FirstOrDefault();

        Console.WriteLine($"Name: {obj.Name}, Age: {obj.Id}");




        Console.WriteLine("Object serialized to XML successfully.");
    }
}
public class TestClass
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; } = new();
}

