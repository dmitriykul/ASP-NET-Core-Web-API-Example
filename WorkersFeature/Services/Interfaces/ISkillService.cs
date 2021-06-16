using System.Collections.Generic;
using System.Threading.Tasks;
using WorkersFeature.Dtos;

namespace WorkersFeature.Services.Interfaces
{
    public interface ISkillService
    {
        /// <summary>
        /// Создаёт навык
        /// </summary>
        /// <param name="skill">Навык для создания</param>
        /// <returns>Созданный навык</returns>
        public Task<SkillDto> Create(SkillDto skill);
        
        /// <summary>
        /// Возвращает список всех навыков
        /// </summary>
        /// <returns>Список всех навыков</returns>
        public Task<List<SkillDto>> Get();
        
        /// <summary>
        /// Возвращает навык по id
        /// </summary>
        /// <param name="id">id навыка для получения</param>
        /// <returns>Навык</returns>
        public Task<SkillDto> Get(int id);

        /// <summary>
        /// Редавктировать навык по id
        /// </summary>
        /// <param name="skillDto">Навык для редактирования</param>
        /// <returns>Отредактированный навык</returns>
        public Task<SkillDto> Edit(SkillDto skillDto);
        
        /// <summary>
        /// Удалить навык по id
        /// </summary>
        /// <param name="id">id навыка для удаления</param>
        /// <returns></returns>
        public Task<int> Delete(int id);
    }
}