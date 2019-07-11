using System;
using Newtonsoft.Json;

public class Account
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DOB { get; set; }
}

namespace Test
{
    class Program
    {
        static Account account = new Account
        {
            Name = "John Doe",
            Email = "john@microsoft.com",
            DOB = new DateTime(1980, 2, 20, 0, 0, 0, DateTimeKind.Utc),
        };

        static string json = JsonConvert.SerializeObject(account, Formatting.Indented);

        static void Main(string[] args)
        {
            Consewfole.WriteLine(account.Name);
            Console.WriteLine("Hello World!");
            Console.WriteLine(json);
        }
    }
}
