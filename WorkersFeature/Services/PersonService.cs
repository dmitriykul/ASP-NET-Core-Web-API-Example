using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkersFeature.Dtos;
using WorkersFeature.Models;
using WorkersFeature.Services.Interfaces;

namespace WorkersFeature.Services
{
    public class PersonService : IPersonService
    {
        private ApplicationContext _context;
        private ISkillService _skillService;

        public PersonService(ApplicationContext context, ISkillService skillService)
        {
            _context = context;
            _skillService = skillService;
        }
        
        public async Task<PersonDto> Create(PersonDto person)
        {
            var personToAdd = ToModel(person);
            await _context.Persons.AddAsync(personToAdd);
            await _context.SaveChangesAsync();

            return person;
        }

        public async Task<List<PersonDto>> Get()
        {
            var persons = await _context.Persons.Include(s => s.Skills).ToListAsync();
            var result = new List<PersonDto>();

            foreach (Person person in persons)
            {
                result.Add(ToDto(person));
            }

            return result;
        }

        public async Task<PersonDto> Get(int id)
        {
            var person = await _context.Persons.Include(s => s.Skills).FirstOrDefaultAsync(p => p.Id == id);

            if (person != null) return ToDto(person);

            return null;
        }

        public async Task<PersonDto> Edit(PersonDto personDto)
        {
            var existingPerson = await _context.Persons.FirstOrDefaultAsync(p => p.Id == personDto.Id);
            if (existingPerson != null)
            {
                existingPerson.Name = personDto.Name;
                existingPerson.DisplayName = personDto.DisplayName;
                _context.Persons.Update(existingPerson);
                await _context.SaveChangesAsync();
            }

            return personDto;
        }

        public async Task<int> Delete(int id)
        {
            var personToDelete = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);

            if (personToDelete == null) return 0;

            _context.Persons.Remove(personToDelete);
            await _context.SaveChangesAsync();

            return id;
        }
        
        private Person ToModel(PersonDto person)
        {
            return new Person
            {
                Name = person.Name,
                DisplayName = person.Name,
                Skills = _skillService.ToListModel(person.Skills)
            };
        }
        
        private PersonDto ToDto(Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                DisplayName = person.DisplayName,
                Skills = _skillService.ToListDto(person.Skills)
            };
        }
    }
}