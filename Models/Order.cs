﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClothingStore.Models
{
    public class Order
    {
        [BindNever]//поле не будет отображаться в ui
        public int Id { get; set; }

        [BindNever]
        public bool Shipped { get; set; }

        [Display(Name = "Name")]
        [StringLength(20)]
        [Required(ErrorMessage = "Name must match the format")]
        public string CustomerName { get; set; }

                
        [Display(Name = "Surname")]
        [StringLength(20)]
        [Required(ErrorMessage = "Surname must match the format")] 
        public string CustomerSurname { get; set; }

        
        [StringLength(70)]
        [Required(ErrorMessage = "Adress must match the format")]
        public string Adress { get; set; }

               
        [DataType(DataType.PhoneNumber)]
        [StringLength(13)]        
        public string Phone { get; set; }
        
        
        [DataType(DataType.EmailAddress)]
        [StringLength(40)]
        [Required(ErrorMessage = "Email adress must match the format")]
        public string Email { get; set; }

        [BindNever]
        [ScaffoldColumn(false)] //запрет на отображение в исходном коде(системное поле)
        public DateTime OrderDate { get; set; }
        
        public ICollection<ShopCartItem> Items { get; set; }
    }
}
