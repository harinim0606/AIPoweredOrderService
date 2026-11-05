/*
PROMPT FOR AI:

Generate a C# ShippingService class for a .NET 8 Console Application
that calculates shipping costs based on package weight and destination.

Requirements:
1. Follow the Single Responsibility Principle (SRP).
2. Keep shipping calculation logic separate from console input/output.
3. Support Domestic and International destinations.
4. Support Standard and Express shipping methods.
5. Validate package weight, destination, and shipping method.
6. Use clear methods, meaningful names, and decimal for currency.
7. Keep shipping rates separate from calculation logic.
8. Avoid SQL, external services, and unnecessary dependencies.
*/

namespace OrderShipping;

// Represents the supported delivery destinations.
public enum ShippingDestination
{
    Domestic,
    International
}

// Represents the available shipping methods.
public enum ShippingMethod
{
    Standard,
    Express
}

public class ShippingService
{
    /*
     CODE REVIEW – SOLID IMPROVEMENTS:

     1. SRP: This class only calculates shipping costs.
        Console input and output are handled by Program.cs.

     2. OCP: Shipping rates are maintained separately from
        the calculation algorithm, making rate changes easier.

     3. Type Safety: Enums restrict destination and method
        values to supported options.

     4. Security: Invalid weights and enum values are rejected
        before performing calculations.

     5. Maintainability: Decimal is used for monetary values
        to avoid floating-point precision issues.
    */

    // Shipping rates per kilogram.
    private static readonly IReadOnlyDictionary<
        (ShippingDestination Destination, ShippingMethod Method),
        decimal> ShippingRates = new Dictionary<
            (ShippingDestination, ShippingMethod), decimal>
    {
        {
            (ShippingDestination.Domestic, ShippingMethod.Standard),
            50m
        },
        {
            (ShippingDestination.Domestic, ShippingMethod.Express),
            80m
        },
        {
            (ShippingDestination.International, ShippingMethod.Standard),
            200m
        },
        {
            (ShippingDestination.International, ShippingMethod.Express),
            300m
        }
    };

    // Calculates shipping cost based on weight, destination, and method.
    public decimal CalculateShippingCost(
        decimal weightKg,
        ShippingDestination destination,
        ShippingMethod method)
    {
        // Validate package weight.
        if (weightKg <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(weightKg),
                "Package weight must be greater than zero.");
        }

        // Validate destination.
        if (!Enum.IsDefined(destination))
        {
            throw new ArgumentException(
                "Invalid shipping destination.",
                nameof(destination));
        }

        // Validate shipping method.
        if (!Enum.IsDefined(method))
        {
            throw new ArgumentException(
                "Invalid shipping method.",
                nameof(method));
        }

        // Retrieve the configured shipping rate.
        var key = (destination, method);

        if (!ShippingRates.TryGetValue(key, out decimal ratePerKg))
        {
            throw new InvalidOperationException(
                "Shipping rate is not configured.");
        }

        // Calculate and return the shipping cost.
        return weightKg * ratePerKg;
    }
}