using Godot;
using System;
using System.Text;
using System.Collections.Generic;

public partial class Version : Control {
    public override void _Ready() {
        Label VersionLabel = GetNode<Label>("VersionLabel");
        string GameVersion = ProjectSettings.GetSetting("application/config/version").AsString();

        VersionLabel.Text = "Version: " + GameVersion;

        if (Globals.VersionChecked) {
            Label LatestVersionLabel = GetNode<Label>("LatestVersionLabel");

            LatestVersionLabel.Text = "Latest Version: " + Globals.LatestVersion;
        } else {
            HttpRequest GetLatestVersionNode = GetNode<HttpRequest>("LatestVersionLabel/HTTPRequest");

            GetLatestVersionNode.RequestCompleted += OnRequestCompleted;

            string url = "https://everserver.puppet57.xyz/latest?version=" + GameVersion;

            GetLatestVersionNode.Request(url);
        }
    }

    private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body) {
        string ResponseBody = Encoding.UTF8.GetString(body);
        // GD.Print(ResponseBody);

        List<string> ResponseValues = [.. ResponseBody.Split(";")];

        Globals.LatestVersion = ResponseValues[0];
        Globals.AccessLevel = ResponseValues[1];

        Label LatestVersionLabel = GetNode<Label>("LatestVersionLabel");

        LatestVersionLabel.Text = "Latest Version: " + Globals.LatestVersion;

        Globals.VersionChecked = true;

        string GameVersion = ProjectSettings.GetSetting("application/config/version").AsString();

        if (Globals.AccessLevel != "all" || GameVersion != Globals.LatestVersion) {
            var error = GetTree().ChangeSceneToFile("res://Scenes/OutdatedVersion.tscn");

            if (error != Error.Ok) {
                GD.PrintErr("Failed to change scene: " + error);
            }
        }
    }
}