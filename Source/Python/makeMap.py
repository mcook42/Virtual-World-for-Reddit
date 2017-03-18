# imports
from memory_profiler import profile
import networkx as nx
import dbInteractions

# GLOBAL VARIABLES
# Create graph
B = nx.Graph()

# Get db connection and cursor
conn = dbInteractions.open_conn()
cur = conn.cursor()

# Query DB for the subreddit names
query = "SELECT subreddit FROM intermediary LIMIT 60;"
cur.execute(query)
sub_names = cur.fetchall()


# @profile(precision=4)
def create_nodes():
    # Add nodes
    B.add_nodes_from(sub_names)  # Add the node attribute "bipartite"
    # print(B.nodes())
    return sub_names


# @profile(precision=4)
def create_edges():
    for subreddit1 in sub_names:
        # Add edges
        cur.execute("""SELECT table1.subreddit, table2.subreddit, table2.author,
                        table1.postnum, table2.postnum, table1.commentnum, table2.commentnum
                        FROM intermediary AS table1, intermediary AS table2
                        WHERE table1.author=table2.author AND
                         table1.subreddit != table2.subreddit AND table1.subreddit=%s
                        """, (subreddit1,))
        # res: tuple of (sub1, sub2, common_author, post_num1, post_num2, comm_num1, comm_num2)

        # Initialize storage of first 10,000 sub2 names and the common authors etc
        common = cur.fetchmany(60)
        # print (common)
        # print(common)
        # While there is something to fetch
        for row in common:
            # Check if the subreddit has any common authors
            if row[1] is not []:
                print("starting: ", row[0])
                print("with: ", row[1])
                # Get name of first linked subreddit
                sub2 = row[1]

                # Add to the weight of the edge
                # TODO: Fix the arbitrary weighting of edges
                # .5 * (post_num1 + post_num2) + (.5 * (comm_num1 + comm_num2))
                try:
                    weight = (.5 * (row[3] + row[4])) + (.5 * (row[5] + row[6]))
                except Exception as e:
                    print(row[:-1])
                # print("The edge weight is: ", weight)
                # Add weighted edge between sub1 and sub2
                # print(subreddit1, "and 2: ", sub2)
                # Hack for checking if there is already a weighted edge
                # B[subreddit1][sub2] refrences the node and its data at that index (fastest way)
                try:
                    # If cur_weight exists
                    cur_weight = weight + B[subreddit1][sub2]
                except KeyError:
                    # Else
                    cur_weight = weight
                # Add/update edge with new weight
                B.add_edge(common[0], common[1], weight=(cur_weight + weight))

            # Get next 10,000 rows
            common = cur.fetchmany(10000)

# Queries
# TODO: Review this because we changed to intermediary
# Post Res: list of (sub1_id, sub2_id, author, num_p1, num_p2)
# Comment Res: list of (sub1_id, sub2_id, author, num_c1, num_c2)

# TODO: Filter

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

# Get list of subredditors in common

# Find post weights

# Find comment weights

# TODO: Combine the results

# TODO: Submit results to new database table

if __name__ == '__main__':
    sub_names = create_nodes()
    print(sub_names)
    print("creating edges")
    create_edges()
    # Close cursor and connection to db
    cur.close()
    conn.close()
    print("writing")
    # Write data to CSV file
    nx.write_weighted_edgelist(B, "~/test/weighted_graph_list.csv",
                               delimiter=',', encoding='utf-8')
    print("written")

