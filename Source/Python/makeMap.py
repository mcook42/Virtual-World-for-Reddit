 # -*- coding: utf-8 -*-
import networkx as nx
import dbInteractions
import time
import timeit

__author__ = "Matt Cook"
__version__ = "1.0.0"
__email__ = "mattheworion.cook@gmail.com"

# CONSTANT
# LIMIT = 10


def create_nodes():
    # Add nodes
    print("creating nodes")
    B.add_nodes_from(sub_names)  # Add the nodes


def create_edges():
    print("fetching authors")
    cur.execute("SELECT DISTINCT author FROM intermediary;")
    authors = cur.fetchall()
    print("creating edges")
    for author in authors:
        # Extract name from (name,)
        author = author[0]

        # start = time.time()
        # Query database for the subreddits an author is connected to
        cur2.execute("SELECT subreddit FROM intermediary WHERE author = %s;", (author,))
        # print("query time: ", time.time() - start)

        # Get all the subs the author is an active member of (active defined in documentation)
        subs = cur2.fetchall()

        # Add all edges
        # Hack for checking if there is already a weighted edge
        # B[author][sub2] refrences the node and its data at that index (fastest way)
        for sub in subs:
            # Need to extract names from (name,)
            sub = sub[0]
            try:
                B[author][sub]['weight'] += 1
            except:
                B.add_edge(u=author, v=sub, weight=1)


# WE MAY NEED THIS LATER, BUT FOR NOW IT'S NOT IN USE
#
# def create_edges():
#     print("creating edges")
#     for subreddit1 in sub_names:
#         # start = time.time()
#         # Query database for the common authors and their scores
#         # cur2.execute("""SELECT table1.subreddit, table2.subreddit, table2.author,
#         #                 table1.postnum, table2.postnum,(table1.postnum+table2.postnum) AS sumOrder
#         #                 FROM intermediary AS table1, intermediary AS table2
#         #                 WHERE table1.author=table2.author AND
#         #                  table1.subreddit != table2.subreddit AND table1.subreddit=%s
#         #                  ORDER BY sumOrder DESC LIMIT 25
#         #                 """, (subreddit1,))
#         # res: tuple of (sub1, sub2, common_author, post_num1, post_num2, comm_num1, comm_num2)
#
#
#         print("time to complete query: ", time.time() - start)
#         # Initialize storage of first sub2 names and the common authors etc
#         common = cur2.fetchall()
#
#         for row in common:
#             # Check if the subreddit has any common authors
#             # Get name of first linked subreddit
#             sub2 = row[1]
#             # Add to the weight of the edge
#             # TODO: Fix the arbitrary weighting of edges
#             # .5 * (post_num1 + post_num2) + (.5 * (comm_num1 + comm_num2))
#             try:
#                 weight = (.5 * (row[3] + row[4])) + (.5 * (row[5] + row[6]))
#                 # Round to thousandths
#                 weight = round(weight, 2)
#             except Exception as e:
#                 print(e, " ", row[:-1])
#             # Add weighted edge between sub1 and sub2
#             # Hack for checking if there is already a weighted edge
#             # B[subreddit1][sub2] refrences the node and its data at that index (fastest way)
#             try:
#                 # If cur_weight exists
#                 cur_weight = weight + B[subreddit1][sub2]
#             except KeyError:
#                 # Else
#                 cur_weight = weight
#             # Add/update edge with new weight
#             B.add_edge(row[0], row[1], weight=(cur_weight + weight))
#         common = cur2.fetchmany(LIMIT)
#
#             # Get next 10,000 rows, then check at top if we ran out (will return [] if no more rows)
#             # common = cur2.fetchmany(LIMIT)

# TODO: Implement the edge filtering

"""
This algorithm begins by replacing symmetric valued edges (Sij) with asymmetric weighted edges (Aij and Aji),
where Aij = Sij/i’s degree and Aji = Sij/j’s degree. It then preserved edges whose weight is statistically incompatible,
at a given level of significance α, with a null model in which edge weights are distributed uniformly at random.
In our resulting backbone network, two subreddits are linked if the number of users who post in both of them is
statistically significantly larger than expected in a null model, from the perspective of both subreddits.
To recombine the directed edges between each two nodes, we replaced the two directed edges with a single undirected
edge whose weight is the average of the two directed edges.
"""


"""
We defined a bipartite network X, where Xij = 1 if user i is an active poster in subreddit j and otherwise is 0.
We then projected this as a weighted unipartite network Y as XX′, where Yij is the number of users that post
in both subreddits i and j.
"""

# TODO: We want to change this weighting to account for the "importance" of the subredditors

# TODO: Submit results to new database table


def main():
    # GLOBAL VARIABLES
    global B, cur, cur2, sub_names

    # Create the graph
    B = nx.Graph()

    # Get db connection and cursors
    conn = dbInteractions.open_conn()
    cur = conn.cursor()
    cur2 = conn.cursor()

    print("fetching names")
    cur.execute("SELECT subreddit FROM intermediary;")  # LIMIT %s;", (LIMIT,))
    sub_names = cur.fetchall()

    # Create the nodes and edges
    create_nodes()
    auth_create_edges()

    # Close cursors and connection to db
    print("cleaning up db connections")
    cur.close()
    cur2.close()
    conn.close()

    print("writing")
    # Write data to CSV file
    nx.write_weighted_edgelist(B, "weighted_graph_list.csv",
                               delimiter=',', encoding='utf-8')
    print("written")

if __name__ == '__main__':
    main()


