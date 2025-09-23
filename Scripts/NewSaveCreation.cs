using Godot;
using System;

public partial class NewSaveCreation : Button {
    public void _on_button_up() {
        Globals.CreateSavesDir();

        string SaveName = GetNode<LineEdit>("../SaveNameTextBox").Text;
        string SaveFolderName = SaveName
            .Replace("/", "-")
            .Replace("\\", "-")
            .Replace(":", "-")
            .Replace("*", "-")
            .Replace("?", "-")
            .Replace("\"", "-")
            .Replace("<", "-")
            .Replace(">", "-")
            .Replace("|", "-");

        string SaveFolder = "user://saves/" + SaveFolderName;
        GD.Print("Creating Save: " + SaveFolder);
        DirAccess.MakeDirAbsolute(ProjectSettings.GlobalizePath(SaveFolder));

        using var save_json = FileAccess.Open(SaveFolder + "/" + "save.json", FileAccess.ModeFlags.Write);

        Globals.SaveJson = new Godot.Collections.Dictionary<string, Variant> {
            ["savename"] = SaveName,
            ["savefolder"] = SaveFolderName,
            ["playerposition"] = new Godot.Collections.Dictionary<string, int> {
                ["planet"] = 1,
                ["latitude"] = 0,
                ["longitude"] = 0,
                ["altitude"] = 1000
            },
            ["inventory"] = new Godot.Collections.Array<string>(),
            ["difficultysettings"] = Globals.DifficultySettings
        };

        GD.Print("Creating save.json: " + Json.Stringify(Globals.SaveJson));
        save_json.StoreString(Json.Stringify(Globals.SaveJson));

        if (!FileAccess.FileExists("user://save_list.txt")) {
            FileAccess.Open("user://save_list.txt", FileAccess.ModeFlags.Write);
        }

        using var SaveList = FileAccess.Open("user://save_list.txt", FileAccess.ModeFlags.ReadWrite);
        string SaveListContents = SaveList.GetAsText();

        // SaveList.SeekEnd();

        if (SaveListContents == "") {
            SaveListContents += SaveFolderName;
        } else {
            SaveListContents += ":" + SaveFolderName;
        }

        GD.Print("Updating Save List: " + SaveListContents);

        SaveList.StoreString(SaveListContents);
    }
}
