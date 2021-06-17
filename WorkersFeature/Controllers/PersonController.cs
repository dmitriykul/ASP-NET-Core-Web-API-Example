using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkersFeature.Dtos;
using WorkersFeature.Services;
using WorkersFeature.Services.Interfaces;

namespace WorkersFeature.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PersonController : ControllerBase
    {
        private IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        
        /// <summary>
        /// Добавить нового работника
        /// </summary>
        /// <param name="person">Объект PersonDto</param>
        /// <returns>PersonDto</returns>
        [HttpPost]
        public async Task<PersonDto> Post(PersonDto person)
        {
            await _personService.Create(person);

            return person;
        }
        
        /// <summary>
        /// Получить список всех работников
        /// </summary>
        /// <returns>Список из объектов PersonDto</returns>
        [HttpGet]
        public async Task<List<PersonDto>> Get()
        {
            return await _personService.Get();
        }
        
        /// <summary>
        /// Получить работника по id
        /// </summary>
        /// <param name="id">id работника</param>
        /// <returns>PersonDto</returns>
        [HttpGet("{id}")]
        public async Task<PersonDto> Get(int id)
        {
            return await _personService.Get(id);
        }

        /// <summary>
        /// Обновить работника
        /// </summary>
        /// <param name="person">Работник для обновления</param>
        /// <returns>PersonDto</returns>
        [HttpPut]
        public async Task<PersonDto> Put(PersonDto person)
        {
            return await _personService.Edit(person);
        }

        /// <summary>
        /// Удалить работника по id
        /// </summary>
        /// <param name="id">id работника для удаления</param>
        /// <returns>id удалённого работника</returns>
        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await _personService.Delete(id);
        }
    }
}