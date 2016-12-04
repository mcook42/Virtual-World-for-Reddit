#Use Case: 1, Server Responds to Requests and Processes Information

###Characteristic Information

**Primary Actor:** Server

**Scope:** Reddit Data Handling/Backend

**Level:** Background (?)

**Trigger:** Server recieves request

**Success End Condition:** Server remains updated and promptly responds to requests

**Failed End Condition:** Server responds to all requests with error messages

###Stakeholders/Interests

 * Reddit users: Must receive data in timely fashion

 * Server: Must correctly structure data and preserve user preferences

 * Reddit: Requires its data to be appropriately used and represented, no misuse via content downloading or uploading
 
###Preconditions

 * Server is currently running

 * Reddit is currently running and a proper connection exists via OAuth2

###Minimal Gaurantee

 * Server can receive and process requests, and respond with appropriate error messages or data obtained successfully.

 * Server recognizes the need to update its content and will attempt to do so

 * All data successfully required is correctly stored

###Main Success Scenario

1. Recognizes a need to update data

 a) Request from user

 b) Automatic update

 c) Processing of graphic-specific data

2. Respond to the request

 a) Query the table for data

 b) Request the data from Reddit

3. Process, format, and return the acquired data to the user

###Extensions

 * 2: Server overloaded with requests
 
   + 1: Ignore new requests until request queue has room to accept new requests
   
 * 2a: Query takes too long
 
   + 1: Notify developers
   
   + 2: Notify user
   
   + 3: Let query continue unless there is a lot of traffic
   
   + 4: Abort query; Send error message to user and developers 

 * 2b: Unable to retrieve information from Reddit
 
   + 1: Retry the request 5 times
   
   + 2: Respond to user with an appropriate error message
   
   + 3: Notify developers that Reddit is inaccessible
   
   + 4: Record that server may not be up to date
  
 * 1a: User spams server with requests

   + 1: Remember user
   
   + 2: Ignore all requests from user for a set time period
   
###SCHEDULE
   
**Due Date:** release 1.0
