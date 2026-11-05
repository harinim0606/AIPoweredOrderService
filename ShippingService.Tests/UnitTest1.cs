using OrderShipping;
using Xunit;

namespace ShippingService.Tests;

public class ShippingServiceTests
{
    private readonly OrderShipping.ShippingService _shippingService = new();

    // Test 1: Domestic Standard shipping
    [Fact]
    public void CalculateShippingCost_DomesticStandard_ReturnsCorrectCost()
    {
        decimal result = _shippingService.CalculateShippingCost(
            2m,
            ShippingDestination.Domestic,
            ShippingMethod.Standard);

        Assert.Equal(100m, result);
    }

    // Test 2: Domestic Express shipping
    [Fact]
    public void CalculateShippingCost_DomesticExpress_ReturnsCorrectCost()
    {
        decimal result = _shippingService.CalculateShippingCost(
            2m,
            ShippingDestination.Domestic,
            ShippingMethod.Express);

        Assert.Equal(160m, result);
    }

    // Test 3: International Standard shipping
    [Fact]
    public void CalculateShippingCost_InternationalStandard_ReturnsCorrectCost()
    {
        decimal result = _shippingService.CalculateShippingCost(
            2m,
            ShippingDestination.International,
            ShippingMethod.Standard);

        Assert.Equal(400m, result);
    }

    // Test 4: International Express shipping
    [Fact]
    public void CalculateShippingCost_InternationalExpress_ReturnsCorrectCost()
    {
        decimal result = _shippingService.CalculateShippingCost(
            2m,
            ShippingDestination.International,
            ShippingMethod.Express);

        Assert.Equal(600m, result);
    }

    // Test 5: Fractional package weight
    [Fact]
    public void CalculateShippingCost_FractionalWeight_ReturnsCorrectCost()
    {
        decimal result = _shippingService.CalculateShippingCost(
            0.5m,
            ShippingDestination.Domestic,
            ShippingMethod.Standard);

        Assert.Equal(25m, result);
    }

    // Test 6: Zero weight should be rejected
    [Fact]
    public void CalculateShippingCost_ZeroWeight_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _shippingService.CalculateShippingCost(
                0m,
                ShippingDestination.Domestic,
                ShippingMethod.Standard));
    }

    // Test 7: Negative weight should be rejected
    [Fact]
    public void CalculateShippingCost_NegativeWeight_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            _shippingService.CalculateShippingCost(
                -1m,
                ShippingDestination.Domestic,
                ShippingMethod.Standard));
    }

    // Test 8: Invalid destination should be rejected
    [Fact]
    public void CalculateShippingCost_InvalidDestination_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            _shippingService.CalculateShippingCost(
                1m,
                (ShippingDestination)999,
                ShippingMethod.Standard));
    }

    // Test 9: Invalid shipping method should be rejected
    [Fact]
    public void CalculateShippingCost_InvalidMethod_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            _shippingService.CalculateShippingCost(
                1m,
                ShippingDestination.Domestic,
                (ShippingMethod)999));
    }
}