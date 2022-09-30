using System.Net;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Kyc.Dtos;
using Kyc.Entities;
using Kyc.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kyc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AadharController : ControllerBase
    {
        private IMapper mapper;
        private readonly IValidator<Aadhar> validator;

        public AadharController(IMapper mapper,
                                IValidator<Aadhar> validator)
        {
            this.mapper = mapper;
            this.validator = validator;
        }


        /// <summary>
        /// This method is used to extract aadhar from raw input text.
        /// </summary>
        /// <param name="rawtext"></param>
        /// <returns>12 digit aadhar</returns>


        [HttpPost("findAadhar")]
        public IActionResult FindAadhar([FromBody] Request request)
        {
            var mappedAadhar = mapper.Map<Aadhar>(request);
            var validation = validator.Validate(mappedAadhar);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors?.Select(e => new Response()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = e.ErrorMessage

                }));
            }

            var aadharToProcess = request?.Rawtext.Replace(" ", "");
            bool isValidAadhar = CheckSumService.validateVerhoeff(num: aadharToProcess);
            return Ok(new Response()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = request.Rawtext.ToString()
            });
        }
    }
}