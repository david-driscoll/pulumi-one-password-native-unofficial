using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using DiffEngine;
using Pulumi.Automation;

// ReSharper disable NullableWarningSuppressionIsUsed

namespace TestProject.Helpers;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        string[] serverGeneratedFields = { "id:", "uuid:", "reference:", "title:" };
        var regex = new Regex(@"^[\da-z]{26}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        var regex2 = new Regex(@"""[\da-z]{26}""", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        VerifierSettings.ScrubLinesWithReplace(
            replaceLine: line =>
            {
                if (regex.IsMatch(line))
                {
                    return "[server-generated]";
                }
                if (regex2.IsMatch(line))
                {
                    line = regex2.Replace(line, @"""[server-generated]""");
                }

                foreach (var field in serverGeneratedFields)
                {
                    if (line.Contains(field, StringComparison.OrdinalIgnoreCase))
                    {
                        return line.Substring(0, line.IndexOf(field, StringComparison.OrdinalIgnoreCase) + field.Length) + " [server-generated]";
                    }
                }

                return line;
            });
        VerifierSettings.IgnoreMember<UpdateSummary>(nameof(UpdateSummary.Config));
        VerifierSettings.IgnoreMember<UpdateSummary>(nameof(UpdateSummary.Environment));
        VerifierSettings.IgnoreMember(nameof(UpdateResult.StandardError));
        VerifierSettings.IgnoreMembersWithType<DateTimeOffset>();
        // regex to capture and remove the time in this string
        // one-password-native-unofficial:index:LoginItem login updated (0.30s) 

        var pattern = new Regex(@" \(\d+\.\d+s\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        var pattern2 = new Regex(@" \(\d+s\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        var pattern3 = new Regex(@": \d+\.\d+s", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        var pattern4 = new Regex(@": \d+s", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        Func<Regex, string, Func<string, string>> factory = (regex, replacement) => s => regex.Replace(s, replacement);
        var patterns = new Func<string, string> []
        {
            factory(pattern, " (0.0s)"), 
            factory(pattern2, " (0s)"), 
                factory(pattern3, ": 0.0s"), 
                    factory(pattern4, ": 0s")
        };
        VerifierSettings.ScrubLinesWithReplace(replaceLine: s => 
            string.IsNullOrWhiteSpace(s)
                ? s 
                : patterns.Aggregate(s, (current, pattern) => pattern(current)));
        VerifierSettings.AddExtraSettings(
            serializer =>
            {
                var converters = serializer.Converters;
                converters.Add(new PropertyValueConverter());
                converters.Add(new DictionaryPropertyValueConverter());
            }
        );
        DiffRunner.Disabled = true;
        DerivePathInfo(
            (sourceFile, _, type, method) =>
            {
                static string GetTypeName(Type type)
                {
                    return type.IsNested ? $"{type.ReflectedType!.Name}.{type.Name}" : type.Name;
                }

                var typeName = GetTypeName(type);

                // ReSharper disable once RedundantAssignment
                var path = Path.Combine(Path.GetDirectoryName(sourceFile)!, "snapshots");
                return new(path, typeName, method.Name);
            }
        );
    }
}
