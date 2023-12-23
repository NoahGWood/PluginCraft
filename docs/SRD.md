# PluginCraft Software Requirements Document

## Introduction

### Purpose
The purpose of this document is to outline the requirements for the development of PluginCraft, a cross-platform desktop application designed for Makers to control CNC machines and other devices. PluginCraft empowers users to craft their digital workspace by extending its' core functionality through plugins.

### Scope
PluginCraft aims to provide a user-friendly desktop app with a modular architecture, allowing Makers to control CNC machines and other electronic devices they build through an extensible plugin system.

## System Overview
### Product Perspective
PluginCraft will serve as a standalone desktop application with a core engine designed specifically for Makers to control CNC machines and other devices. The extensible plugin system will allow users to customize their experience.

### Product Features
* Cross-platform support (Windows, macOS, Linux)
* Core UI using EZImGui
* Plugin architecture for controlling CNC machines and other devices.

### User Classes & Characteristics
* **Makers:** Individuals who utilize PluginCraft for controlling CNC machines and devices.
* **Plugin Developers:** Individuals who develop additional features for PluginCraft to enhance its' capabilities.

## Functional Requirements
### Core Engine
#### Cross-Platform Support
PluginCraft must be compatible with the following Operating Systems:
* Windows
* macOS
* Linux

#### User Interface
The core UI will be implemented using EZImGui and ImGuiNet, providing a consistent and user-friendly experience for device control.

#### Plugin Support
* The application should allow dynamic loading and unloading of plugins.
* Plugins must seamlessly extned the application's functionality for device control.

### Plugin Development Kit
#### Plugin Interface
#### Device API

## Non-Functional Requirements
### Performance
* PluginCraft should have minimal impact on system resources during normal oeration.
* Plugins should not significantly degrade the application's performance.

## Naming/Branding
### Application Name:
PluginCraft
### Branding
[todo]