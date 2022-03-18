using System;
using System.ComponentModel.DataAnnotations;

namespace Notes_API.Models
{
    public class Owner
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "name is required.")]
        public string Name { get; set; }
    }
}
