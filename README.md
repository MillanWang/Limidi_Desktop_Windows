# Limidi Desktop (Work In Progress)
A customizable MIDI controller mobile app that communicates with the accompanying Limidi desktop app to emulate a MIDI controller in your digital audio workstation (DAW) of choice. 

Developed using C#/.NET


## Required Software
To run this desktop application, the free tool "LoopMIDI" by Tobias Erichsen is required. 

https://www.tobias-erichsen.de/software/loopmidi.html

LoopMIDI creates a virtual MIDI output device that forwared messages to a created virtual MIDI input device. Limidi Desktop sends MIDI messages to the virtual output device, which are then immediately sent to the virtual input device. The virtual input device is treated like a regular plugged in MIDI keyboard in your DAW of choice. 

This is a temporary workaround to unblock the mobile application development, and eventually, I will develop ways to create MIDI devices dynamically, or get this loopMIDI tool licensed for distribution and built directly into Limidi desktop. 


## Limidi Mobile 
https://github.com/MillanWang/Limidi_Mobile





## Why the name "Limidi"?
If you say "melody" really fast, it kinda sounds like "milidi", and when I produce music, I like to rearange melodies. If you sample "mi-li-di", and do a quick chop & flip, you get "Limidi". 

Li can be translated to "strength" in Chinese, and the average user of this app will be strong with MIDI. 

Limidi is also a city in Italy, which is kinda close to the city Milan, and my name is Millan. Shout out to all of my Italian friends!