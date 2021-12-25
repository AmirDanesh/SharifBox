using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceApi.Domain.Enums
{
    public enum PaymentStatus
    {
        [Display(Name = "موفق")]
        Paid = 0,
        [Display(Name = "در انتظار پرداخت")]
        Pending = 1,
        [Display(Name = "ناموفق")]
        Cancel = 2
    }
}
