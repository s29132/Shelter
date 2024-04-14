using WebApplicationShelter.Models;

namespace WebApplicationShelter;

public class Visit
{
    public int Id { set; get; }
    public string Date { set; get; }
    public Animal Animal { set; get; }
    public string Description { set; get; }
    public double Price { set; get; }
}