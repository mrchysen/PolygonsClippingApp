using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Media;

namespace GeometryAlgorithms.Models.JSONConverters;

public class SolidColorBrushConverter : JsonConverter<SolidColorBrush>
{
    public override SolidColorBrush Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        reader.Read();
        byte A = ReadComponent(ref reader);
        byte R = ReadComponent(ref reader);
        byte G = ReadComponent(ref reader);
        byte B = ReadComponent(ref reader);

        return new SolidColorBrush(Color.FromArgb(A,R,G,B));
    }

    private byte ReadComponent(ref Utf8JsonReader reader)
    {
        reader.Read();

        byte value = reader.GetByte();

        reader.Read();

        return value;
    }

    public override void Write(Utf8JsonWriter writer, SolidColorBrush brush, JsonSerializerOptions options)
    {
        var color = brush.Color;

        writer.WriteStartObject();
        writer.WriteNumber("A", color.A);
        writer.WriteNumber("R", color.R);
        writer.WriteNumber("G", color.G);
        writer.WriteNumber("B", color.B);
        writer.WriteEndObject();

        writer.Flush();
    }
}
