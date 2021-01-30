# ITSS2
ITSS2 is an implementation of the WWKS2 protocol. The WWKS2 protocol is used in a pharmacy or hospital environment to enable a communication between IT-Systems and Storage Systems. This library is implemented based on the .NET Framework and is written in C#. These are the key features at the moment:

- Supports two different layers of abstraction of the business logic: dialogs and workflows. At the workflow level you have a simple access to the logic of the protocol. You don't have to care of the details. If you'd like to be more flexible, you can use the APIs at the dialog level. Here you have the full control what message is sent and when.
- Provides a strict separation of protocol messages and its transfer and serialization. As serialization formats XML and JSON are currently available. But it is quite easy to introduce new transfer modes and serialization formats without changing the existing business logic.
- The library is extendible. Basically it provides you with the standard implementation of the WWKS2 protocol. If you wish to introduce new dialogs or extend existing ones, this is easily possible. That means there is a clear distinction between what is standard and what are your own extensions.
- The participants of a connection are not fixed tied to a client or server role.
- Simple and straight forward error handling. If a message cannot be handled for some reason at your side of the connection, you have the chance to get notified with all details about what went wrong and why.

# Current status
At the moment the code is an a development phase. So no Beta, RC or even Release status. It should be treated as a technical preview. The next steps are to continue testing and fix the found bugs.

# What's coming up next?
The features/topics mentioned below will be provided in the future. -Not necessarily in that order.-
As I'm doing all of the coding in my free time, it may take some time.

- Extending unit tests
- Workflow layer
- Format bridge
- Documentation (API/Architecture)
- TCP and file system transfer modules
- TLS encryption
- Samples
- Simulators
