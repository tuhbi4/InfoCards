using InfoCards.Common.Entities;
using InfoCards.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace InfoCards.BLL.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfoCardController : ControllerBase
    {
        private readonly IRepository<InfoCard> _cardRepository;
        private readonly ILogger<InfoCardController> _logger;

        public InfoCardController(IRepository<InfoCard> cardRepository, ILogger<InfoCardController> logger)
        {
            _cardRepository = cardRepository;
            _logger = logger;
        }

        //Get all infocards
        //api/infoCards
        [HttpGet]
        public IActionResult Get()
        {
            _cardRepository.ReadAll();
            IEnumerable<InfoCard> infoCards = _cardRepository.GetAll();
            return Ok(infoCards);
        }

        //Get infocard by Id
        //Get: api/infoCard/3
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            _cardRepository.ReadAll();
            InfoCard infoCard = _cardRepository.Read(id);

            if (infoCard == null)
            {
                return NotFound("InfoCard record could not be found");
            }

            return Ok(infoCard);
        }

        //Post: api/infoCard
        [HttpPost]
        public IActionResult Post([FromBody] InfoCard infoCard)
        {
            _cardRepository.ReadAll();

            if (infoCard == null)
            {
                return BadRequest("InfoCard is Null");
            }

            _cardRepository.Create(infoCard);
            _cardRepository.Save(); // perhaps a response code is needed?

            return CreatedAtRoute("Get", new { Id = infoCard.Id }, infoCard);
        }

        //PUT: /infoCard/3
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] InfoCard infoCard)
        {
            _cardRepository.ReadAll();

            if (infoCard == null)
            {
                return BadRequest("InfoCard is Null");
            }

            if (_cardRepository.Read(id) == null)
            {
                return NotFound("InfoCard record could not be found");
            }

            infoCard.Id = id;
            _cardRepository.Update(infoCard);
            _cardRepository.Save(); // perhaps a response code is needed?

            return Ok(infoCard);
        }

        //Delete: /infoCard/3
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _cardRepository.ReadAll();
            InfoCard infoCard = _cardRepository.Read(id);

            if (infoCard == null)
            {
                return NotFound("InfoCard record could not be found");
            }

            _cardRepository.Delete(id);
            _cardRepository.Save(); // perhaps a response code is needed?

            return Ok(infoCard);
        }
    }
}