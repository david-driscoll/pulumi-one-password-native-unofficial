using System.Collections.Immutable;
using System.Text;

namespace pulumi_resource_one_password_native_unofficial.OnePasswordCli;

public record ArgsBuilder
{
    private readonly ImmutableArray<string> _args;
    private const string KeyPrefix = "--";
    private const string KeyValueSeparator = " ";
    private const string ArgsSeparator = " ";
    private const string ValueSeparator = ",";

    public ArgsBuilder()
    {
        _args = ImmutableArray.Create<string>();
    }

    private ArgsBuilder(ImmutableArray<string> args)
    {
        _args = args;
    }

    public ArgsBuilder Add(string value) => new(_args.Add(value));

    public ArgsBuilder Add(string key, string? arg, bool condition = true)
    {
        if (!condition || string.IsNullOrWhiteSpace(arg))
        {
            return this;
        }

        return new ArgsBuilder(_args
            .Add($"{KeyPrefix}{key}")
            .Add(escapeValue(arg))
        );
    }

    public ArgsBuilder Add(string key, bool arg)
    {
        if (!arg)
        {
            return this;
        }

        return new ArgsBuilder(_args
            .Add($"{KeyPrefix}{key}")
        );
    }

    public ArgsBuilder Add(string key, IEnumerable<string> args)
    {
        if (!args.Any())
        {
            return this;
        }
        return new ArgsBuilder(_args
            .Add($"{KeyPrefix}{key}")
            .Add(escapeValue(string.Join(ValueSeparator, args)))
        );
    }

    public string Build()
    {
        var builder = new StringBuilder();
        foreach (var arg in _args)
        {
            builder.Append(arg);
            builder.Append(ArgsSeparator);
        }

        return builder.ToString().TrimEnd();
    }

    private string escapeValue(string? arg) => arg.IndexOf(' ', StringComparison.Ordinal) > -1 ? $"\"{arg}\"" : arg;
}
