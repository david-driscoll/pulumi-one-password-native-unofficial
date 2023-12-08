using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

namespace pulumi_resource_one_password_native_unofficial;

public static class DebugHelper
{
    public static void WaitForDebugger()
    {
        if (Environment.GetCommandLineArgs().Any(z => z == "--debugger" || z == "--debug"))
        {
            var count = 0;
            while (!Debugger.IsAttached)
            {
                Thread.Sleep(1000);
                if (count++ > 30)
                {
                    break;
                }
            }
        }
    }
}

public static class Helpers
{
    public static string GetNameFromUrn(string urn)
    {
        return urn.Split(':').Last();
    }
    public static string NewUniqueName(string prefix, int? randomSeed = null, int randlen = 8, int? maxlen = null, string? charset = null)
    {
        if (randlen <= 0)
        {
            randlen = 8;
        }
        if (maxlen.HasValue && maxlen > 0 && prefix.Length + randlen > maxlen)
        {
            throw new Exception($"name '{prefix}' plus {randlen} random chars is longer than maximum length {maxlen}");
        }

        if (charset is null)
        {
            charset = "0123456789abcdef";
        }

        if (!randomSeed.HasValue)
        {
            randomSeed = new Random().Next();
        }

        var r = new Random(randomSeed.Value);
        var randomSuffix = "";
        for (var i = 0; i < randlen; i++)
        {
            randomSuffix += charset[r.Next(0, charset.Length - 1)];
        }

        return prefix + randomSuffix;
    }
    /*

function getNameFromUrn(urn: pulumi.URN) {
    const parts = urn.split(':');
    return parts[parts.length - 1]
}

function newUniqueName(prefix: string, randomSeed?: Buffer, randlen = 8, maxlen?: number, charset?: string): string {
    if (randlen <= 0) {
        randlen = 8
    }
    if (maxlen && maxlen > 0 && prefix.length + randlen > maxlen) {
        throw new Error(`name '${prefix}' plus ${randlen} random chars is longer than maximum length ${maxlen}`)
    }

    if (!charset) {
        charset = "0123456789abcdef";
    }

    let r: import("random").Random;
    if (!randomSeed || randomSeed.length === 0) {
        r = new Random(RNGFactory(randomBytes(randlen).toString('hex')))
    } else {
        const hash = createHash('sha256');
        hash.write(randomSeed);
        const seed = hash.digest('hex')
        r = new Random(RNGFactory(seed));
    }

    let randomSuffix = '';
    for (let i = 0; i < randlen; i++) {
        randomSuffix += charset[r.int(0, charset.length - 1)]
    }

    return prefix + randomSuffix;
}
*/
}