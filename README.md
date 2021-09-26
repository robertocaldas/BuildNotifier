# Build Notifier

Build Notifier is a tool to send Push Notifications using Pushover (https://pushover.net) to devices when Unity finishes a build.

## Installation

In Unity, open the Package Manager.
Click the plus button (Add) and select `Add Package from git URL...`.
Add the .git URL (the same URL as if you were cloning this project), and press the `Add` button.
Unity should install the package and display it in the Package Manager list.

## Usage

Create an Application on Pushover, add your devices to it.

In Unity, open `Tools > Build Notifier`. Add the API token, the User key and press the `Test` button.
It should send a notification to your device.

That's it, now when you build you should get a notification.

## Remarks

- When building iOS the notification is going to be sent when Unity finishes building, not when XCode finishes building.
- Because of a bug in Unity the format of the messages will be different depending on the status (failure/success), and success may (will?) show with an unknown status.