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

        using var file = FileAccess.Open(SaveFolder + "/" + "save.json", FileAccess.ModeFlags.Write);

        Globals.SaveJson = new Godot.Collections.Dictionary<string, Variant>();

        Globals.SaveJson["savename"] = SaveName;
        Globals.SaveJson["savefolder"] = SaveFolderName;
        Globals.SaveJson["playerposition"] = new Godot.Collections.Dictionary<string, int> {
            ["planet"] = 1,
            ["latitude"] = 0,
            ["longitude"] = 0,
            ["altitude"] = 1000
        };
        Globals.SaveJson["inventory"] = new Godot.Collections.Array<string>();

        GD.Print("Creating save.json: " + Json.Stringify(Globals.SaveJson));
        file.StoreString(Json.Stringify(Globals.SaveJson));
    }
}
