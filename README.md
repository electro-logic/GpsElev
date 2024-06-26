# GpsElev

This small utility convert the GPS Altitude encoded in the GPS timestamp metadata

# Why this utility

Sony mirrorless cameras (ex. A6400, A7, etc..) write limited GPS metadata: latitude, longitude and timestamp to images when Location Information Linkage is enabled. Unfortunately the altitude is missing.

To overcome this limitation a custom app can send a manipulated timestamp containing the altitude information, this app will take care of putting the altitude data in the right metadata tag.

# How this works

GPS Timestamp is composed by 3 numbers in base 60, for example 02:04:50

The altitude information from the GPS/GNNS is usually not very accurated, so a decimal digit only is used.

The raw altitude is scaled by 10 and then converted from base 10 to base 60.

The maximum storable altitude is 60*60*60/10=21600 meters, that's enough for most applications.

# How to use

Run GpsElev.exe from your picture folder to automatically process all .ARW files of the folder. Exiftool.exe needs to be in the same folder.

# Requirements

.NET 8

Exiftool