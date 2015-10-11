namespace Itad2015.Contract.DTO.PostDto
{
    public class ExcelPostFileDto
    {
        public int WorkSheetNumber { get; set; }
        public int EmailPosition { get; set; }
        public int NamePosition { get; set; }
        public int LastNamePosition { get; set; }
        public string FilePath { get; set; }
        public bool HasHeader { get; set; }

    }
}
