
import DatabaseNames
import lib.sparksee as sparksee
import logging
from time import time, ctime

__author__ = "Caleb Whitman"
__version__ = "1.0.0"
__email__ = "calebrwhitman@gmail.com"

#Fill the database with the nodes and eges.
#It takes about 15 seconds to load 120,000 nodes and 110,000 edges. So about one edge every 0.00013 seconds or about 7333 edges per second.
#Three log files are written in the process. Logging.log logs the overall progres. EdgeLogging.log logs the edge progresss. NodeLogging.log logs the node progress.

#Prints all not empty nodes.
def printNodes(graph):
    value = sparksee.Value()
    value.set_string("")
    cursor = graph.select(name_attr_id, sparksee.Condition.NOT_EQUAL, value)
    for node in cursor:
        print graph.get_attribute(node,name_attr_id)

#Deletes all non empty nodes.
def dropAll(graph):

    value = sparksee.Value()
    value.set_string("")
    cursor = graph.select(name_attr_id, sparksee.Condition.NOT_EQUAL, value)
    for node in cursor:
        graph.drop(node)

    cursor.close()


#Adds the Nodes using the csv reader
#Errors for loading in the nodes written to NodeLoading.log
#Returns true if nodes are successfully loaded in. False otherwise.
def addNodes(sparksee,graph,nameColumn):
    logging.info("Nodes Before Input %s"%graph.count_nodes())
    csv = sparksee.CSVReader()
    csv.set_separator(",")
    csv.set_start_line(1)
    csv.open(file)
    # set attributes to be loaded and their positions
    attrs = sparksee.AttributeList()
    attrPos = sparksee.Int32List()
    # FULL_NAME attribute in the second column
    attrs.add(name_attr_id)
    attrPos.add(nameColumn)
    # import Subreddit node type
    ntl = sparksee.NodeTypeLoader(csv, graph, subreddit_type_id, attrs, attrPos)
    ntl.set_log_error("NodeLoading.log")
    try:
        ntl.run()
    except Exception:
        logging.error("Error: Nodes not loaded! Check Log file NodeLoading.log")
        return False
    csv.close()
    logging.info("Nodes After Input %s"%graph.count_nodes())
    return True

#Adds the edges to the graph.
#Returns True is nodes are successfully loaded in, false otherwise.
def addEdges(sparksee,graph):
    logging.info("Edges before input: %s" % (graph.count_edges()))
    # Reading in the edges
    csv = sparksee.CSVReader()
    csv.set_separator(",")
    csv.set_start_line(1)
    csv.open(file)
    # set attributes to be loaded and their positions
    attrs = sparksee.AttributeList()
    attrPos = sparksee.Int32List()

    attrs.add(edge_attr_id)
    attrPos.add(2)
    # import PEOPLE node type
    ntl = sparksee.EdgeTypeLoader(csv, graph, edge_type_id, attrs, attrPos, 0, 1, name_attr_id, name_attr_id)
    ntl.set_log_error("EdgeLoading.log")
    try:
        ntl.run()
    except Exception:
        logging.error("Error: Edges not loaded! Check Log file EdgeLoading.log")
        return False
    csv.close()
    logging.info("Edges after input: %s" % (graph.count_edges()))
    return True


#Adds the nodes one at a time and prints ndoes to output. Useful for debugging purposes.
#Not currently used.
def addNodesOneAtATime(sparksee,graph):
    # sess.begin()
    # http://www.sparsity-technologies.com/UserManual/API.html
    # Open the csv file, set the local if needed
    # sess.commit()
    csv = sparksee.CSVReader()
    csv.set_separator(",")
    csv.set_start_line(0)
    csv.open(file)
    row = sparksee.StringList()
    while csv.read(row):
        rowList = []
        for elem in row:
            rowList.append(elem)
        v = sparksee.Value()

        sess.begin()
        node = graph.new_node(subreddit_type_id)
        graph.new_edge(edge_type_id, node, node)
        try:
            graph.set_attribute(node, name_attr_id, v.set_string(rowList[0] + "%s" % (csv.get_row())))
            sess.commit()
        except RuntimeError:

            sess.rollback()
    csv.close()


def main():

    #Attributes and types used
    global name_attr_id,subreddit_type_id,edge_attr_id,edge_type_id,file

    ###########
    #Open everything
    #############

    #Variables to use.
    logFile="Loading.log"
    logging.basicConfig(filename=logFile, level=logging.DEBUG)
    file ="/scripts/uni_weighted_edge_list-latin1.csv"
    sess=None
    db= None
    sparks=None

    #Almost every operation here can throw an IO exception.
    try:
        logging.info("\nNew try started at %s"% ctime())
        #Loading in the configuration file. The configuration file specifies the max cache and the license.
        config = sparksee.SparkseeConfig()

        #Create a new sparksee instance
        sparks = sparksee.Sparksee(config)

        #Create the database
        db = sparks.open(DatabaseNames.dbNameGDB,False)

        #create a new session
        sess = db.new_session()

        #get the graph
        graph =  sess.get_graph()

        #######
        #Set up types and attributes.
        ######

        #Get the types all up and running.
        subreddit_type_id = graph.find_type(DatabaseNames.subredditType)
        edge_type_id = graph.find_type(DatabaseNames.edgeType)

        #Get attributes up and running.
        name_attr_id = graph.find_attribute(subreddit_type_id, DatabaseNames.subredditAttributes[0][0])

        edge_attr_id = graph.find_attribute(edge_type_id,DatabaseNames.edgeAttributes[0][0])

        #Add Nodes
        noError = True
        t1=time()
        logging.info("Loading Nodes first pass.")
        noError = addNodes(sparksee,graph,0)
        if(noError):
            t2=time()
            logging.info("\nTime to load %s nodes was about %s seconds.\n"%(graph.count_nodes(),t2-t1))
        else:
            "\nNodes not loaded.\n"
            logging.info("\nNodes not loaded.\n")

        if (noError):
            t1 = time()
            logging.info("Loading Nodes second pass.")
            noError = addNodes(sparksee, graph, 1)
            t2 = time()
            logging.info("\nTime to load %s nodes was about %s seconds.\n" % (graph.count_nodes(), t2 - t1))
        else:
            "\nNodes not loaded.\n"
            logging.info("\nNodes not loaded.\n")

        #add edges
        t1=time()
        if(noError):
            noError=addEdges(sparksee,graph)
            t2 = time()
            logging.info("Time to load %s edges was about %s seconds.\n" % (graph.count_edges(), t2 - t1))
        else:
            logging.info("Edges not loaded.\n")
    except Exception, e:
        logging.error("IO exception encountered. Make sure the database exists by running CreateDatabase.py. Exact exception is :%s\n" % e.message)
    finally:
        #close everything. If this is not called the database may not save the data into memory.
        if sess is not None:
            sess.close()
        if db is not None:
            db.close()
        if sparks is not None:
            sparks.close()

    logging.info("\nTry ended at %s" % ctime())

if __name__ == "__main__":
    main()
