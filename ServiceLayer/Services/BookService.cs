public class BooksService
{
    private int _nextId = 1;

    private readonly Dictionary<int, string> _authors = new()
    {
        { 1, "Adam Mickiewicz" },
        { 2, "Henryk Sienkiewicz" }
    };

    private readonly List<(int Id, DateTime CreatedOn, string Title, int AuthorId)> _books = new();

    public CreateBookResponseDto CreateBook(CreateBookRequestDto request)
    {
        if (!_authors.ContainsKey(request.AuthorId))
            throw new ArgumentException("Author not found");

        var book = (
            Id: _nextId++,
            CreatedOn: DateTime.UtcNow,
            Title: request.Title,
            AuthorId: request.AuthorId
        );

        _books.Add(book);

        return new CreateBookResponseDto
        {
            Id = book.Id,
            CreatedOn = book.CreatedOn,
            Title = book.Title,
            AuthorName = _authors[book.AuthorId]
        };
    }

    public BookDetailsDto GetBookById(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);

        if (book.Id == 0)
            throw new KeyNotFoundException("Book not found");

        return new BookDetailsDto
        {
            Id = book.Id,
            CreatedOn = book.CreatedOn,
            Name = book.Title, // wymagane pole "name"
            AuthorName = _authors[book.AuthorId]
        };
    }

    public IEnumerable<BookDetailsDto> GetAllBooks()
    {
        return _books.Select(book => new BookDetailsDto
        {
            Id = book.Id,
            CreatedOn = book.CreatedOn,
            Name = book.Title,
            AuthorName = _authors.ContainsKey(book.AuthorId) ? _authors[book.AuthorId] : "Unknown"
        });
    }

}
