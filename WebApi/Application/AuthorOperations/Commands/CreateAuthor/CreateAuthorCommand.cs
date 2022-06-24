using AutoMapper;
using FluentValidation;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommand 
{
    private readonly IBookStoreDbContext context;
    private readonly IMapper mapper;
    public CreateAuthorModel Model { get; set; }

    public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var author = context.Authors.SingleOrDefault(a=>a.Name.ToLower() == Model.Name.ToLower() && a.Surname.ToLower() == Model.Surname.ToLower());

        if(author is not null)
            throw new InvalidOperationException("Yazar zaten mevcut.");
        
        author = mapper.Map<Author>(Model);

        context.Authors.Add(author);
        context.SaveChanges();
    }
}

public class CreateAuthorModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
}