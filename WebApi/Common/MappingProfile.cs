using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entities;

namespace WebApi.Common;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookModel,Book>();
        CreateMap<Book,GetBookDetailViewModel>().ForMember(dest=> dest.Genre, opt=> opt.MapFrom(src=>src.Genre.Name));
        CreateMap<Book,GetBooksViewModel>().ForMember(dest=> dest.Genre, opt=> opt.MapFrom(src=> src.Genre.Name));
        
        CreateMap<Genre,GetGenresViewModel>();
        CreateMap<Genre,GetGenreDetailViewModel>();
        
        CreateMap<CreateGenreModel,Genre>();
        
        CreateMap<Author,GetAuthorViewModel>();
        CreateMap<Author,GetAuthorDetailViewModel>();
        
        CreateMap<CreateAuthorModel,Author>();
        
        CreateMap<UpdateAuthorModel,Author>()
            .ForMember(dest=>dest.Id,opt=>opt.Ignore())
            .ForMember(dest=>dest.Name, opt=>opt.Condition(src=>!String.IsNullOrEmpty(src.Name.Trim())))
            .ForMember(dest=>dest.Surname, opt=>opt.Condition(src=>!String.IsNullOrEmpty(src.Surname.Trim())));
    }
}