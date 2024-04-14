using WebApplicationShelter.Models;

namespace WebApplicationShelter;

public class DB
{
    public static List<Animal> Animals = new()
    {
        new Animal { Id = 1, Name = "Burek", Category = "pies", Mass = 25.5, Color = "Black" },
        new Animal { Id = 2, Name = "Puszek", Category = "kot", Mass = 7.2, Color = "Grey" },
        new Animal { Id = 3, Name = "Szarik", Category = "pies", Mass = 34.8, Color = "Brown" },
    };
    
    public static List<Visit> Visits = new()
    {
        new Visit { Date = "11.2.2023", Animal = DB.Animals[0], Description = "wizyta psa", Price = 123.2},
        new Visit { Date = "12.8.2023", Animal = DB.Animals[1], Description = "wizyta puszka", Price = 500.3},
        new Visit { Date = "10.3.2024", Animal = DB.Animals[0], Description = "wizyta kota", Price = 193.2}
    };
}