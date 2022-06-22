using AutoMapper;
using WebApi.Application.GenreOperations.Command.CreateGenre;
using WebApi.Application.GenreOperations.Command.UpdateGenre;
using WebApi.Application.GenreOperations.Query.GetGenreDetail;
using WebApi.Application.GenreOperations.Query.GetGenres;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.Entities;

namespace WebApi.Common;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookModel,Book>();
        CreateMap<Book,GetByIdQueryModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>src.Genre.Name));
        CreateMap<Book,BooksViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=> src.Genre.Name));
        CreateMap<Genre,GetGenresViewModel>();
        CreateMap<Genre,GetGenreDetailViewModel>();
        CreateMap<CreateGenreModel,Genre>();
    }
}