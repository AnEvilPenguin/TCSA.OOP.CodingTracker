# TCSA.OOP.CodingTracker

A console based C# application used to track time spent coding. Developed using SQLite and Spectre.Console, 
with Bogus for data generation. Implemented using a CRUD and MVC based methodologies.

# How to Use

## Interactive

## Command Line


# Requirements

- [X] Application should create a sqlite database if one isn't present.
- [X] It should create a table in the database where sessions will be logged.
- [X] Users should be able to insert, delete, update, and view their logged sessions.
- [X] The user should use a specific format for date/time and not allow any other format.
- [X] Create a configuration file to contain database path and connection strings.
- [X] User cannot input the duration of the session this will be calculated based on the start and end times.
- [X] User should be able to input the start and end times manually.
- [X] Use Dapper ORM for data access.
- [X] When reading from database anonymous objects are not allowed.

## Stretch Goals

- [ ] Allow tracking of coding time using a stopwatch.
- [ ] Allow filtering of coding records per period.
- [ ] Allow ordering of records in ascending or descending order.
- [ ] Create reports where users can see their total and average coding session per period.
- [ ] Create the ability to set coding goals and how far the user is from reaching their goal.
- [X] Create demo data.

# Features

# Challenges

# Lessons Learned

# Areas to Improve

# Resources Used

- StackOverflow
- Dapper Docs
- Spectre.Console docs
- MS docs for ConfigurationManager
- MS docs on working with SQLite
- SQLite docs
