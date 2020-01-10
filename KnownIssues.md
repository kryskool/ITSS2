# XML-Message Parsing
If an invalid Unprocessed Message is sent, the parser may enter an endless loop.

# Server side shutdown
If multiple connections to the server are open disposing is not working properly.

# Roles of a connections
The role of a connection is not always fixed to client or server. Especially, if a proxy is used in the middle.

Examples:

IT-System (client) <-> (server) Proxy (client) <-> (server) Storage System
IT-System (client) <-> (server) Proxy (server) <-> (client) Digital Shelf
