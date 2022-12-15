# Salve Test Api Service

The backend API service is run on .NET core v6.

To run, open solution folder from your desired IDE and debug.

The application is set to run on ssl via development certificate on port 7145.

## Key features

Code uses the new minimal API scaffolding rather than controllers
Code is production ready, but there are some key features missing
- Caching
- Logging
- Resilience
- Stronger configuration management
- etc

Code follows strict SOLID principles

## Testing

Test coverage is not fantastic given severe time constraints and boilerplate code setup.

Current testing has been stripped down to the tests with most value and a component test to cover real edge to edge mapping to ensure service can interpret data from CSV files.

## Next steps
- Increase code coverage
- Implement caching
- Store clinics and patient data in database or distributed cache and periodically update using dedicated sync workers