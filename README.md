# VitalInfo server for Windows

## See vital information about your computer on your N900.
This is the "server" software which sends information about your computer over UDP to a specified IP-address (your N900).
To be used with the Nokia N900 [display script](https://github.com/Logima/vinfo-display).

Data to send:
* CPU-usage per core
* Total CPU-usage
* Total RAM
* Used RAM
* Network usage on specified adapter
* Local and public IP-addresses
* Volume percent and mute status on default playback device
* Space left on local disks
* Now Playing -information

### IP resolving
VitalInfo relies on external website to ask public IP from. It needs to be as plain as possible, just containing the IP.
The hostname of a website should be inserted under "MyIP Host" without the http://-prefix.

### Now Playing -information
Now Playing -information is being read from a text-file and is assumed to be in the following format:

    artist -+- title -+- "1" if paused -+- position -+- length

I use foobar2000 with [Now Playing Simple](http://skipyrich.com/wiki/Foobar2000:Now_Playing_Simple)-plugin to generate this.
Formatting string for Now Playing Simple:

    $if(%isplaying%,%artist% -+- %title% -+- %ispaused% -+- %playback_time% -+- %length%)
