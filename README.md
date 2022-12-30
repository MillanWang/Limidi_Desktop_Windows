# Limidi Desktop (Work In Progress)

- A customizable MIDI controller mobile app that communicates with the accompanying Limidi desktop app to emulate a MIDI controller in your digital audio workstation (DAW) of choice.

- Developed using C#/.NET

## Required Software

- To run this desktop application, the free tool "LoopMIDI" by Tobias Erichsen is required.

    - <https://www.tobias-erichsen.de/software/loopmidi.html>

- LoopMIDI creates a virtual MIDI output device that forwared messages to a created virtual MIDI input device. Limidi Desktop sends MIDI messages to a virtual output device, which are then immediately forwarded to a virtual input device, which is named by the user. Your DAW will treat that virtual input device like a regular MIDI keyboard.

- This is a temporary workaround to unblock the mobile application development because that is the main thing to focus on for now. Eventually, I will develop ways to create MIDI devices dynamically directly in the deskptop app. Performance is a non-issue with the current approach,  it's just  some extra software steps.

## Limidi Mobile

- <https://github.com/MillanWang/Limidi_Mobile>

## Why the name "Limidi"?

- If you say "melody" really fast, it kinda sounds like "milidi", and when I produce music, I like to rearange melodies. If you sample "mi-li-di", and do a quick chop & flip, you get "Limidi".

- Limidi is also a city in Italy, which is kinda close to the city Milan, and my name is Millan. Shout out to all of my Italian friends!
