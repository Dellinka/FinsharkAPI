using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {        
        [MinLength(5, ErrorMessage = "Title must be over 5 characters")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters")]
        public string? Title { get; set; } = null;

        [MinLength(5, ErrorMessage = "Content must be over 5 characters")]
        [MaxLength(280, ErrorMessage = "Content cannot be over 280 characters")]
        public string? Content { get; set; } = null;
    }
}