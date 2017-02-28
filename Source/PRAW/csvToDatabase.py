#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
For initial conversion from csv data to database. Data collected from GoogleBigQuery.
"""

import os
import sys
import pandas
import numpy as np
import dbInteractions

__author__ = "Matt Cook"
__version__ = "0.0.1"
__email__ = "mattheworion.cook@gmail.com"


def clean_data(table_struct, filename, cur):
    """Cleans unecessary data from table and converts empty entries to NULL"""
    # NOTE: If you run this twice, you WILL DUPLICATE THE DUPLICATES OF YOUR FILES.
    # BigQuery has no null representation, so go through and add NULL
    dtypes = {"created_utc": np.int64, "subreddit": np.str_, "subreddit_id": np.str_, "author": np.str}
    namez = ["ï»¿created_utc", "subreddit", "subreddit_id", "author", "domain", "num_comments",
                                 "score", "title", "selftext", "full_name", "gilded", "over_18", "thumbnail", "is_self",
                                 "from_id", "permalink", "distinguished"]

    newname = filename.replace(".csv", "-new.csv")
    if table_struct is "posts":
        try:
            df = pandas.read_csv(filename, header=0, low_memory=False, quoting=1,
                                 usecols=[0, 1, 2, 3, 5, 6, 9, 10, 14, 18, 19, 20, 25, 27, 28, 32])
        except Exception as e:
            print("error: ", e)
        df.rename(columns={'name': 'full_name', 'ï»¿created_utc': "created_utc"}, inplace=True)

        # Drop entries where the subreddit doesn't exist in our subreddit database.
        # This should never be the case on the server side, but we still need to do it to ignore
        # unnecessary data and prevent issues with the COPY FROM function.
        valid_names = dbInteractions.get_subreddit_ids(cur)
        df = df[df.subreddit_id.isin(valid_names)]

        # Drop entries where no subreddit is specified. We do not need this data.
        df.dropna(subset=["subreddit"], inplace=True)

        # Write to file
        df.to_csv(newname, sep=',',
                  # columns=(("created_utc", "subreddit", "subreddit_id", "author", "domain", "num_comments",
                  #           "score", "title", "selftext", "full_name", "gilded", "over_18", "thumbnail", "is_self",
                  #           "permalink", "distinguished")),
                  na_rep="NULL", quoting=1,
                  encoding='utf-8', index=False)
    if table_struct is "comments":
        df.to_csv(newname, sep=",", na_rep="NULL",
                  columns=("body", "score_hiddden", "name", "author", "created_utc", "subreddit_id",
                           "link_id", "parent_id", "controversiality", "gilded", "id", "subreddit",
                           "distinguished"), dtypes=dtypes, index=False)
    return newname


def main():
    directory = "C:\\Users\\Someone\\Desktop\\testDir" #sys.argv[1]

    if not directory:
        print("Please provide the directory of the file(s) to be read into the db.\n")
        return 1

    # TODO: Validate the inputted directory
    os.chdir(directory)

    # Get the names of the files in the directory specified
    files = os.listdir(directory)

    for filename in files:
        # Open connection and cursor for the insertion of the table
        conn = dbInteractions.open_conn()
        cur = conn.cursor()
        if "comments" in filename:
             new_filename = clean_data(table_struct="comments", filename=filename, cur=cur)
        elif "post" in filename:
            new_filename = clean_data(table_struct="posts", filename=filename, cur=cur)

        with open(new_filename, 'r', encoding='utf-8') as infile:
            # Check which table it's supposed to go into
            if "post" in filename:
                query = "COPY reddit.post FROM %s CSV HEADER"
                try:
                    path = "'" + directory + "\\" + new_filename + "'"
                    cur.copy_expert(sql=query % path, file=infile)
                except Exception as e:
                    print("something went wrong: ", e)
                    conn.rollback()

            elif "comment" in filename:
                try:
                    cur.copy_from(infile, "reddit.comment", null='', sep=',',
                                  columns=("body", "score_hiddden", "author", "created_utc", "subreddit_id",
                                           "link_id", "parent_id", "controversiality", "gilded", "id", "subreddit",
                                           "distinguished")
                                  )
                except Exception as e:
                    print("Something went wrong: ", e)
                    conn.rollback()

            conn.commit()
            cur.close()
            conn.close()
    return 0


if __name__ == '__main__':
    sys.exit(main())
