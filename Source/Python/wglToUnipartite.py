# -*- coding: utf-8 -*-
import pandas as pd
import networkx as nx

__author__ = "Matt Cook"
__version__ = "1.0.0"
__email__ = "mattheworion.cook@gmail.com"

# CONSTANTS
auth_index = 1
sub_index = 0


def main():
    # Create graph
    U = nx.Graph()

    # Read in pandas dataframe
    df = pd.read_csv("weighted_graph_list.csv", sep=',', usecols=[0, 1, 2], header=None, index_col=False)

    # Drop AutoModerator authors
    df = df[df[1] != 'AutoModerator']

    # Sort by authors
    df.sort_values(by=1, ascending=False, inplace=True)

    # Get row, extract nodes and weight
    # Use previous to store previous sub, author
    previous = ['', '']

    i = 1
    # Row: [sub, author]
    for row in df.iterrows():
        try:
            # row[0] is the index, row[1] is a tuple of our data
            row = row[1]
            auth = row[1]
            sub = row[0]

            # For each row, if next author == this author, add edge between this sub and next sub
            if auth == previous[1] and sub != previous[0]:
                # If edge exists, increment weight
                if U.has_edge(previous[0], row[0]):
                    U[previous[0]][row[0]]['weight'] += 1
                # Else, add edge
                else:
                    U.add_edge(previous[0], row[0], weight=1)
            # Set previous to current row
            previous = [sub, auth]
            i += 1
            if i % 10000:
                print("finished ", i, " iterations")
                print("writing")
                # Write to uni_weighted_edge_list.csv, so if it fails, we still have the data up to this.
                nx.write_weighted_edgelist(U, "uni_weighted_edge_list.csv", delimiter=',', encoding='utf-8')
                print("written")
        except IndexError as e:
            print(e)


if __name__ == "__main__":
    main()

