# Battery Notifier
## Description

Battery Notifier is a lightweight Windows application designed to help you manage your laptop's battery life more effectively. By monitoring the battery percentage and power status, the application provides timely notifications when your battery reaches a user-defined threshold. This helps prevent overcharging and extends the overall lifespan of your battery.

The application runs quietly in the system tray, ensuring it doesn't interfere with your workflow. You can customize the battery threshold and notification settings to suit your needs. Additionally, Battery Notifier supports custom notification sounds, allowing you to choose an alert that best grabs your attention.

Key features include:
- Real-time battery monitoring
- Customizable battery threshold for notifications
- Custom notification sounds
- System tray integration for minimal disruption
- Easy-to-use interface with settings accessible via a context menu

Battery Notifier is built using .NET 8 and leverages the NAudio library for audio playback, ensuring a smooth and reliable user experience.

## Installation

1. Clone the repository:
    git clone https://github.com/yourusername/battery-notifier.git
2. Open the solution in Visual Studio.
3. Build the project to restore the necessary NuGet packages.
4. Run the application.

## Usage

1. Set the desired battery threshold using the numeric up-down control.
2. Ensure notifications are enabled using the checkbox.
3. Minimize the application to the system tray.
4. The application will notify you when the battery reaches the specified threshold.

## Monetization

This application includes advertisements to support development. Ads are displayed in the notification form when the battery threshold is reached.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgements

- [NAudio](https://github.com/naudio/NAudio) for audio playback
- [Microsoft](https://www.microsoft.com) for .NET framework and Visual Studio

## User Guide

### Setting the Battery Threshold

1. Open the application.
2. Use the numeric up-down control to set the desired battery threshold percentage.
3. Ensure the checkbox for enabling notifications is checked.

### Running in the System Tray

1. Minimize the application to hide it in the system tray.
2. The application will continue to monitor the battery level and notify you when the threshold is reached.

### Custom Notification Sound

1. Replace the `notificationSoundPath` in the code with the path to your custom sound file.
2. The application will play this sound when the battery threshold is reached.

## Developer Guide

### Project Structure

- `Form1.cs`: Main form and logic for the application.
- `Program.cs`: Entry point of the application.

### Setting Up Development Environment

1. Clone the repository:
    git clone https://github.com/yourusername/battery-notifier.git
2. Open the solution in Visual Studio.
3. Build the project to restore the necessary NuGet packages.

### Coding Standards

- Follow C# coding conventions.
- Use meaningful variable and method names.
- Include comments to explain complex logic.

## API Documentation

Currently, the application does not expose any APIs.

## Changelog

### v1.0.0

- Initial release with basic battery monitoring and notification features.

## FAQ

### How do I change the notification sound?

Replace the `notificationSoundPath` in the code with the path to your custom sound file.

### How do I disable notifications?

Uncheck the checkbox for enabling notifications in the application settings.

### How do I exit the application?

Right-click the system tray icon and select "Exit" from the context menu.
