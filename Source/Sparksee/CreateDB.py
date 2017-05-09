# -*- coding: utf-8 -*-
import sparksee
import DatabaseNames

__author__ = "Caleb Whitman"
__version__ = "1.0.0"
__email__ = "calebrwhitman@gmail.com"

#Creates the database "Map" and adds the appropriate types and attributes from the DatabaseNames module.
def main():

    #Loading in the configuration file. The configuration file specifies the max cache and the license.
    config = sparksee.SparkseeConfig()

    #Create a new sparksee instance
    sparks = sparksee.Sparksee(config)

    #Create the database
    db = None
    try:
        #see if it already exists
        db = sparks.open(DatabaseNames.dbNameGDB,False)
    except IOError:
        #If not create it.
        print "Database was not created before. Being created."
        db = sparks.create(DatabaseNames.dbNameGDB,DatabaseNames.dbName)

    #create a new session
    sess = db.new_session()

    #get the graph
    graph =  sess.get_graph()

    #Adding the types (Subreddit and edge) if they don't realy exist.
    subreddit_type_id = graph.find_type(DatabaseNames.subredditType)
    if subreddit_type_id == sparksee.Type.INVALID_TYPE:
        subreddit_type_id = graph.new_node_type(DatabaseNames.subredditType)
    edge_type_id = graph.find_type(DatabaseNames.edgeType)
    if edge_type_id == sparksee.Type.INVALID_TYPE:
        edge_type_id = graph.new_edge_type(DatabaseNames.edgeType, False, True) #undirected, neighbors indexed


    #Adding the attributes if they don't already exist.
    for attribute in DatabaseNames.subredditAttributes:
        attr_id = graph.find_attribute(subreddit_type_id, attribute[0])
        if sparksee.Attribute.INVALID_ATTRIBUTE == attr_id:
            attr_id = graph.new_attribute(subreddit_type_id, attribute[0], attribute[1], attribute[2])

    for attribute in DatabaseNames.edgeAttributes:
        attr_id = graph.find_attribute(subreddit_type_id, attribute[0])
        if sparksee.Attribute.INVALID_ATTRIBUTE == attr_id:
            full_name_attr_id = graph.new_attribute(subreddit_type_id, attribute[0], attribute[1],attribute[2])


    #Close everything
    sess.close()
    db.close()
    sparks.close()

    print "Database created."


if __name__ == '__main__':
  main()