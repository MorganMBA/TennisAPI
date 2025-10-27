namespace Tennis.Core.Dtos
{
    public class PlayerDto
    {
        public int Id { get; set; }

        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public string Shortname { get; set; } = default!;
        public string Sex { get; set; } = default!;

        public CountryDto Country { get; set; } = new();
        public string Picture { get; set; } = default!;
        public PlayerDataDto Data { get; set; } = new();
    }

    public class CountryDto
    {
        public string Picture { get; set; } = default!;
        public string Code { get; set; } = default!;
    }

    public class PlayerDataDto
    {
        public int Rank { get; set; }
        public int Points { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public List<int> Last { get; set; } = new();
    }
}
