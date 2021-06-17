using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkersFeature.Dtos;
using WorkersFeature.Services.Interfaces;

namespace WorkersFeature.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SkillController : ControllerBase
    {
        private ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }
        
        /// <summary>
        /// Добавить новый навык
        /// </summary>
        /// <param name="skill">Dto модель навыка для добавления</param>
        /// <returns>Добавленный навык</returns>
        [HttpPost]
        public async Task<SkillDto> Post(SkillDto skill)
        {
            await _skillService.Create(skill);

            return skill;
        }
        
        /// <summary>
        /// Получить список всех навыков
        /// </summary>
        /// <returns>Список навыков</returns>
        [HttpGet]
        public async Task<IEnumerable<SkillDto>> Get()
        {
            return await _skillService.Get();
        }
        
        /// <summary>
        /// Получить навык по id
        /// </summary>
        /// <param name="id">id навыка для получения</param>
        /// <returns>dto модель навыка с Id = id</returns>
        [HttpGet("{id}")]
        public async Task<SkillDto> Get(int id)
        {
            var skill = await _skillService.Get(id);

            return skill;
        }
        
        /// <summary>
        /// Обновить навык
        /// </summary>
        /// <param name="skill">dto модель навыка для обновления</param>
        /// <returns>Обновленный навык</returns>
        [HttpPut]
        public async Task<SkillDto> Put(SkillDto skill)
        {
            await _skillService.Edit(skill);

            return skill;
        }
        
        /// <summary>
        /// Удалить навык по id
        /// </summary>
        /// <param name="id">id навыка для удаления</param>
        /// <returns>id удаленного навыка</returns>
        [HttpDelete]
        public async Task<int> Delete(int id)
        {
            return await _skillService.Delete(id);
        }
    }
}