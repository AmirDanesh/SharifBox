using System.ComponentModel.DataAnnotations;

namespace SpaceApi.Domain.Enums
{
    public enum SpaceType : byte
    {
        [Display(Name = "ساختمان")]
        Building = 0,
        [Display(Name = "فضا")]
        Space = 1,
        [Display(Name = "صندلی منعطف")]
        FlexChair = 200,
        [Display(Name = "صندلی ثابت")]
        FixChair = 201,
        [Display(Name = "اتاق")]
        Room = 202,
        [Display(Name = "اتاق جلسات")]
        ConferenceRoom = 203,
    }    
}