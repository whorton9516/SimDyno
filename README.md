# SimDyno
SimDyno is a desktop application designed to take telemetry data from racing games and display live metrics. The goal is to help users analyze vehicle data in real time to improve their racing performance.


## Overview

SimDyno provides a dashboard with live telemetry data such as speed, RPMs, power, boost, torque, tire temperatures, tire slip, and suspension travel. It also includes lap-by-lap performance history, race history, social challenges, and milestone tracking. Future updates aims to integrate a machine learning model that learns your driving habits and offers personalized coaching tips.

## Features

- **Dashboard with Car Data**: Displays live telemetry data including speed, RPMs, power, boost, torque, tire temps, tire slip, and suspension travel.
- **Lap-by-Lap Performance History**: Records and shows detailed performance metrics for each lap.
- **Race History**: Summarizes past race statistics to help analyze overall performance.
- **Social Challenges & Milestone Tracking**: Engages users through challenges and community competitions.
- **Future Machine Learning Integration**: Planned AI features will learn your driving habits and provide personalized coaching.

## Architecture

SimDyno is composed of two main components:

- **Backend**: An ASP.NET web app built in C# that receives, parses, and processes telemetry data. It uses SignalR to stream data in real time.
- **Frontend**: An Electron-based desktop application with a React UI written in TypeScript. Popular UI libraries such as Mantine UI and Material UI are used to ensure a clean, modern, and aesthetic design.

## Contributing
Contributions are welcome! Please review [CONTRIBUTING.md](./contributing.md) for guidelines on how to contribute. We follow a clear branching strategy and require code reviews for pull requests to maintain code quality.

## License
This project is licensed under the [MIT License](./LICENSE).
