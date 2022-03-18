using System;
using System.ComponentModel.DataAnnotations;

namespace Notes_API.Models
{
    public class Note
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = "owner id required.")]
        public Guid? OwnerId { get; set; }
        [Required(ErrorMessage = "title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "description is required")]
        [MinLength(10)]
        public string Description { get; set; }
        [Required(ErrorMessage = "category id is required")]
        public long CategoryId { get; set; }
    }
}
