using Microsoft.AspNetCore.Mvc;
using WebApplicationShelter.Models;

namespace WebApplicationShelter.Controllers;

[Route(template:"api/animals")]
[ApiController]
public class AnimalsController : ControllerBase

{
    [HttpGet]
    public IActionResult GetAnimals()
    {
        return Ok(DB.Animals);
    }

    [HttpGet("id : int")]
    public IActionResult GetAnimal(int id)
    {
        var animal = DB.Animals.FirstOrDefault(animal => animal.Id == id);
        return animal == null ? NotFound($"Animal with Id {id} was not found") : Ok(animal);
    }

    [HttpPost]
    public IActionResult PostAnimal(Animal animal)
    {
        var animalResult = DB.Animals.FirstOrDefault(animalFromDb => animalFromDb.Id == animal.Id);
        if (animalResult != null)
        {
            return Conflict($"Animal with id {animal.Id} is already present in the database");
        }
        DB.Animals.Add(animal);
        return Created($"api/animals/{animal.Id}", animal);
    }
    
    [HttpPatch]
    [Route("api/animals/{id}")]
    public IActionResult UpdateAnimal(int id, [FromBody] Animal animalPatch)
    {
        var existingAnimal = DB.Animals.FirstOrDefault(animal => animal.Id == id);
        if (existingAnimal == null)
        {
            return NotFound();
        }

        DB.Animals.Remove(existingAnimal);
        
        var animalPatched = ApplyPatchData(existingAnimal, animalPatch);
        DB.Animals.Add(animalPatched);
        
        return Ok("Animal updated successfully");
    }
    
    private Animal ApplyPatchData(Animal existingAnimal, Animal animalPatch)
    {
        if (animalPatch.Name != "")
        {
            existingAnimal.Name = animalPatch.Name;
        }
        if (animalPatch.Category != "")
        {
            existingAnimal.Category = animalPatch.Category;
        }

        if (animalPatch.Mass != 0)
        {
            existingAnimal.Mass = animalPatch.Mass;
        }

        if (animalPatch.Color != "")
        {
            existingAnimal.Color = animalPatch.Color;
        }

        return existingAnimal;
    }

    [HttpDelete("id:int")]
    public IActionResult DeleteAnimal(int id)
    {
        var animal = DB.Animals.FirstOrDefault(animal => animal.Id == id);
        if (animal == null)
        {
            return NotFound($"Animal with Id {id} was not found");
        }
        DB.Animals.Remove(animal);
        return Ok($"Animal with id {id} was successfully deleted from the database.");
    }
    
    [HttpGet("/visits/id : int")]
    public IActionResult GetAnimalVisits(int id)
    {
        var animal = DB.Animals.FirstOrDefault(animal => animal.Id == id);
        if (animal == null)
        {
            return NotFound($"Animal with Id {id} was not found");
        }
        //var visit = DB.Visits.FirstOrDefault(visit => visit.Animal == animal);
        var visits = DB.Visits.Where(visit => visit.Animal == animal).ToList();
        if (visits.Count == 0)
        {
            return NotFound($"Visits with animal Id {id} were not found");
        }

        return Ok(visits);
    }
    
    [HttpPost("/visits/")]
    public IActionResult PostVisit(Visit visit)
    {
        var visitResult = DB.Animals.FirstOrDefault(visitFromDb => visitFromDb.Id == visit.Id);
        if (visitResult != null)
        {
            return Conflict($"Visit with id {visit.Id} is already present in the database");
        }
        DB.Visits.Add(visit);
        return Created($"api/animals/visits/{visit.Id}", visit);
    }
    
    
    
    
}