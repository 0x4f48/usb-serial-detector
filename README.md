# usb-serial-detector
Application detecting USB serial port on Windows systems.

## What is this for?

This applications shows up COM port number, when a USB-serial device is plugged in. You don't need to visit device manager to figure out COM port number.


## How it works?

When the application starts, it will create an icon in the notification area of your task bar. If you insert a USB-serial device while the app is running, it will show up a port info Window. You can also use RIGHT-CLICK on its icon to show up the window.


## How to install?

You can build it using the source code or setup package under the *Installer/Output* directory. If you use the setup package, it will create a shortcut in the startup menu. In other words, it will be launched everytime you boot your system.


## etc

The *Inno Setup* is used for the installer. It's a free, but great installer application.  
