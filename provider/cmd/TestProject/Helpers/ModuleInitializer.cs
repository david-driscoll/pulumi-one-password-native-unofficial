using System.Runtime.CompilerServices;
using DiffEngine;

namespace TestProject.Helpers;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        string[] serverGeneratedFields = { "id:", "uuid:", "reference:", "title:" };
        VerifierSettings.ScrubLinesWithReplace(
            replaceLine: _ =>
            {
                if (_ is { Length: 26 })
                {
                    // might be a uuid
                    return "[server-generated]";
                }
                
                foreach (var field in serverGeneratedFields)
                {
                    if (_.Contains(field, StringComparison.OrdinalIgnoreCase))
                    {
                        return _.Substring(0, _.IndexOf(field, StringComparison.OrdinalIgnoreCase) + field.Length) + " [server-generated]";
                    }                    
                }

                return _;
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
