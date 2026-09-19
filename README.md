# IT Service Asset Management

A full-stack IT Service and Asset Management System built using React, ASP.NET Core, Entity Framework Core, and MySQL.

## Overview

This application is designed to manage an organization's IT assets, employees, maintenance activities, and IT support tickets through a centralized system.

The system provides a React-based frontend, an ASP.NET Core Web API backend, and a MySQL database.

## Features

### Asset Management
- Create, view, update, and delete IT assets
- Track asset type and status
- Store serial number and asset tag
- Store manufacturer and model information
- Track purchase and warranty information
- Track asset location
- Assign assets to employees
- Return assigned assets
- Manage asset lifecycle status

### Employee Management
- Create and manage employee records
- Store employee ID, name, email, department, and designation
- Associate employees with assigned IT assets

### Maintenance Management
- Record asset maintenance issues
- Track reported and completed dates
- Record maintenance resolution
- Record maintenance cost
- Automatically manage asset repair status during maintenance

### Ticket Management
- Create IT support tickets
- Assign tickets to employees and assets
- Set ticket priority
- Track ticket status
- Ticket workflow:

  `Open → In Progress → Resolved → Closed`

- Start, resolve, and close tickets
- Validate employee and asset relationships

### Dashboard
- Total assets
- Available assets
- Assigned assets
- Assets under repair
- Retired assets
- Total employees
- Open tickets
- In-progress tickets
- Resolved tickets

## Technology Stack

### Frontend
- React
- TypeScript
- Vite
- React Router
- Axios
- CSS

### Backend
- ASP.NET Core Web API
- .NET 10
- C#
- Entity Framework Core
- Swagger / OpenAPI

### Database
- MySQL

### Development Tools
- Visual Studio
- Visual Studio Code
- Git
- GitHub

## Architecture

```text
React + TypeScript
        |
        | HTTP / REST API
        ↓
ASP.NET Core Web API
        |
        | Entity Framework Core
        ↓
      MySQL
