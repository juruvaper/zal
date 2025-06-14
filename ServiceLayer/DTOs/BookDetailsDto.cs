public class BookDetailsDto
{
    public int Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Name { get; set; }  // zamiast "Title", bo wymóg to "name"
    public string AuthorName { get; set; }
}
