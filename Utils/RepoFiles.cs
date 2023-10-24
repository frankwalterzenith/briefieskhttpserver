using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace KernelHttpServer.Utils;

internal static class RepoFiles
{
    /// <summary>
    /// Provides the path to the skills directory.
    /// </summary>
    /// <returns>The full path to the skills directory</returns>
    internal static string SampleSkillsPath()
    {
        // Temporary logger for debugging
        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = loggerFactory.CreateLogger("RepoFiles");

        // Get the location of the KernelHttpServer assembly
        var assemblyLocation = Assembly.GetExecutingAssembly().Location;

        if (string.IsNullOrWhiteSpace(assemblyLocation))
        {
            throw new InvalidOperationException("Failed to retrieve the location of the executing assembly.");
        }

        // Go up two directories from the current assembly location to get to the project's root directory.
        var projectRootDirectory = Directory.GetParent(Directory.GetParent(Path.GetDirectoryName(assemblyLocation)).FullName).FullName;

        // Log the project root directory
        logger.LogInformation($"Project root directory: {projectRootDirectory}");

        // The skills directory is assumed to be a subdirectory named "skills" of the project root directory.
        var skillsDirectory = Path.Combine(projectRootDirectory, "skills");

        if (!Directory.Exists(skillsDirectory))
        {
            throw new InvalidOperationException($"Skills directory not found at expected location: {skillsDirectory}");
        }

        return skillsDirectory;
    }
}
