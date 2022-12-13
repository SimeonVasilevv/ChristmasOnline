﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ChristmasOnline.Models.ViewModels
{
	public class ProductViewModel
	{
        public string? Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters long.")]
        public string Name { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [Required]
        [Range(1, 30000)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 30000)]
        public decimal Price10 { get; set; }

        [Required]
        [Range(1, 30000)]
        public decimal Price20 { get; set; }

        [Required]
        [Range(1, 30000)]
        public decimal ListPrice { get; set; }

        public DateTime DateOfReceiving { get; set; } = DateTime.Now;

        [Required]
        [StringLength(200)]
        [ValidateNever]
        public string ImageUrl { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{13}$", ErrorMessage = "{0} must be exactly 13 characters long.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "{0} must be exactly 13 characters long.")]
        public string Barcode { get; set; }

        public string CategoryId { get; set; }

        public string MaterialId { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> MaterialList { get; set; }
    }
}
