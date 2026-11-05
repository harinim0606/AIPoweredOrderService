using OrderShipping;

var shippingService = new ShippingService();

Console.WriteLine("================================");
Console.WriteLine("      AI-POWERED ORDER SERVICE");
Console.WriteLine("================================");

Console.Write("Enter package weight (kg): ");

if (!decimal.TryParse(Console.ReadLine(), out decimal weight))
{
    Console.WriteLine("Invalid weight. Please enter a number.");
    return;
}

Console.WriteLine("\nSelect Destination:");
Console.WriteLine("1. Domestic");
Console.WriteLine("2. International");
Console.Write("Enter choice: ");

string? destinationChoice = Console.ReadLine();

ShippingDestination destination;

switch (destinationChoice)
{
    case "1":
        destination = ShippingDestination.Domestic;
        break;

    case "2":
        destination = ShippingDestination.International;
        break;

    default:
        Console.WriteLine("Invalid destination.");
        return;
}

Console.WriteLine("\nSelect Shipping Method:");
Console.WriteLine("1. Standard");
Console.WriteLine("2. Express");
Console.Write("Enter choice: ");

string? methodChoice = Console.ReadLine();

ShippingMethod method;

switch (methodChoice)
{
    case "1":
        method = ShippingMethod.Standard;
        break;

    case "2":
        method = ShippingMethod.Express;
        break;

    default:
        Console.WriteLine("Invalid shipping method.");
        return;
}

try
{
    decimal cost = shippingService.CalculateShippingCost(
        weight, destination, method);

    Console.WriteLine("\n---------- SHIPPING DETAILS ----------");
    Console.WriteLine($"Weight: {weight} kg");
    Console.WriteLine($"Destination: {destination}");
    Console.WriteLine($"Method: {method}");
    Console.WriteLine($"Shipping Cost: ₹{cost:F2}");
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}