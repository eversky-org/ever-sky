using Godot;
using System;

public partial class Globals : Node {
    public static string LatestVersion = "";
    public static string AccessLevel = "";
    public static bool VersionChecked = false;

    public static Godot.Collections.Dictionary<string, Variant> DifficultySettings = new() {
        ["weather-damage"] = 1f,
        ["enemy-damage"] = 1f,
        ["pvp"] = "no-one",
        ["base-building"] = "no-one",
    };

    public static Godot.Collections.Dictionary<string, Variant> SaveJson = [];

    public static void CreateSavesDir() {
        string folderPath = "user://saves";

        var dir = DirAccess.Open(folderPath);
        if (dir != null) {
            GD.Print("Folder exists: " + folderPath);
        } else {
            GD.Print("Creating saves folder");
            DirAccess.MakeDirAbsolute(ProjectSettings.GlobalizePath(folderPath));
        }
    }
}