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
  - It's possible to setup IIS stuff and firewall openings with powershell. Just gotta integrate that into an installer process
