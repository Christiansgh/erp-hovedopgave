using System.Text.Json.Serialization;

namespace erp.Models;

[JsonSerializable(typeof(ModelRecords))]
public record ModelRecords(string Name, string Brand, decimal Price, List<SizeModel> Sizes, int Id);
[JsonSerializable(typeof(SizeModel))]
public record SizeModel(string SKU, int Size, int Stock);
