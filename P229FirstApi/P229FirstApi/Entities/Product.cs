namespace P229FirstApi.Entities
{
    public class Product : BaseEntity
    {
        //public Category Category { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
