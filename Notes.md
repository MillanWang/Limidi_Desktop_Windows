# Notes

## Endpoints

- MidiControlChange
  - GET ?controlIndex=int&level=int
- MidiDevice
  - GET
  - PUT - ?deviceID=int
- MidiNote
  - GET ?isNoteOn=bool&noteNumber=int&velocity=int

## Deployment process

- Right click csproj/Publish
- Make IIS point to the file location of folder export
