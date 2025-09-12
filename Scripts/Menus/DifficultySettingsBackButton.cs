using Godot;
using System;

public partial class DifficultySettingsBackButton : Button {
    public void _on_button_up() {
        GetNode<Control>("..").QueueFree();
    }
}
