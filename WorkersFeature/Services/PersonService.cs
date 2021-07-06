using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using WorkersFeature.Dtos;
using WorkersFeature.Models;
using WorkersFeature.Services.Interfaces;

namespace WorkersFeature.Services
{
    public class PersonService : IPersonService
    {
        private ApplicationContext _context;
        private ISkillService _skillService;
        private readonly ILogger<PersonService> _logger;

        public PersonService(ApplicationContext context, ISkillService skillService, ILogger<PersonService> logger)
        {
            _context = context;
            _skillService = skillService;
            _logger = logger;
        }
        
        public async Task<int> Create(PersonLessInfoDto person)
        {
            var personToAdd = ToModel(new PersonDto
            {
                Name = person.Name,
                DisplayName = person.DisplayName,
                Skills = person.Skills
            });
            var personAdded = await _context.Persons.AddAsync(personToAdd);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Created new Person object");

            return Convert.ToInt32(personAdded.Entity.Id);
        }

        public async Task<List<PersonDto>> Get()
        {
            var persons = await _context.Persons.Include(s => s.Skills).ToListAsync();

            return persons.Select(ToDto).ToList();
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
                
                foreach (SkillDto skill in personDto.Skills)
                {
                    await _skillService.Edit(skill);
                }
                
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