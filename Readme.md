# Pchtxt to IPS

[![License](https://img.shields.io/badge/License-MIT-blue.svg?logo=github&logoColor=5751ff&labelColor=2A2C33&color=5751ff&style=for-the-badge)](https://github.com/ArchLeaders/PchtxtToIps/blob/master/License.md) [![Downloads](https://img.shields.io/github/downloads/ArchLeaders/PchtxtToIps/total?label=downloads&logo=github&logoColor=37c75e&labelColor=2A2C33&color=37c75e&style=for-the-badge)](https://github.com/ArchLeaders/PchtxtToIps/releases) [![Latest](https://img.shields.io/github/v/tag/ArchLeaders/PchtxtToIps?label=Release&logo=github&logoColor=324fff&color=324fff&labelColor=2A2C33&style=for-the-badge)](https://github.com/ArchLeaders/PchtxtToIps/releases/latest)

Simple CLI tool to convert MSBT files to/from YAML

- [Pchtxt to IPS](#pchtxt-to-ips)
- [Usage](#usage)
  - [Single Input/Output](#single-inputoutput)
    - [Examples](#examples)
  - [Multiple Inputs/Outputs](#multiple-inputsoutputs)
    - [Examples](#examples-1)
- [Install](#install)
  - [Windows (x64)](#windows-x64)
  - [Windows (Arm64)](#windows-arm64)
  - [Linux (x64)](#linux-x64)
  - [Linux (arm64)](#linux-arm64)
  - [MacOS (x64)](#macos-x64)
  - [MacOS (Arm64)](#macos-arm64)
  - [Build From Source](#build-from-source)
- [Help](#help)

# Usage

> [!NOTE]
> *File extensions are ignored. **Every** file is assumed to be a **YAML** file unless the `MsgStdBn` magic is found.*

The input file can either be a **YAML** or **MSBT** file. The output file will always be the converted input file (regardless of the extension).

The tool can take any number of inputs immediately followed by `-o` or `--output` to specify a custom output path for the input (see examples).

Use `-f` or `--function-map` to load a function map file. This function map file will be used for every following conversion until the flag is used again. Setting the function map to `null`, `none`, `default` or `~` will reset the function map file.

> [!NOTE]
> [Aeon's](https://gitlab.com/AeonSake) `mfm` format is not supported, however you can use [MfmToYaml](https://github.com/ArchLeaders/MfmToYaml) to convert `mfm` files to YAML.

## Single Input/Output

```powershell
PchtxtToIps <input> [-o|--output OUTPUT_FOLDER]
```

### Examples

```powershell
PchtxtToIps ".\exefs\1.1.1.pchtxt"
```

```powershell
PchtxtToIps ".\exefs\1.1.1.ips" -o ".\exefs\"
```

## Multiple Inputs/Outputs

```powershell
PchtxtToIps <input> <input> ... [-o|--output OUTPUT_FOLDER]
```

### Examples

```powershell
PchtxtToIps ".\exefs\1.1.1.pchtxt" ".\exefs\1.2.1.pchtxt"
```

```powershell
PchtxtToIps ".\exefs\1.1.1.pchtxt" ".\exefs\1.2.1.pchtxt" -o ".\exefs\" 
```

```powershell
PchtxtToIps ".\exefs\1.1.1.pchtxt" -o ".\exefs\" ".\exefs\1.2.1.pchtxt" -o ".\exefs" 
```

# Install

## Windows (x64)

- Download and install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) runtime for Windows x64.
- Download the [latest win-x64 release](https://github.com/ArchLeaders/PchtxtToIps/releases/latest/download/PchtxtToIps-win-x64.zip)

## Windows (Arm64)

- Download and install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) runtime for Windows Arm64.
- Download the [latest arm-x64 release](https://github.com/ArchLeaders/PchtxtToIps/releases/latest/download/PchtxtToIps-win-arm64.zip)

## Linux (x64)

- Install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) x64 runtime for your linux distribution.
- Download the [latest linux-x64 release](https://github.com/ArchLeaders/PchtxtToIps/releases/latest/download/PchtxtToIps-linux-x64.zip)

## Linux (arm64)

- Install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) Arm64 runtime for your linux distribution.
- Download the [latest linux-arm64 release](https://github.com/ArchLeaders/PchtxtToIps/releases/latest/download/PchtxtToIps-linux-arm64.zip)

## MacOS (x64)

- Download and install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) runtime for MacOS x64.
- Download the [latest osx-x64 release](https://github.com/ArchLeaders/PchtxtToIps/releases/latest/download/PchtxtToIps-osx-x64.zip)

## MacOS (Arm64)

- Download and install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) runtime for MacOS Arm64.
- Download the [latest osx-arm64 release](https://github.com/ArchLeaders/PchtxtToIps/releases/latest/download/PchtxtToIps-osx-arm64.zip)

## Build From Source

```powershell
git clone "https://github.com/ArchLeaders/PchtxtToIps"
dotnet build PchtxtToIps
```

# Help

<a href="https://github.com/ArchLeaders/PchtxtToIps/issues">
  <img src="https://img.shields.io/github/issues/ArchLeaders/PchtxtToIps?style=for-the-badge&logoColor=c71b42&color=c71b42&labelColor=2A2C33&logo=github&label=Issues" alt="ArchLeaders' Website"/>
</a>

*If you need any help or found an issue, please create an [issue on GitHub](https://github.com/ArchLeaders/PchtxtToIps/issues) or contact me via [Email](mailto:archleaders@outlook.com) @ [archleaders@outlook.com](mailto:archleaders@outlook.com).*