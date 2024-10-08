using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Utils;

public class GuidConverter : JsonConverter<Guid>
{
    public override Guid Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
        Guid.Parse(reader.GetString()!);

    public override void Write(
        Utf8JsonWriter writer,
        Guid value,
        JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());

}