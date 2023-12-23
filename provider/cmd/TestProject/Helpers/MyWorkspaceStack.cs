using System.Collections.Immutable;
using Microsoft.Extensions.Logging;
using Pulumi.Automation;

namespace TestProject.Helpers;

public class MyWorkspaceStack(WorkspaceStack stack, ILogger logger)
{
    /// <summary>
    /// The Workspace the Stack was created from.
    /// </summary>
    public Workspace Workspace => stack.Workspace;

    /// <summary>
    /// The underlying stack DO NOT USE THIS
    /// </summary>
    public WorkspaceStack WorkspaceStack => stack;

    public Task<string> GetTagAsync(string key)
    {
        return stack.GetTagAsync(key);
    }

    public Task SetTagAsync(string key, string value)
    {
        return stack.SetTagAsync(key, value);
    }

    public Task RemoveTagAsync(string key)
    {
        return stack.RemoveTagAsync(key);
    }

    public Task<Dictionary<string, string>> ListTagsAsync()
    {
        return stack.ListTagsAsync();
    }

    public Task<ConfigValue> GetConfigAsync(string key)
    {
        return stack.GetConfigAsync(key);
    }

    public Task<ConfigValue> GetConfigAsync(string key, bool path)
    {
        return stack.GetConfigAsync(key, path);
    }

    public Task<ImmutableDictionary<string, ConfigValue>> GetAllConfigAsync()
    {
        return stack.GetAllConfigAsync();
    }

    public Task SetConfigAsync(string key, ConfigValue value)
    {
        return stack.SetConfigAsync(key, value);
    }

    public Task SetConfigAsync(string key, ConfigValue value, bool path)
    {
        return stack.SetConfigAsync(key, value, path);
    }

    public Task SetAllConfigAsync(IDictionary<string, ConfigValue> configMap)
    {
        return stack.SetAllConfigAsync(configMap);
    }

    public Task SetAllConfigAsync(IDictionary<string, ConfigValue> configMap, bool path)
    {
        return stack.SetAllConfigAsync(configMap, path);
    }

    public Task RemoveConfigAsync(string key)
    {
        return stack.RemoveConfigAsync(key);
    }

    public Task RemoveConfigAsync(string key, bool path)
    {
        return stack.RemoveConfigAsync(key, path);
    }

    public Task RemoveAllConfigAsync(IEnumerable<string> keys)
    {
        return stack.RemoveAllConfigAsync(keys);
    }

    public Task RemoveAllConfigAsync(IEnumerable<string> keys, bool path)
    {
        return stack.RemoveAllConfigAsync(keys, path);
    }

    public Task<ImmutableDictionary<string, ConfigValue>> RefreshConfigAsync()
    {
        return stack.RefreshConfigAsync();
    }

    public Task<UpResult> UpAsync(PulumiFn? program = null)
    {
        return stack.UpAsync(new() { Program = program, Logger = logger, LogToStdErr = true, LogFlow = true, LogVerbosity = LogVerbosity, OnStandardOutput = OnStandardOutput, OnStandardError = OnStandardError });
    }

    public Task<PreviewResult> PreviewAsync(PulumiFn? program = null)
    {

        return stack.PreviewAsync(new() { Program = program, Logger = logger, LogToStdErr = true, LogFlow = true, LogVerbosity = LogVerbosity, OnStandardOutput = OnStandardOutput, OnStandardError = OnStandardError });
    }

    public Task<UpdateResult> RefreshAsync()
    {
        return stack.RefreshAsync(new() { LogToStdErr = true, LogFlow = true, LogVerbosity = LogVerbosity, OnStandardOutput = OnStandardOutput, OnStandardError = OnStandardError });
    }

    public Task<UpdateResult> DestroyAsync()
    {
        return stack.DestroyAsync(new() { LogToStdErr = true, LogFlow = true, LogVerbosity = LogVerbosity, OnStandardOutput = OnStandardOutput, OnStandardError = OnStandardError });
    }

    public Task<ImmutableDictionary<string, OutputValue>> GetOutputsAsync()
    {
        return stack.GetOutputsAsync();
    }

    public Task<StackDeployment> ExportStackAsync()
    {
        return stack.ExportStackAsync();
    }

    public Task ImportStackAsync(StackDeployment state)
    {
        return stack.ImportStackAsync(state);
    }

    public Task<UpdateSummary?> GetInfoAsync()
    {
        return stack.GetInfoAsync();
    }

    public Task CancelAsync()
    {
        return stack.CancelAsync();
    }

    public void Dispose()
    {
        stack.Dispose();
    }

    /// <summary>
    /// The name identifying the Stack.
    /// </summary>
    public string Name => "TestWorkspace";
    
    void OnStandardOutput(string s) => logger.LogInformation(s);
    void OnStandardError(string s) => logger.LogError(s);
    private int LogVerbosity => 3;
}
