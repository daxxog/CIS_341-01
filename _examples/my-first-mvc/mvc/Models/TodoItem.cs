using System.ComponentModel.DataAnnotations;

namespace mvc.Models;

public class TodoItem {
    [Key]
    public int Id { get; set; }
    public string Message;
    public string Title;
    public bool Checked;
    public DateTime DueDate;
}
