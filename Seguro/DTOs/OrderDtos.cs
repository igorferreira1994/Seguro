using System.ComponentModel.DataAnnotations;

namespace DTOs;

/// <summary>
/// DTO (Data Transfer Object) for receiving input data when creating orders.
/// Used in the presentation layer to capture data from the client.
/// </summary>
public class OrderInputDto
{
    /// <summary>
    /// Order identifier
    /// </summary>
    [Required(ErrorMessage = "Order ID is required")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// List of order items
    /// </summary>
    [Required(ErrorMessage = "Items list is required")]
    [MinLength(1, ErrorMessage = "There must be at least one item in the order")]
    public List<string> Items { get; set; } = new();
}

/// <summary>
/// DTO for order creation response
/// </summary>
public class OrderResponseDto
{
    /// <summary>
    /// Created order ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Success message
    /// </summary>
    public string Message { get; set; } = string.Empty;
}

/// <summary>
/// DTO for displaying order information
/// </summary>
public class OrderDisplayDto
{
    /// <summary>
    /// Order ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// List of items
    /// </summary>
    public List<string> Items { get; set; } = new();
}
