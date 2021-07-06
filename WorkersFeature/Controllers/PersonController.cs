using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public async Task<int> Post(PersonLessInfoDto person)
        {
            return await _personService.Create(person);
        }
        
        /// <summary>
        /// Получить список всех работников
        /// </summary>
        /// <returns>Список из объектов PersonDto</returns>
        [HttpGet]
        public async Task<ActionResult<List<PersonDto>>> Get()
        {
            var result = await _personService.Get();
            if (result.Count == 0) return NotFound();
            
            return result;
        }
        
        /// <summary>
        /// Получить работника по id
        /// </summary>
        /// <param name="id">id работника</param>
        /// <returns>PersonDto</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> Get(int id)
        {
            var result = await _personService.Get(id);
            if (result == null) return NotFound();

            return result;
        }

        /// <summary>
        /// Обновить работника
        /// </summary>
        /// <param name="person">Работник для обновления</param>
        /// <returns>PersonDto</returns>
        [HttpPut]
        public async Task<ActionResult<PersonDto>> Put(PersonDto person)
        {
            var result = await _personService.Edit(person);
            if (result == null) return NotFound();
            
            return Ok(result);
        }

        /// <summary>
        /// Удалить работника по id
        /// </summary>
        /// <param name="id">id работника для удаления</param>
        /// <returns>id удалённого работника</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var idDeleted = await _personService.Delete(id);
            if (idDeleted == 0) return NotFound();
            
            return idDeleted;
        }
    }
}