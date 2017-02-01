using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class BuildTool
{
    [MenuItem("BuildTool/Build")]
    private static void Build()
    {
        CustomizedCommandLine();

        string destinationPath = Path.Combine(_destinationPath, PlayerSettings.productName);
        destinationPath += GetExtension();

        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, destinationPath, EditorUserBuildSettings.activeBuildTarget, BuildOptions.None);
    }


    private static string _destinationPath;
    private static void CustomizedCommandLine()
    {
        Dictionary<string, Action<string>> cmdActions = new Dictionary<string, Action<string>>
        {
            {
                "-destinationPath", delegate(string argument)
                {
                    _destinationPath = argument;
                }
            }
        };

        Action<string> actionCache;
        string[] cmdArguments = Environment.GetCommandLineArgs();

        for (int count = 0; count < cmdArguments.Length; count++)
        {
            if (cmdActions.ContainsKey(cmdArguments[count]))
            {
                actionCache = cmdActions[cmdArguments[count]];
                actionCache(cmdArguments[count + 1]);
            }
        }

        if (string.IsNullOrEmpty(_destinationPath))
        {
            _destinationPath = Path.GetDirectoryName(Application.dataPath);
        }
    }


    private static string GetExtension()
    {
        string extension = "";

        switch (EditorUserBuildSettings.activeBuildTarget)
        {
            case BuildTarget.StandaloneOSXIntel:
            case BuildTarget.StandaloneOSXIntel64:
            case BuildTarget.StandaloneOSXUniversal:
                extension = ".app";
                break;
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                extension = ".exe";
                break;
            case BuildTarget.Android:
                extension = ".apk";
                break;
            case BuildTarget.iOS:
                extension = ".ipa";
                break;
        }

        return extension;
    }
}