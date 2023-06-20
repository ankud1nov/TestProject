using System.ComponentModel;

namespace TestProject.Reports.Enums;

public enum ExperienceTypeEnum
{
    [Description("Больше")]
    MoreThen,
    [Description("Меньше")]
    LessThen,
    [Description("Равно")]
    Equals,
    [Description("Не важно")]
    Skip
}
