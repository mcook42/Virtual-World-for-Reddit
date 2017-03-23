# -*- coding: utf-8 -*-
import pandas as pd
import dbInteractions
from sys import argv
# from memory_profiler import profile
from timeit import timeit

__author__ = "Matt Cook"
__version__ = "1.0.0"
__email__ = "mattheworion.cook@gmail.com"

# CONSTANTS
# LIMIT = 100000
headers = ['subreddit', 'author', 'weight']


#@profile()
def main():
    # Get db connection and cursors
    conn = dbInteractions.open_conn()
    cur = conn.cursor()
    cur2 = conn.cursor()

    # The filename
    fname = "weighted_graph_list.csv"

    # Create the matrix
    B = pd.DataFrame(columns=headers)

    print("fetching authors")
    cur.execute("SELECT (author, subreddit) FROM intermediary;")
    authors = cur.fetchall()

    i = 1
    print("adding rows")
    for row in authors:
        # Extract name from (name,)
        row = row[0]

        # row will be a string '(sub, author)'
        # split on ','
        split = row.split(sep=',', maxsplit=1)

        # remove leading '(' from 0
        author = split[0]
        author = author.lstrip('(')
        author = author.strip("'")

        # remove trailing ')' from 1
        sub = split[1]
        sub = sub.rstrip(')')

        # Add edges with weight 1
        temp = pd.DataFrame([[sub, author, 1]], columns=headers)
        B = B.append(temp, ignore_index=True)

        # Provide status updates
        if (i % 10000) == 0:
            print("finished ", i, " iterations")
            print("writing")
            # Append data to CSV file without indexes or headers
            B.to_csv(fname, sep=',', mode='a', encoding='utf-8', header=False, index=False)
            print("written")
            # Reduce memory load by resetting B
            B = pd.DataFrame(columns=headers)

        i += 1



    # Close cursors and connection to db
    print("cleaning up db connections")
    cur.close()
    cur2.close()
    conn.close()

if __name__ == '__main__':
    print(timeit("main()", setup="from __main__ import main", number=1))
