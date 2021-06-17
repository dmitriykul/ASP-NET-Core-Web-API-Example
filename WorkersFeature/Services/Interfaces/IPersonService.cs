using System.Collections.Generic;
using System.Threading.Tasks;
using WorkersFeature.Dtos;

namespace WorkersFeature.Services.Interfaces
{
    public interface IPersonService
    {
        /// <summary>
        /// Создаёт нового Person
        /// </summary>
        /// <param name="person">Dto модель Person для создания</param>
        /// <returns>Созданный Person</returns>
        public Task<PersonDto> Create(PersonDto person);
        
        /// <summary>
        /// Получить список всех Person
        /// </summary>
        /// <returns>Список всех Person</returns>
        public Task<List<PersonDto>> Get();
        
        /// <summary>
        /// Получить Person по id
        /// </summary>
        /// <param name="id">id Person для получения</param>
        /// <returns>Dto модель Person</returns>
        public Task<PersonDto> Get(int id);
        
        /// <summary>
        /// Редактировать отдельного Person
        /// </summary>
        /// <param name="personDto">Dto модель Person для редактирования</param>
        /// <returns>Отредактированный Person</returns>
        public Task<PersonDto> Edit(PersonDto personDto);
        
        /// <summary>
        /// Удалить Person по id
        /// </summary>
        /// <param name="id">id удаляемого Person</param>
        /// <returns>id удаляемого Person</returns>
        public Task<int> Delete(int id);
    }
}