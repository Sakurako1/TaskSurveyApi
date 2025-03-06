using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyApi.Dto;
using SurveyApi.Interfaces;
using SurveyApi.Models;

namespace SurveyApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly IRepositorySurvey _repository;
        private readonly IMapper _mapper;
        public SurveyController(IRepositorySurvey repository, IMapper mapper) { _repository = repository; _mapper = mapper; }

        [HttpGet("surveys/{surveyId}/questions/{id}")]
        public async Task<IActionResult> GetQuestion([FromRoute] int id, [FromRoute] int surveyId)
        {
            var question = _mapper.Map<QuestionDto> (await _repository.GetQuestionAsync(id, surveyId));
            if (question == null) return NotFound();
            return Ok(question);
        }

        [HttpPost("interviews/{interviewId}/questions/{questionId}/answers")]
        public async Task<IActionResult> SaveAnswer([FromBody] SaveAnswerDto saveAnswerDto)
        {

            if (saveAnswerDto == null) return BadRequest("Invalid answer data.");

            var result = _mapper.Map<Result>(saveAnswerDto);

            var nextQuestionId = await _repository.SaveAnswerAsync(result.InterviewId, result.QuestionId, result.AnswerId);

            if (nextQuestionId == 0)
            {
                return NotFound(new { Message = "The questions have ended." });
            }
            else if (nextQuestionId == null)
            {
                return NotFound(new { Message = "Invalid Data" });
            }
            return Ok(new { NextQuestionId = nextQuestionId });
        }
    }
}
