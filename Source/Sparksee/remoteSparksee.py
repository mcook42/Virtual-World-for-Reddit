"""
    Simple socket server using threads
"""

import socket
import sys
from thread import start_new_thread
from get_town_sparksee import get_town

__author__ = "Matt Cook"
__version__ = "1.0.0"
__contributors__ = ["Matthew Cook"]
__email__ = "mattheworion.cook@gmail.com"

HOST = ""    # Symbolic name, meaning all available interfaces
PORT = 4269  # Arbitrary port

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
print "Socket created"

# Bind socket to local host and port
try:
    s.bind((HOST, PORT))
except socket.error as msg:
    print "Bind failed. Error Code : " + str(msg[0]) + " Message " + msg[1]
    sys.exit()

# Start listening on socket with 500 possible waiting connections
s.listen(500)


# Function for handling connections. This will be used to create threads
def client_thread(conn):
    # infinite loop so that function do not terminate and thread do not end.
    while True:

        # Receiving from client
        data = conn.recv(1024)

        # If no subreddit sent
        if not data:
            break

        # Get the town info in json format
        reply = get_town(data)

        # Send the data back to the client
        conn.sendall(reply)

    # came out of loop so close the connection
    conn.close()


# now keep talking with the client
while True:
    # wait to accept a connection - blocking call
    conn, addr = s.accept()
    print 'Connected with ' + addr[0] + ':' + str(addr[1])


    # Start new thread takes 1st argument as a function name to be run,
    # second is the tuple of arguments to the function.
    start_new_thread(client_thread, (conn,))
s.close()

