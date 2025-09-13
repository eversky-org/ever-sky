using Godot;
using System;

public partial class DifficultySettingRow : ColorRect {
    [Export] public string HoverText = "";
    [Export] public bool IsTextBoxSetting = true;

    public override void _Ready() {
        MouseEntered += Hovered;
    }

    public override void _Process(double delta) {
        if (IsTextBoxSetting) {
            LineEdit Input = GetNode<LineEdit>("Input");

            foreach (char character in Input.Text) {
                if (character != '.' && !char.IsDigit(character)) {
                    Input.Text = Input.Text.Replace(character.ToString(), "");
                }
            }

            if (Input.Text.EndsWith('.') || Input.Text.StartsWith('.')) {
                Input.Text = "1.0";
            }
        }
    }

    private void Hovered() {
        GetNode<Label>("../../../SettingDescription");
    }
}
