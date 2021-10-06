using AutoMapper;
using HotelListing.Data;
using HotelListing.IRepository;
using HotelListing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;
        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitOfwork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
        {
            try
            {
                var countries = await _unitOfwork.countries.GetPagedList(requestParams);
                var result = _mapper.Map<IList<CountryDTO>>(countries);
                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somth went wrong in {nameof(GetCountries)}");
                return StatusCode(500, $"serv error");
            }
        }
        [HttpGet("{id:int}", Name = "GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var country = await _unitOfwork.countries.Get(q => q.Id == id, new List<string> { "Hotels" });
                var result = _mapper.Map<CountryDTO>(country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetCountry)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Something Went Wrong in the {nameof(CreateCountry)}");
                return BadRequest(ModelState);
            }
            try
            {
                var country = _mapper.Map<Country>(countryDTO);
                await _unitOfwork.countries.Insert(country);
                await _unitOfwork.Save();

                return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something Went Wrong in the {nameof(CreateCountry)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");

            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Something Went Wrong in the {nameof(UpdateCountry)}");
                return BadRequest(ModelState);
            }
            try
            {
                var country = await _unitOfwork.countries.Get(q => q.Id == id);
                if (country == null)
                {
                    _logger.LogError($"Something Went Wrong in the {nameof(UpdateCountry)}");
                    return BadRequest("submitted dt is inc");
                }
                _mapper.Map(countryDTO, country);
                _unitOfwork.countries.Update(country);
                await _unitOfwork.Save();

                return NoContent();
            }


            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something Went Wrong in the {nameof(UpdateCountry)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");

            }

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Something Went Wrong in the {nameof(DeleteCountry)}");
                return BadRequest();

            }
            try
            {
                var country = await _unitOfwork.countries.Get(q => q.Id == id);
                if (country == null)
                {
                    _logger.LogError($"Something Went Wrong in the {nameof(DeleteCountry)}");
                    return BadRequest("Submitted data is invalid");

                }
                await _unitOfwork.countries.Delete(id);
                await _unitOfwork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Something Went Wrong in the {nameof(DeleteCountry)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");

            }
        }
    }
}
