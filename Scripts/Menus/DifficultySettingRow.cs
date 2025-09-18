using Godot;
using System;

public partial class DifficultySettingRow : ColorRect {
    [Export(PropertyHint.MultilineText)] public string HoverText = "";
    [Export] public string SettingName = "";
    [Export(PropertyHint.Enum, "Float,NoOne Group Anyone")]
    public string SettingType { get; set; } = "Float";

    public override void _Ready() {
        MouseEntered += Hovered;
    }

    public override void _Process(double delta) {
        if (SettingType == "Float") {
            LineEdit Input = GetNode<LineEdit>("Input");

            foreach (char character in Input.Text) {
                if (character != '.' && !char.IsDigit(character)) {
                    Input.Text = Input.Text.Replace(character.ToString(), "");
                }
            }

            if (Input.Text.EndsWith('.') || Input.Text.StartsWith('.')) {
                Input.Text = "1.0";
            }
        } else if (SettingType == "NoOne Group Anyone") {
            Button NoOne = GetNode<Button>("Buttons/NoOne");
            Button Group = GetNode<Button>("Buttons/Group");
            Button Anyone = GetNode<Button>("Buttons/Anyone");

            if (NoOne.ButtonPressed) {
                Globals.DifficultySettings[SettingName] = "no-one";
            } else if (Group.ButtonPressed) {
                Globals.DifficultySettings[SettingName] = "group";
            } else if (Anyone.ButtonPressed) {
                Globals.DifficultySettings[SettingName] = "anyone";
            }
        }
    }

    private void Hovered() {
        GetNode<Label>("../../../SettingDescription").Text = HoverText;
    }
}
