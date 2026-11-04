using System.Globalization;

namespace BlueCraneCoffee.Models;

public static class Format
{
    public static string Money(this decimal value) =>
        "R" + value.ToString("0.00", CultureInfo.InvariantCulture);
}
