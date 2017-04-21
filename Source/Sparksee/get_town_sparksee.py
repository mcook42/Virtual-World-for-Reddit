# -*- coding: utf-8 -*-
import lib.sparksee as sparksee
import DatabaseNames
from sys import argv, stdout
import json

__author__ = "Matt Cook"
__version__ = "1.0.0"
__contributors__ = ["Matthew Cook", "Caleb Whitman"]
__email__ = "mattheworion.cook@gmail.com"

# Tutorial here: http://www.sparsity-technologies.com/UserManual/API.html
# Documentation stored in Sparksee/doc
###########################################################
# Opening and running that database. This is about equivlant to running "psql start" in postgres.
# Ideally this will run once, and then the script will keep running in the background, keeping the db open.
###########################################################


# noinspection PyArgumentList
def main():
    """
    :string subr: name of root subreddit
    """
    if len(argv) < 2:
        print "No subreddit provided. Exiting."
        exit(1)

    # Get root subreddit name from command line
    subreddit = argv[1]

    # Set limit of nodes
    limit = 15

    # Opening config
    config = sparksee.SparkseeConfig()

    # Create a new sparksee instance
    sparks = sparksee.Sparksee(config)

    # Create the database. DatabaseNames contains the database names.
    db = sparks.open(DatabaseNames.dbNameGDB, False)

    ###########################################
    # Setting up a session for a specific user.
    ###########################################

    # create a new session
    sess = db.new_session()

    # Get the graph
    graph = sess.get_graph()

    # Get the types all up and running.
    subreddit_type_id = graph.find_type(DatabaseNames.subredditType)
    edge_type_id = graph.find_type(DatabaseNames.edgeType)

    # Get attributes up and running.
    name_attr_id = graph.find_attribute(subreddit_type_id, DatabaseNames.subredditAttributes[0][0])
    weight_attr_id = graph.find_attribute(edge_type_id, DatabaseNames.edgeAttributes[0][0])

    ##############################
    # Getting the nodes and edges.
    ##############################

    # Going from and to the database, you must use sparksee.Value() to input all values.
    v = sparksee.Value()
    v.set_string(subreddit)

    # Getting the nodes for a given depth
    # depth = 3
    graph = sess.get_graph()
    cursor = graph.select(name_attr_id, sparksee.Condition.EQUAL, v)

    # Get all edges for the node
    for node in cursor:
        # To hold data for JSON conversion
        # Structure (example):
        # { "center" : "s1", "nodes": ["s1","s2","s3","s4", "sn"], "edges" : [ [ "s1","s3","5" ], [ "s2", "s7", "6" ] }
        # data_dict = {"center": "", "nodes": [], "edges": [[]]}

        # Init storage for less ambiguity
        nodes = []
        edge_list = []

        # Edges are gotten using graph.explode
        edges = graph.explode(node, edge_type_id, sparksee.EdgesDirection.ANY)

        for edge in edges:
            # Can get the edge data using data.get_head() and data.get_tail()
            data = graph.get_edge_data(edge)

            # Check if tail is same as head. data.get_head() and data.get_tail() return the unique integer key for the nodes.
            if not data.get_head() == data.get_tail():
                # Get weight and store edge with weight in list of lists
                edge_list.append([str(graph.get_attribute(data.get_head(), name_attr_id).to_string()), \
                                  str(graph.get_attribute(data.get_tail(), name_attr_id).to_string()),
                                  int(graph.get_attribute(edge, weight_attr_id).get_integer())])

                # Add to node list for dict use later
                # All edges in the graph are undirected.
                #This means that the tail is not always the node we exploded on.
                #As a result, we must check to see which node (head or tail) should be added.

                #Add head if tail is center node.
                if data.get_tail() == node:
                        nodes.append(edge_list[-1][0])
                #Add tail if head is center node.
                else:
                        nodes.append(edge_list[-1][1])

        edges.close()

    try:
        # Sort edge_list in descending order by weight
        edge_list.sort(key=lambda x: int(x[2]), reverse=True)

        if len(edge_list) > 0 and len(edge_list[0]) > limit:
            # Drop all entries after 'limit'
            edge_list[0] = edge_list[0][limit]

        # Store in dictionary for easy translation to json
        data_dict = dict([('center', subreddit), ('nodes', nodes), ('edges', edge_list)])

    except UnboundLocalError:
        data_dict = dict([('center', subreddit), ('nodes', ""), ('edges', "")])

    # Turn to json.
    data_json = json.dump(data_dict, stdout, ensure_ascii=False)

    #Print a new line
    print ""

    # TODO: Make sure we have at least 'limit' nodes?
    # To get more edges one can do another explode using data.get_tail()
    # For indexes in nodes and edges array

    # Close cursor
    cursor.close()

    # Close the session
    if sess is not None:
        sess.close()

    #####################
    # Close the database.
    #####################

    if db is not None:
        db.close()
    if sparks is not None:
        sparks.close()

    return data_json


if __name__ == '__main__':
    main()

