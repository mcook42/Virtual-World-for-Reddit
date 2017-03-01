#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
For initial conversion from csv data to database. Data collected from GoogleBigQuery.
"""

import os
import sys
import pandas
from time import sleep
import numpy as np
import dbInteractions

__author__ = "Matt Cook"
__version__ = "1.0.0"
__email__ = "mattheworion.cook@gmail.com"


def clean_data(table_struct, filename, cur):
    """
    :param table_struct: string specifying how to handle table
    :param filename: string specifying the input filename
    :param cur: cursor to the psql database for querying
    :return: None

    Cleans unecessary data from table and converts empty entries to NULL
        NOTE: If you run this twice, you WILL DUPLICATE THE DUPLICATES OF YOUR FILES.
    """

    # For renaming file
    newname = filename.replace(".csv", "-new.csv")

    # For removal of entries for subreddits we do not have/need.
    valid_names = dbInteractions.get_subreddit_ids(cur)

    if table_struct is "posts":
        try:
            # usecols specifies the index of the columns to use. Header specifies the row of headers.
            # quoting quotes the text fields and prevents errors in copying data
            df = pandas.read_csv(filename, header=0, low_memory=False, quoting=1,
                                 usecols=[0, 1, 2, 3, 5, 6, 9, 10, 14, 18, 19, 20, 25, 27, 28, 32])
        except Exception as e:
            print("error: ", e)

        # Rename columns. created_utc is renamed due to an error in decompression (?)
        df.rename(columns={'name': 'full_name', 'ï»¿created_utc': "created_utc"}, inplace=True)

    elif table_struct is "comments":
        try:
            df = pandas.read_csv(filename, header=0, low_memory=False, quoting=1,
                                 usecols=[0, 4, 7, 8, 9, 10, 11, 13, 14, 15, 16, 18])

        except Exception as e:
            print("error: ", e)

    # Drop entries where the subreddit doesn't exist in our subreddit database.
    # This should never be the case on the server side, but we still need to do it to ignore
    # unnecessary data and prevent issues with the COPY FROM function.
    df = df[df.subreddit_id.isin(valid_names)]

    # Drop entries where no subreddit is specified. We do not need this data.
    df.dropna(subset=["subreddit"], inplace=True)
    df.to_csv(newname, sep=',', na_rep="NULL", quoting=1, encoding='utf-8', index=False)
    print(newname)
    return newname


def main():
    directory = "/media/usb/decompressed_posts"

    if not directory:
        print("Please provide the directory of the file(s) to be read into the db.\n")
        return 1

    # TODO: Validate the inputted directory
    os.chdir(directory)

    # Get the names of the files in the directory specified
    files = os.listdir(directory)

    # Sort in reverse alphabetical order to ensure that posts are inserted before comments into the db
    files.sort(reverse=True)

    for filename in files:
        # Open connection and cursor for the insertion of the table
        conn = dbInteractions.open_conn()
        cur = conn.cursor()

        if "comment" in filename:
            new_filename = clean_data(table_struct="comments", filename=filename, cur=cur)
            table_name = "reddit.comment"

        elif "post" in filename:
            new_filename = clean_data(table_struct="posts", filename=filename, cur=cur)
            table_name = "reddit.post"

        sleep(0.5)
        with open(new_filename, 'r', encoding='utf-8') as infile:
            query = "COPY %s FROM %s CSV HEADER"
            try:
                path = "'" + directory + "/" + new_filename + "'"
                cur.copy_expert(sql=query % (table_name, path), file=infile)
            except Exception as e:
                print("something went wrong with ", new_filename, ": ", e)
                conn.rollback()
                conn.commit()
        cur.close()
        print("Done with: ", new_filename)

    conn.close()
    return 0


if __name__ == '__main__':
    sys.exit(main())
