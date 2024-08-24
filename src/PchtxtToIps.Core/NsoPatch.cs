using CommunityToolkit.HighPerformance.Buffers;
using Revrs;
using System.Globalization;

namespace PchtxtToIps.Core;

file enum State
{
    None,
    Enabled,
}

public class NsoPatch(string nsoId, Dictionary<uint, uint> entries)
{
    private const uint MAGIC = 0x49505333;
    private const byte MAGIC_END = 0x32;
    private const uint EOF = 0x45454F46;

    private const string NSOID_KEYWORD = "@nsobid-";
    private const string ENABLED_KEYWORD = "@enabled";
    private const char COMMENT_CHAR = '@';
    private const string STOP_KEYWORD = "@stop";

    private const uint NSO_OFFSET_SHIFT = 0x100;

    public string NsoId { get; set; } = nsoId;
    public Dictionary<uint, uint> Entries { get; } = entries;

    public static NsoPatch FromIpsFile(string file)
    {
        using FileStream fs = File.OpenRead(file);
        int size = Convert.ToInt32(fs.Length);
        using SpanOwner<byte> spanOwner = SpanOwner<byte>.Allocate(size);
        fs.Read(spanOwner.Span);
        return FromIps(spanOwner.Span, Path.GetFileNameWithoutExtension(file));
    }

    public static NsoPatch FromIps(Span<byte> data, string nsoId)
    {
        RevrsReader reader = new(data, Endianness.Big);
        if (reader.Read<uint>() is not MAGIC || reader.Read<byte>() is not MAGIC_END) {
            throw new InvalidDataException("Invalid magic.");
        }

        Dictionary<uint, uint> entries = [];

        uint address;
        while ((address = reader.Read<uint>()) is not EOF) {
            int valueSize = reader.Read<short>();
            if (valueSize is not sizeof(uint)) {
                throw new InvalidDataException(
                    $"Invalid value size, expected {sizeof(uint)} but found '{valueSize}'.");
            }

            entries[address - NSO_OFFSET_SHIFT] = reader.Read<uint>();
        }

        return new(nsoId, entries);
    }

    public static NsoPatch FromPchtxtFile(string file)
    {
        using FileStream fs = File.OpenRead(file);
        using StreamReader reader = new(fs);
        return FromPchtxt(reader);
    }

    public static NsoPatch FromPchtxt(StreamReader reader)
    {
        State state = State.None;

        string? nsoId = null;
        Dictionary<uint, uint> entries = [];

        while (reader.ReadLine() is string line) {
            ReadOnlySpan<char> lineChars = line.AsSpan();

            if (lineChars.Length >= 48 && lineChars[0..8] is NSOID_KEYWORD) {
                nsoId = line[8..48];
                continue;
            }

            if (state is State.Enabled) {
                if (lineChars.Length >= STOP_KEYWORD.Length && lineChars[..STOP_KEYWORD.Length] is STOP_KEYWORD) {
                    state = State.None;
                    goto Skip;
                }

                if (line.Length > 0 && line[0] == COMMENT_CHAR) {
                    continue;
                }

                if (!uint.TryParse(lineChars[0..8], NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out uint address)) {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine($"Could not parse entry address '{lineChars[0..8]}' skipping '{line}'.");
#endif
                    continue;
                }

                if (!uint.TryParse(lineChars[9..17], NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out uint value)) {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine($"Could not parse entry value '{lineChars[9..17]}' skipping '{line}'.");
#endif
                    continue;
                }

                entries[address] = value;
                continue;
            }

            if (lineChars.Length >= ENABLED_KEYWORD.Length && lineChars[..ENABLED_KEYWORD.Length] is ENABLED_KEYWORD) {
                state = State.Enabled;
                continue;
            }
        }

    Skip:
        if (nsoId is null) {
            throw new InvalidDataException("Invalid pchtxt file. The nso ID could not be found.");
        }

        return new(nsoId, entries);
    }

    public void WriteIps(string outputFolder)
    {
        Directory.CreateDirectory(outputFolder);
        string outputFile = Path.Combine(outputFolder, $"{NsoId}.ips");

        using FileStream fs = File.Create(outputFile);
        WriteIps(fs);
    }

    public void WriteIps(Stream output)
    {
        RevrsWriter writer = new(output, Endianness.Big);

        writer.Write(MAGIC);
        writer.Write(MAGIC_END);

        foreach (var (address, value) in Entries) {
            writer.Write(address + NSO_OFFSET_SHIFT);
            writer.Write<short>(sizeof(uint));
            writer.Write(value);
        }

        writer.Write(EOF);
    }

    public void WritePchtxt(string outputFile)
    {
        if (Path.GetDirectoryName(outputFile) is string directory) {
            Directory.CreateDirectory(directory);
        }
        
        using FileStream fs = File.Create(outputFile);
        WritePchtxt(fs);
    }

    public void WritePchtxt(Stream output)
    {
        using StreamWriter writer = new(output);
        writer.Write("@nsobid-");
        writer.WriteLine(NsoId);

        writer.WriteLine($"""

            @flag print_values
            @flag offset_shift {NSO_OFFSET_SHIFT:x3}

            @enabled
            """);

        foreach (var (address, value) in Entries) {
            writer.Write($"{address:x8} ");
            writer.WriteLine($"{value:x8}");
        }

        writer.WriteLine("@stop");
    }
}
