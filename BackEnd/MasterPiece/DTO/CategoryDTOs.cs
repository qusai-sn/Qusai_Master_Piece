namespace MasterPiece.DTO
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class CreateCategoryDto
    {
        public string CategoryName { get; set; }
    }

    public class TypeDto
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
    }

    public class CreateTypeDto
    {
        public string TypeName { get; set; }
    }
}