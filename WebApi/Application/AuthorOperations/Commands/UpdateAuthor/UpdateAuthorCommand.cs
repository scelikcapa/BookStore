using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommand
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;
    public int AuthorId { get; set; }
    public UpdateAuthorModel Model { get; set; }

    public UpdateAuthorCommand(BookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var author=context.Authors.SingleOrDefault(a=>a.Id==AuthorId);

        if(author is null)
            throw new InvalidOperationException("Güncellenecek Yazar bulunamadı.");
            
        if(context.Authors.Any(a=>a.FullName==Model.FullName && a.Id != AuthorId))
            throw new InvalidOperationException("Aynı isimli bir Yazar zaten mevcuttur.");
        
        author = mapper.Map<Author>(Model);

        context.SaveChanges();
    }
}

public class UpdateAuthorModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FullName
    {
        get { return Name.ToLower().Trim()+ " "+ Surname.ToLower().Trim(); }
    }
    
    public DateTime BirthDate { get; set; }
}