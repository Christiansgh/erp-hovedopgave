using System.Text.Json.Serialization;

namespace erp.Models;

[JsonSerializable(typeof(Shoe))]
public record Shoe(string Brand, string Color, int Size, int StockAmount);
