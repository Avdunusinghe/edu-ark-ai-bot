using System.ComponentModel;

namespace EduArk.Domain.Enums
{
    public enum ClassCategory
    {
        [Description("Primary")]
        Primary = 1,
        [Description("Junior")]
        Junior = 2,
        [Description("O/Level")]
        OLevel = 3,
        [Description("A/Level-Maths")]
        ALevelMaths = 4,
        [Description("A/Level-Bio")]
        ALevelBio = 5,
        [Description("A/Level-Technology")]
        ALevelTechnology = 6,
        [Description("A/Level-Commerce")]
        ALevelCommerce = 7,
        [Description("A/Level-Art")]
        ALevelArt = 8
    };
}
