using Godot;
using System;

public partial class OpenDifficultySettings : Button {
    public override void _Ready() {
        Pressed += OnButtonPressed;
    }

    private void OnButtonPressed() {
        PackedScene DifficultySettings = GD.Load<PackedScene>("res://Scenes/DifficultySettings.tscn");

        Control DifficultySettingsInstance = DifficultySettings.Instantiate<Control>();

        DifficultySettingsInstance.Position = new Vector2(x: 0, y: 0);

        GetNode<Control>("/root/NewSave").AddChild(DifficultySettingsInstance);
    }
}
