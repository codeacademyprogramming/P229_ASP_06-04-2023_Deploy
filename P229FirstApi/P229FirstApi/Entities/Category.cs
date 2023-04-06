namespace P229FirstApi.Entities
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }

        //public int OneId { get; set; }
        //public Product One { get; set; }

        //public int? test { get; set; }
        //public Category? elebe { get; set; }

        //public IEnumerable<Category>? coxluq { get; set; }

        public IEnumerable<Product>? Products { get; set; }
    }
}
