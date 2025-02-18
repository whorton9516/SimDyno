# Contributing to SimDyno

We appreciate your interest in contributing to SimDyno! By participating in this project, you agree to abide by our code of conduct and follow these guidelines.

## How to Contribute

There are several ways you can help improve SimDyno:
- **Reporting Issues:**  
  If you encounter a bug or have a feature request, please check the [Issue Tracker](../../issues) before opening a new issue.
- **Submitting Pull Requests:**  
  Fork the repository, create a branch for your work, commit your changes, and then open a pull request. We require that all pull requests are linked to an open issue when applicable.

## Branching and Naming Conventions

- **Branch Names:**  
  Use descriptive names for your branches. For example:  
  - `feature/LiveTelemetryDashboard`  
  - `bugfix/SignalRConnectionIssue`
- **Commit Messages:**  
  Write clear and concise commit messages. Include references to relevant issues when applicable (e.g., `Fixes #42`).

## Coding Guidelines

- **Naming Conventions:**  
  - When creating new methods, follow the pattern: `ObjectName_Action` (e.g., `Dashboard_UpdateMetrics`).
  - Use `var` for variable declarations where appropriate.
  - For private properties, prefix with an underscore (e.g., `_settings`), while public properties should be in UpperCamelCase.
- **Code Style:**  
  Ensure that your code adheres to the established coding standards. Run tests locally before submitting your changes.
- **Documentation:**  
  Update or add documentation as needed, especially if your changes affect how the app works. Please ensure the `Docs` folder is updated with relevant details.

## Pull Request Process

1. Fork the repository and create your branch from `main` or `develop`.
2. Make your changes in your branch.
3. Ensure all tests pass and add new tests if needed.
4. Submit a pull request with a clear description of your changes and reference the corresponding issue.
5. Respond to any feedback and make necessary changes.

## Code Review

- All pull requests will be reviewed by one or more maintainers.
- Maintain a collaborative and respectful tone during code reviews.
- Be prepared to make adjustments based on feedback.

## Reporting Issues

- When reporting issues, include as much detail as possible:
  - A clear description of the problem or feature request.
  - Steps to reproduce the issue.
  - Any logs or error messages if applicable.
- For design-related suggestions, feel free to provide sketches or examples.

## Getting Started

Before contributing, please take a moment to familiarize yourself with:
- The [README.md](README.md) for a project overview.
- The [Docs](./Docs) folder for detailed technical documentation.
- Our [Code of Conduct](CODE_OF_CONDUCT.md) to understand our expectations for community behavior.

## License

By contributing, you agree that your contributions will be licensed under the MIT License, as is the rest of the project.

## Questions or Feedback

If you have any questions or suggestions regarding the contribution process, please open an issue or contact one of the maintainers.

Thank you for helping make SimDyno better!
