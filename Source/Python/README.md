**All python scripts are run on the Ubuntu server (all written by Matt)**

 **Authbot-sanspassword.py**
 
 This script deals with authenticating our reddit bot and providing the OAUTH2 credentials to our reddit bots.
 
 **csvToDatabase.py**
 
 This script deals with cleaning the data grabbed from BigQuery and inserting the info into the postgresql database.
 
 **dbInteractions-sanspassword.py**
 
 This script deals with providing utility functions for database interactions.
 
 **initDataCollection.py**
 
 This script is a run daily to update our database of subreddits and their subscriber counts.
 
 **makeMap-np.py**
 
 This script is used to create the bipartite graph of the subreddits and authors.  It saves the weighted edge list to a csv for
 later use in the wglToUnipartite.py.  The two scripts are run separately due to a memory issue
 
 **wglToUnipartite.py**
 
 This script is used to create the final unipartite network of subreddits.  It save the weighted edgelist to a csv to later be imported to the Sparksee database.  This techinque proved to be faster than inserting the data straight from here to the Sparksee database.
