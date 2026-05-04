using backend.Domain;
using backend.DTOs;

namespace backend.Mappers;

public static class BadgeMapper
{
    public static BadgeResponseDto ToDto(this Badge badge)
    {
        if (badge == null) return null!;
        return new BadgeResponseDto
        {
            Id = badge.Id,
            Title = badge.Title,
            Description = badge.Description,
            Xp = badge.Xp,
            Icon = badge.Icon,
            Color = badge.Color,
            Type = badge.Type.ToString()
        };
    }

    public static Badge ToEntity(this CreateBadgeDto dto)
    {
        if (dto == null) return null!;
        return new Badge
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            Xp = dto.Xp,
            Icon = dto.Icon,
            Color = dto.Color,
            Type = Enum.Parse<global::backend.Enums.BadgeType>(dto.Type, true)
        };
    }
}
