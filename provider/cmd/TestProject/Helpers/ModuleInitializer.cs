using System.Runtime.CompilerServices;
using DiffEngine;
// ReSharper disable NullableWarningSuppressionIsUsed

namespace TestProject.Helpers;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        string[] serverGeneratedFields = { "id:", "uuid:", "reference:", "title:" };
        VerifierSettings.ScrubLinesWithReplace(
            replaceLine: line =>
            {
                if (line is { Length: 26 })
                {
                    // might be a uuid
                    return "[server-generated]";
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
