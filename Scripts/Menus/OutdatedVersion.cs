using Godot;
using System;

public partial class OutdatedVersion : Control {
    public override void _Ready() {
        if (Globals.AccessLevel == "none") {
            GetNode<Label>("Center/Header/SecondaryLabel").Text = "Please update to play!";
            GetNode<Button>("Center/Buttons/ContinueButton").Visible = false;
        }
    }
}