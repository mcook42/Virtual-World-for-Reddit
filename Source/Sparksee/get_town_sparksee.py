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
def main(subr):
    """
    :string subr: name of root subreddit
    """
    if subr is '':
        print "No subreddit provided. Exiting."
        exit(1)

    # Get root subreddit name from command line
    subreddit = subr

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
        edges = graph.explode(node, edge_type_id, sparksee.EdgesDirection.OUTGOING)

        for edge in edges:
            # Can get the edge data using data.get_head() and data.get_tail()
            data = graph.get_edge_data(edge)

            # Check if tail is same as head...
            if graph.get_attribute(data.get_tail(), name_attr_id).to_string() is not subreddit:
                # Get weight and store edge with weight in list of lists
                edge_list.append([subreddit, str(graph.get_attribute(data.get_tail(), name_attr_id).to_string()),
                                  int(graph.get_attribute(edge, weight_attr_id).get_integer())])

            # Add to node list for dict use later
            nodes.append(edge_list[-1][1])

        edges.close()

    try:
        # Sort edge_list in descending order by weight
        edge_list.sort(key=lambda x: int(x[2]), reverse=True)

        if len(edge_list[0]) > limit:
            # Drop all entries after 'limit'
            edge_list[0] = edge_list[0][limit]

        # Store in dictionary for easy translation to json
        data_dict = dict([('center', subreddit), ('nodes', nodes), ('edges', edge_list)])

    except UnboundLocalError:
        data_dict = dict([('center', subreddit), ('nodes', ""), ('edges', "")])

    # Turn to json.
    data_json = json.dump(data_dict, stdout, ensure_ascii=False)

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
    main(argv[1])

