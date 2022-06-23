using InfoCards.Common.Entities;
using InfoCards.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InfoCards.BLL.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfoCardController : ControllerBase
    {
        private readonly IRepository<InfoCard> _cardRepository;

        public InfoCardController(IRepository<InfoCard> cardRepository)
        {
            _cardRepository = cardRepository;
        }

        //Get all InfoCards
        //GET: api/InfoCard
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<InfoCard> infoCards = _cardRepository.GetAll();

            return Ok(infoCards);
        }

        //Get InfoCard by Id
        //GET: api/InfoCard/3
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            InfoCard infoCard = _cardRepository.Read(id);

            if (infoCard == null)
            {
                return NotFound("InfoCard record could not be found");
            }

            return Ok(infoCard);
        }

        //POST: api/InfoCard
        [HttpPost]
        public IActionResult Post([FromBody] InfoCard infoCard)
        {
            if (ModelState.IsValid)
            {
                if (infoCard == null)
                {
                    return BadRequest("InfoCard is Null");
                }

                if (!InfoCardControllerHelpers.IsBase64String(infoCard.ImageBase64))
                {
                    return BadRequest("Image is not Base64");
                }

                _cardRepository.Create(infoCard);

                return CreatedAtRoute("Get", new { Id = infoCard.Id }, infoCard);
            }

            return BadRequest("Validation error");
        }

        //PUT: /InfoCard/3
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] InfoCard infoCard)
        {
            if (ModelState.IsValid)
            {
                if (infoCard == null)
                {
                    return BadRequest("InfoCard is Null");
                }

                if (!InfoCardControllerHelpers.IsBase64String(infoCard.ImageBase64))
                {
                    return BadRequest("Image is not Base64");
                }

                if (_cardRepository.Read(id) == null)
                {
                    return NotFound("InfoCard record could not be found");
                }

                infoCard.Id = id;
                _cardRepository.Update(infoCard);

                return Ok(infoCard);
            }

            return BadRequest("Validation error");
        }

        //DELETE: /InfoCard/3
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            InfoCard infoCard = _cardRepository.Read(id);

            if (infoCard == null)
            {
                return NotFound("InfoCard record could not be found");
            }

            _cardRepository.Delete(id);

            return Ok(infoCard);
        }
    }
}