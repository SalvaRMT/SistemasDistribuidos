using System;
using System.ComponentModel.DataAnnotations;

namespace PokemonApi.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public DateTime PublishedDate { get; set; }
    }
}
