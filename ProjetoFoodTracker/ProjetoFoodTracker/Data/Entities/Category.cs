using System.ComponentModel.DataAnnotations;

namespace ProjetoFoodTracker.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Category:")]
        public string CategoryName { get; set; }


        public ICollection<Food> Foods { get; set; }
    }
}
