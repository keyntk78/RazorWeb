﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorWeb.Models
{
    
    public class Article
    {
        [Key]
        public int ID { get; set; }
        [StringLength(255, MinimumLength =5, ErrorMessage ="{0} phải dài từ {2} tới {1} ")]
        [Required(ErrorMessage ="{0} phải nhập")]
        [DisplayName("Tiêu đề")]
        [Column(TypeName ="nvarchar")]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [Required (ErrorMessage ="{0} phải nhập")]
        [DisplayName("Ngày tạo")]

        public DateTime Creadted { get; set; }
        [Column(TypeName ="ntext")]
        [DisplayName("Nội dung")]
        public string Content { set; get; }
    }
}
