using MasterPiece.DTO;
using MasterPiece.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterPiece.Services
{
    public interface ICategoryTypeService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto categoryDto);
        Task<CategoryDto> UpdateCategoryAsync(int id, CreateCategoryDto categoryDto);
        Task<bool> DeleteCategoryAsync(int id);

        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        Task<TypeDto> GetTypeByIdAsync(int id);
        Task<TypeDto> CreateTypeAsync(CreateTypeDto typeDto);
        Task<TypeDto> UpdateTypeAsync(int id, CreateTypeDto typeDto);
        Task<bool> DeleteTypeAsync(int id);
    }

    public class CategoryTypeService : ICategoryTypeService
    {
        private readonly MasterPieceContext _context;

        public CategoryTypeService(MasterPieceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            return await _context.EventCategories
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                })
                .ToListAsync();
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _context.EventCategories.FindAsync(id);
            if (category == null) return null;

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            var category = new EventCategory { CategoryName = categoryDto.CategoryName };
            _context.EventCategories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int id, CreateCategoryDto categoryDto)
        {
            var category = await _context.EventCategories.FindAsync(id);
            if (category == null) return null;

            category.CategoryName = categoryDto.CategoryName;
            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.EventCategories.FindAsync(id);
            if (category == null) return false;

            _context.EventCategories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            return await _context.EventTypes
                .Select(t => new TypeDto
                {
                    TypeId = t.TypeId,
                    TypeName = t.TypeName
                })
                .ToListAsync();
        }

        public async Task<TypeDto> GetTypeByIdAsync(int id)
        {
            var type = await _context.EventTypes.FindAsync(id);
            if (type == null) return null;

            return new TypeDto
            {
                TypeId = type.TypeId,
                TypeName = type.TypeName
            };
        }

        public async Task<TypeDto> CreateTypeAsync(CreateTypeDto typeDto)
        {
            var type = new EventType { TypeName = typeDto.TypeName };
            _context.EventTypes.Add(type);
            await _context.SaveChangesAsync();

            return new TypeDto
            {
                TypeId = type.TypeId,
                TypeName = type.TypeName
            };
        }

        public async Task<TypeDto> UpdateTypeAsync(int id, CreateTypeDto typeDto)
        {
            var type = await _context.EventTypes.FindAsync(id);
            if (type == null) return null;

            type.TypeName = typeDto.TypeName;
            await _context.SaveChangesAsync();

            return new TypeDto
            {
                TypeId = type.TypeId,
                TypeName = type.TypeName
            };
        }

        public async Task<bool> DeleteTypeAsync(int id)
        {
            var type = await _context.EventTypes.FindAsync(id);
            if (type == null) return false;

            _context.EventTypes.Remove(type);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}