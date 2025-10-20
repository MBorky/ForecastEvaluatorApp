ForecastEvaluatorApp
A comprehensive weather forecast evaluation system built with ASP.NET Core 8.0 that collects and stores weather predictions from multiple meteorological models alongside real anemometer readings for forecast accuracy analysis.

Overview
ForecastEvaluatorApp is a data aggregation and storage system designed to collect weather forecasts from external meteorological services (ICON and METEO-FRANCE models) and combine them with real-world anemometer measurements. The application serves as a foundation for evaluating forecast accuracy by providing structured data storage and automated data collection capabilities.

Key Features
Multi-Model Data Collection: Automated retrieval of weather forecasts from ICON and METEO-FRANCE meteorological models

Anemometer Data Integration: Real-time collection and storage of wind measurements from anemometer devices

Structured Data Storage: Organized database schema optimized for forecast vs. actual data comparison

Background Processing: Automated data collection using Hangfire for scheduled updates

Hourly Forecast Details: Granular wind data storage including speed, direction, and gusts (10:00-18:00 daily window)

RESTful Update API: Simple endpoints for triggering data collection from external sources

Architecture
The application follows Clean Architecture principles with clear separation of concerns:

text
├── DataModels/          # Entity models and database context
├── Endpoints/           # Minimal API endpoint definitions  
├── Services/            # Business logic and external API integration
├── Extensions/          # Application configuration extensions
├── Migrations/          # Entity Framework database migrations
└── Properties/          # Application properties and settings
Core Components
WeatherForecast: Main forecast entity with model type (ICON/METEO-FRANCE), retrieval date, and target forecast date

ForecastDetail: Hourly wind data including speed, direction, and gusts for 8-hour daily periods (10:00-18:00)

AnemometerReading: Real-world wind measurements with precise timestamps for accuracy comparison

WeatherService: External API integration service for meteorological data collection

AnemometerService: Data processing service for anemometer readings integration

Technology Stack
.NET 8.0: Latest framework with minimal APIs

Entity Framework Core: Database access with migrations support

SQL Server: Primary database with in-memory option for testing

Hangfire: Background job processing for automated data collection

HttpClientFactory: Resilient external API communication

System.Text.Json: High-performance JSON deserialization

Docker: Linux containerization support

API Endpoints
Weather Data Collection
GET /weather-forecasts/update-icon-forecast - Trigger ICON model forecast update

GET /weather-forecasts/update-france-meteo-forecast - Trigger METEO-FRANCE model forecast update

Anemometer Data Collection
GET /anemometer-readings/update-anemometer - Trigger anemometer data collection and storage

Data Collection Workflow
External API Integration: Services fetch JSON data from configured meteorological APIs

Data Validation: Input validation ensures data integrity and proper API key handling

Structured Storage: Forecast data is parsed and stored with proper relationships

Hourly Granularity: Wind data captured for 8-hour periods (10:00-18:00) optimized for comparison analysis

Background Scheduling: Hangfire manages automated data collection cycles

Database Schema
WeatherForecasts: Master forecast records with model identification and date tracking

ForecastDetails: Detailed hourly wind measurements (speed, direction, gusts)

AnemometerReadings: Real-world wind measurements for accuracy validation

External Integrations
The application integrates with:

ICON Weather Model: German meteorological service API

METEO-FRANCE: French national meteorological service API

Anemometer Devices: Real-time wind measurement equipment

This application serves as a robust data collection and storage foundation for meteorological analysis, providing clean, structured datasets for forecast accuracy evaluation and meteorological research.

The structured data collected by ForecastEvaluatorApp serves as the foundation for forecast accuracy evaluation.
