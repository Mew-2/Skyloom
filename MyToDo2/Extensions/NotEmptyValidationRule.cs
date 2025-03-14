using System.Globalization;
using System.Windows.Controls;

namespace MyToDo2.Extensions;

public class NotEmptyValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        return string.IsNullOrWhiteSpace((value ?? "").ToString())
            ? new ValidationResult(false, "概要不可为空。")
            : ValidationResult.ValidResult;
    }
}