namespace backend.Services;

using backend.Domain;
using backend.Data;
using backend.DTOs;
using backend.Mappers;
using Microsoft.EntityFrameworkCore;

public class LevelService : ILevelService
{
    private readonly ApplicationDbContext _context;

    public LevelService(ApplicationDbContext context) => _context = context;

    public async Task<List<LevelResponseDto>> GetAllWithRequirementsAsync()
    {
        var levels = await _context.Levels
            .Include(l => l.LevelTagRequirements)
            .ThenInclude(r => r.Tag)
            .ToListAsync();

        return levels.Select(l => l.ToDto()).ToList();
    }

    public async Task<LevelResponseDto> CreateLevelAsync(CreateLevelDto dto)
    {
        var level = dto.ToEntity();

        _context.Levels.Add(level);
        await _context.SaveChangesAsync();

        return level.ToDto();
    }

    public async Task<bool> UpdateLevelAsync(Guid id, CreateLevelDto updateDto)
    {
        try
        {
            var existingLevel = await _context.Levels
                .Include(l => l.LevelTagRequirements)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (existingLevel == null)
            {
                Console.WriteLine($"DEBUG: Level avec ID {id} non trouvé en DB !");
                return false;
            }

            existingLevel.Name = updateDto.Name;
            existingLevel.LevelRank = updateDto.LevelRank;
            existingLevel.RequiredXP = updateDto.RequiredXP;
            existingLevel.UpdatedAt = DateTime.UtcNow;

            _context.LevelTagRequirements.RemoveRange(existingLevel.LevelTagRequirements);

            if (updateDto.Requirements != null)
            {
                foreach (var reqDto in updateDto.Requirements)
                {
                    _context.LevelTagRequirements.Add(new LevelTagRequirement
                    {
                        LevelId = id,
                        TagId = reqDto.TagId,
                        RequiredOccurrences = reqDto.RequiredOccurrences
                    });
                }
            }

            await _context.SaveChangesAsync();
            Console.WriteLine("DEBUG: Update réussi en base de données !");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERREUR DB: " + ex.Message);
            if (ex.InnerException != null)
                Console.WriteLine("INNER: " + ex.InnerException.Message);
            return false;
        }
    }

    public async Task<bool> DeleteLevelAsync(Guid id)
    {
        var level = await _context.Levels
            .Include(l => l.LevelTagRequirements)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (level == null) return false;

        foreach (var req in level.LevelTagRequirements)
        {
            req.IsDeleted = true;
        }

        level.IsDeleted = true;
        level.DeletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }
}
