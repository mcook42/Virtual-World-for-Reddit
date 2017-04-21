"""
    Simple socket server using threads
"""

import socket
import sys
from thread import start_new_thread
import get_town_sparksee as gt

HOST = ""    # Symbolic name, meaning all available interfaces
PORT = 8888  # Arbitrary non-privileged port

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
print "Socket created"

# Bind socket to local host and port
try:
    s.bind((HOST, PORT))
except socket.error as msg:
    print "Bind failed. Error Code : " + str(msg[0]) + " Message " + msg[1]
    sys.exit()

print "Socket bind complete"

# Start listening on socket
s.listen(10)
print "Socket now listening"


# Function for handling connections. This will be used to create threads
def client_thread(conn):
    # infinite loop so that function do not terminate and thread do not end.
    while True:

        # Receiving from client
        data = conn.recv(1024)

        reply = gt.main(data)
        if not data:
            break

        conn.sendall(reply)

    # came out of loop
    conn.close()


# now keep talking with the client
while 1:
    # wait to accept a connection - blocking call
    conn, addr = s.accept()
    print 'Connected with ' + addr[0] + ':' + str(addr[1])

    # Start new thread takes 1st argument as a function name to be run,
    # second is the tuple of arguments to the function.
    start_new_thread(client_thread, (conn,))

s.close()

