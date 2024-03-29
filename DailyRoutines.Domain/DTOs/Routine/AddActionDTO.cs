﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyRoutines.Domain.DTOs.Routine;

public class AddActionDTO
{
    [Required]
    public Guid UserCategoryId { get; set; }


    [Display(Name = "عنوان فعالیت")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
    [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string ActionTitle { get; set; }

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
    [MaxLength(3000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string ActionDescription { get; set; }
}