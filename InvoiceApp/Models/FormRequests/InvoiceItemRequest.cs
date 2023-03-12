﻿using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models.FormRequests
{
    public class InvoiceItemRequest
    {
        [Required, StringLength(255, MinimumLength = 3, ErrorMessage = "The field first name must be a string with a minimum length of 3 and a maximum length of 255.\r\n")]
        public string Name { get; set; }
        [Required, Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }
        [Required, Range(0, Double.MaxValue)]
        public double ItemPrice { get; set; }
    }
}
