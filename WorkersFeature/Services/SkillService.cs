using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkersFeature.Dtos;
using WorkersFeature.Models;
using WorkersFeature.Services.Interfaces;

namespace WorkersFeature.Services
{
    public class SkillService : ISkillService
    {
        private ApplicationContext _context;

        public SkillService(ApplicationContext context)
        {
            _context = context;
        }
        
        public async Task<SkillDto> Create(SkillDto skill)
        {
            var skillToAdd = ToModel(skill);
            await _context.Skills.AddAsync(skillToAdd);
            await _context.SaveChangesAsync();

            return skill;
        }

        public async Task<List<SkillDto>> Get()
        {
            var skills = await _context.Skills.ToListAsync();
            var result = new List<SkillDto>();

            foreach (Skill skill in skills)
            {
                result.Add(ToDto(skill));
            }

            return result;
        }

        public async Task<SkillDto> Get(int id)
        {
            var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == id);

            if (skill != null) return ToDto(skill);

            return null;
        }

        public async Task<SkillDto> Edit(SkillDto skillDto)
        {
            var existingSkill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == skillDto.Id);
            if (existingSkill != null)
            {
                existingSkill.Name = skillDto.Name;
                existingSkill.Level = skillDto.Level;
                existingSkill.PersonId = skillDto.PersonId;
                _context.Skills.Update(existingSkill);
                await _context.SaveChangesAsync();
            }

            return skillDto;
        }

        public async Task<int> Delete(int id)
        {
            var skillToDelete = await _context.Skills.FirstOrDefaultAsync(s => s.Id == id);

            if (skillToDelete == null) return 0;

            _context.Skills.Remove(skillToDelete);
            await _context.SaveChangesAsync();

            return id;
        }

        private Skill ToModel(SkillDto skill)
        {
            return new Skill
            {
                Name = skill.Name,
                Level = skill.Level,
                PersonId = skill.PersonId
            };
        }

        private SkillDto ToDto(Skill skill)
        {
            return new SkillDto
            {
                Id = skill.Id,
                Name = skill.Name,
                Level = skill.Level,
                PersonId = skill.PersonId
            };
        }
    }
}