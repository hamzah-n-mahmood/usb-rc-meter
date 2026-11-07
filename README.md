# Building a USB RC Meter

Files for the YouTube series **Building a USB RC Meter** by Dr. Hamzah Naser Mahmood on [WASLA | وصلة](https://www.youtube.com/@WaslaEngineering) (Arabic for a link, an electrical connection).
The series rebuilds my 2014 B.Eng. (Hons) final-year project (Northumbria University / KDU College, Penang):
a USB-powered meter that measures resistance and capacitance, with a PIC16F886 and a small Windows app.
Playlist: (link after upload)

Explore every number interactively on the companion dashboard: https://hamzah-n-mahmood.github.io/usb-rc-meter/ (the page is also in `docs/`)

## Episodes

| # | Episode | Video | Worksheet | Data |
|---|---|---|---|---|
| EP00 | Why I'm Rebuilding My 2014 Degree Project (meet your host) | (link after upload) |  |  |
| EP01 | Why build a meter? How instruments measure R and C | (link after upload) | [worksheet](worksheets/EP01_Worksheet.pdf) | [data](data/EP01/) |
| EP02 | The transistor as a constant-current source | (link after upload) | [worksheet](worksheets/EP02_Worksheet.pdf) | [data](data/EP02/) |
| EP03 | Auto-ranging: current stages, analog switches and the ADC | (link after upload) | [worksheet](worksheets/EP03_Worksheet.pdf) | [data](data/EP03/) |
| EP04 | Measuring capacitance with time | (link after upload) | [worksheet](worksheets/EP04_Worksheet.pdf) | [data](data/EP04/) |
| EP05 | Firmware, UART and the PC app (and two bugs) | (link after upload) | [worksheet](worksheets/EP05_Worksheet.pdf) | [data](data/EP05/) |
| EP06 | Does it actually work? Testing, accuracy and a 2026 redesign | (link after upload) | [worksheet](worksheets/EP06_Worksheet.pdf) | [data](data/EP06/) |

## What's in this repository

| Folder | What it holds |
|---|---|
| `worksheets/` | One worksheet per episode, with answers (Word and PDF) |
| `data/` | The measurement data used in each episode, as CSV files. See [data/README.md](data/README.md) |
| `firmware/` | `CapResMeter.c`: my original 2014 firmware for the PIC16F886 (mikroC PRO for PIC), unchanged |
| `pc_app/` | My original 2014 Windows app (VB.NET, Visual Studio 2012, .NET 4.5), source files only, unchanged |

The code is published as it was in 2014, mistakes included: Episode 5 walks through them. Known issues:
- The firmware never writes OSCCON, so the PIC16F886 may run at its 4 MHz reset default rather than the 8 MHz the code assumes. Fix: `OSCCON = 0x70;` at the start of `main`, then verify the timing.
- Interrupts are enabled (GIE, PEIE, RCIE, TMR1IE) but no interrupt routine is written. Fix: keep them off while polling, or write a handler.
- Capacitance is timed by software polling, which adds 5–10 Timer1 counts; the app subtracts 5. Better: let comparator C2 gate Timer1 (T1GSS = 0, TMR1GE = 1).
- The app finds the USB–UART bridge by its device name, which only works with that adapter's driver.

### Building the PC app
- Open `pc_app/RC_Measurement/RC_Measrement.sln` in Visual Studio (the original spelling of the file name is kept).
- Signing and the local publish path are switched off, and build output is not included.
- The form's background picture is not included (it was a downloaded wallpaper). Add any image at
  `pc_app/RC_Measurement/Resources/cool-background-photos-2-2-s-307x512.jpg`, or remove the form's background image in the designer.

## Licence
Everything here is © 2014, 2026 Hamzah Naser Mahmood, licensed under
[CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/): free to share and adapt with credit, not for commercial use.
See [LICENSE](LICENSE).

## The thesis
"Computer-Based RC Meter", B.Eng. (Hons) final-year project, April 2014. A copy should still be in the KDU College library archives.
