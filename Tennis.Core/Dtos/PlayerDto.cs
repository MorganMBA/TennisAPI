namespace Tennis.Core.Dtos
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string Shortname { get; set; } = default!;
        public string Sex { get; set; } = default!;
        public string CountryCode { get; set; } = default!;
        public string CountryPicture { get; set; } = default!;
        public string Picture { get; set; } = default!;
        public int Rank { get; set; }
        public int Points { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public List<int> Last { get; set; } = new();
    }
}
