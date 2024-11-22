using System.Text.Json.Serialization;

namespace erp.Models;

[JsonSerializable(typeof(SeriesModel))]
public record SeriesModel(string Name, string Brand, decimal Price, List<SizeModel> Sizes, int Id);

[JsonSerializable(typeof(SizeModel))]
public record SizeModel(string SKU, int Size, int Stock);
