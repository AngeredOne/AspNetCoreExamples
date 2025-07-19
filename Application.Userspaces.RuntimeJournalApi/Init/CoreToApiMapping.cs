using AutoMapper;

namespace Application.Userspaces.RuntimeJournalApi.Init;
 
 public class CoreToApiMapping : Profile
 {
     public CoreToApiMapping()
     {
         // CreateMap<Node, TreeNodeApi>()
         //     .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children));
         //
         // CreateMap<ExceptionJournal, ExceptionJournalApi>()
         //     .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.StackTrace));
         //
         // CreateMap<ExceptionJournal, ExceptionJournalInfoApi>();
     }
 }