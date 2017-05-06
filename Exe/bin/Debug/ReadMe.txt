SpyUO is a tool to see decrypted and decompressed packets sent and received by UO client.

It's divided into two parts:
- SpyUOLib.dll: this dll contains all packet hooking, Clients.cfg parsing, basic classes for packet reading and some defined packets.
- SpyUO.exe: this program uses SpyUOLib.dll to provide a GUI with some functionalities for packets data handling.

It requires the .Net Framework to run (http://msdn.microsoft.com/netframework).

Packets statistics are calculated as averages over the last 10 seconds (s = sent, r = received, T = total ).

Loot analyzer
- If you kill something and open its corpse, everything in corpse will be logged.
- When you stop killing, click analyze, then save and you will get a nice report containing all item details.
  You can copy paste this report in Excel (tab separated values).

Vendor analyzer
- If you buy something from a vendor or even if you just open vendor buy menu, every
  thing on the list will be logged.
- Click analyze and save. You will get a nice report containing all sell info.

Version 1.02 - 9.6.2009
- Added a few packets.
- Added loot analyzer.
- Added create item (from world item packet) function.

Version 1.03 - 10.8.2009
- Added a few packets.
- Added vendor analyzer.
- SpyUO is now compatible with enhanced client.
- SpyUO is now comaptible with 64 bit windows.

Special thanks to Folko (http://uo.elitecoder.net) for providing Clients.cfg.
Ultima.dll is developed by Krrios (http://www.runuo.com).
--
Lorenzo "Phenos" Castelli
E-Mail: gcastelli@racine.ra.it