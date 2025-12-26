using ModelLayer.Entities;

public class NoteLabel
{
    public int NotesId { get; set; }
    public int LabelId { get; set; }

    public Notes Note { get; set; }
    public Label Label { get; set; }
}