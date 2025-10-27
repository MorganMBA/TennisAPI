using Tennis.Core.Dtos;
using Tennis.Core.Entities;

namespace Tennis.Domain.Mapper
{
    public static class PlayerMapper
    {
        public static PlayerDto ToDto(Player entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return new PlayerDto
            {
                Id = entity.Id,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                Shortname = entity.Shortname,
                Sex = entity.Sex,
                Picture = entity.Picture,
                Country = new CountryDto
                {
                    Code = entity.Country?.Code ?? string.Empty,
                    Picture = entity.Country?.Picture ?? string.Empty
                },
                Data = new PlayerDataDto
                {
                    Rank = entity.Data?.Rank ?? 0,
                    Points = entity.Data?.Points ?? 0,
                    Weight = entity.Data?.Weight ?? 0,
                    Height = entity.Data?.Height ?? 0,
                    Age = entity.Data?.Age ?? 0,
                    Last = entity.Data?.Last ?? new List<int>()
                }
            };
        }


        public static Player ToEntity(PlayerDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

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
                    Code = dto.Country?.Code ?? string.Empty,
                    Picture = dto.Country?.Picture ?? string.Empty
                },
                Data = new PlayerData
                {
                    Rank = dto.Data?.Rank ?? 0,
                    Points = dto.Data?.Points ?? 0,
                    Weight = dto.Data?.Weight ?? 0,
                    Height = dto.Data?.Height ?? 0,
                    Age = dto.Data?.Age ?? 0,
                    Last = dto.Data?.Last ?? new List<int>()
                }
            };
        }

    }
}
