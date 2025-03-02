using AutoMapper;
using SurveyApi.Dto;
using SurveyApi.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SaveAnswerDto, Result>();
        CreateMap<Question, QuestionDto>()
            .ForMember(dest => dest.AnswersDto, opt => opt.MapFrom(src => src.Answers));
        CreateMap<Answer, AnswerDto>();
    }
}