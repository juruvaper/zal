// Controllers/BooksController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BooksService _bookService;

    public BooksController(BooksService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost]
    public ActionResult<CreateBookResponseDto> CreateBook([FromBody] CreateBookRequestDto request)
    {
        var result = _bookService.CreateBook(request);
        return CreatedAtAction(nameof(CreateBook), new { id = result.Id }, result);
    }

    [AllowAnonymous]
    [HttpGet("public")]
    public IActionResult PublicEndpoint()
    {
        return Ok("To jest publiczny endpoint - nie wymaga logowania");
    }

    // Tylko dla zalogowanych użytkowników (token JWT)
    [Authorize]
    [HttpGet("user")]
    public IActionResult UserEndpoint()
    {
        var userName = User.Identity?.Name ?? "unknown";
        return Ok($"Witaj, {userName}! Ten endpoint wymaga bycia zalogowanym.");
    }

    // Tylko dla roli Admin
    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult AdminEndpoint()
    {
        return Ok("To jest endpoint dostępny tylko dla użytkowników z rolą Admin.");
    }

    // Endpoint wymagający specjalnej polityki (np. "CanEdit")
    [Authorize(Policy = "CanEdit")]
    [HttpGet("editor")]
    public IActionResult EditorEndpoint()
    {
        return Ok("Masz odpowiednie uprawnienia do edycji.");
    }

    // Brak dekoratora — dostęp publiczny (zależy od konfiguracji w Program.cs)
    [HttpGet("no-decorator")]
    public IActionResult NoDecoratorEndpoint()
    {
        return Ok("Endpoint bez dekoratora - domyślnie dostępny publicznie.");
    }

    [HttpGet("{id}")]
    public ActionResult<BookDetailsDto> GetBookById(int id)
    {
        var book = _bookService.GetBookById(id);
        if (book == null)
            return NotFound($"Nie znaleziono książki o id {id}");

        return Ok(book);
    }

    // Pobierz wszystkie książki
    [HttpGet]
    public ActionResult<IEnumerable<BookDetailsDto>> GetAllBooks()
    {
        var books = _bookService.GetAllBooks();
        return Ok(books);
    }
}