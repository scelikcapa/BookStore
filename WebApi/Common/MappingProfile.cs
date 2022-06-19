using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;

namespace WebApi.Common;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookModel, Book>();
        CreateMap<Book, GetByIdQueryModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));

    }
}