# -*- coding: utf-8 -*-
import pandas as pd
import dbInteractions
from sys import argv
from memory_profiler import profile
from timeit import timeit

__author__ = "Matt Cook"
__version__ = "1.0.0"
__email__ = "mattheworion.cook@gmail.com"

# CONSTANTS
LIMIT = 10000
headers = ['subreddit', 'author', 'weight']


@profile()
def main():
    # Get db connection and cursors
    conn = dbInteractions.open_conn()
    cur = conn.cursor()
    cur2 = conn.cursor()

    # Create the matrix
    B = pd.DataFrame(columns=headers)

    print("fetching authors")
    cur.execute("SELECT DISTINCT author FROM intermediary LIMIT %s;", (LIMIT,))
    authors = cur.fetchall()

    print("adding rows")
    for author in authors:
        # Extract name from (name,)
        author = author[0]

        # Query database for the subreddits an author is connected to
        cur2.execute("SELECT subreddit FROM intermediary WHERE author = %s;", (author,))

        # Get all the subs the author is an active member of (TODO: active defined in documentation)
        subs = cur2.fetchall()

        # Add all edges
        for sub in subs:
            # Need to extract names from (name,)
            sub = sub[0]
            # Append a row
            temp = pd.DataFrame([[sub, author, 1]], columns=headers)
            B = B.append(temp, ignore_index=True)

    print("writing")
    # Write data to CSV file without indexes
    fname = "weighted_graph_list.csv"
    B.to_csv(fname, delimiter=',', encoding='utf-8', index=False)
    print("written")

    # Close cursors and connection to db
    print("cleaning up db connections")
    cur.close()
    cur2.close()
    conn.close()

if __name__ == '__main__':
    print(timeit("main()", setup="from __main__ import main", number=1))
