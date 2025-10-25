using Tennis.Core.Dtos;
using Tennis.Core.Entities;

namespace Tennis.Domain.Mapper
{
    public static class PlayerMapper
    {
        public static PlayerDto ToDto(Player entity)
        {
            return new PlayerDto
            {
                Id = entity.Id,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                Shortname = entity.Shortname,
                Sex = entity.Sex,
                CountryCode = entity.Country.Code,
                CountryPicture = entity.Country.Picture,
                Picture = entity.Picture,
                Rank = entity.Data.Rank,
                Points = entity.Data.Points,
                Weight = entity.Data.Weight,
                Height = entity.Data.Height,
                Age = entity.Data.Age,
                Last = entity.Data.Last
            };
        }

        public static Player ToEntity(PlayerDto dto)
        {
            return new Player
            {
                Id = dto.Id,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Shortname = dto.Shortname,
                Sex = dto.Sex,
                Picture = dto.Picture,
                Country = new Country
                {
                    Code = dto.CountryCode,
                    Picture = dto.CountryPicture
                },
                Data = new PlayerData
                {
                    Rank = dto.Rank,
                    Points = dto.Points,
                    Weight = dto.Weight,
                    Height = dto.Height,
                    Age = dto.Age,
                    Last = dto.Last
                }
            };
        }
    }
}
