using Amazon.DynamoDBv2.DataModel;
using Tennis.Core.Entities.Converter;

namespace Tennis.Core.Entities
{
    [DynamoDBTable("Players")]
    public class Player
    {
        [DynamoDBHashKey]
        public int Id { get; set; }

        [DynamoDBProperty] public string Firstname { get; set; } = default!;
        [DynamoDBProperty] public string Lastname { get; set; } = default!;
        [DynamoDBProperty] public string Shortname { get; set; } = default!;
        [DynamoDBProperty] public string Sex { get; set; } = default!;

        [DynamoDBProperty] public Country Country { get; set; } = new();
        [DynamoDBProperty] public string Picture { get; set; } = default!;
        [DynamoDBProperty] public PlayerData Data { get; set; } = new();
    }

    public class Country
    {
        [DynamoDBProperty] public string Picture { get; set; } = default!;
        [DynamoDBProperty] public string Code { get; set; } = default!;
    }

    public class PlayerData
    {
        [DynamoDBProperty] public int Rank { get; set; }
        [DynamoDBProperty] public int Points { get; set; }
        [DynamoDBProperty] public int Weight { get; set; }
        [DynamoDBProperty] public int Height { get; set; }
        [DynamoDBProperty] public int Age { get; set; }

        [DynamoDBProperty(Converter = typeof(IntListConverter))]
        public List<int> Last { get; set; } = new();
    }
}
