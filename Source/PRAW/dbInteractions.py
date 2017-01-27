#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
For interactions with the database on the server side.
"""

import psycopg2

__author__ = "Matt Cook"
__version__ = "0.0.1"
__email__ = "mattheworion.cook@gmail.com"


def insert_subreddit(in_tuple):
    """Insert dictionary of subreddit info into redditserver reddit.subreddit table"""
    try:
        # Connect to an existing database
        conn = psycopg2.connect("dbname=redditserver user=postgres password=m5270685")
    except psycopg2.Error:
        print("Cannot connect to server.")

    # Open a cursor to perform database operations
    cur = conn.cursor()

    """
    Structure of the table:

    TABLE reddit.subreddit(
       full_name text,
       created_utc bigint NOT NULL,
       description text NOT NULL,
       display_name text NOT NULL,
       public_description text NOT NULL,
       language text NOT NULL,
       over18 bool NOT NULL,
       public_traffic bool,
       accounts_active int NOT NULL,
       PRIMARY KEY(full_name)
       );
    """

    # Pass data to fill a query placeholders and let Psycopg perform
    # the correct conversion (no more SQL injections!)
    # Note: Uses the tuple passed in to insert values
    # full_name | created_utc | description | display_name | public_description | language | over18 | public_traffic | accounts_active
    cur.executemany("""INSERT INTO reddit.subreddit (full_name, created_utc, description, display_name, public_description,
                    language, over18, public_traffic, accounts_active)
                    VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s)""", in_tuple)

    # Make the changes to the database persistent
    conn.commit()
    fetch = conn.fetchone()
    print(fetch)

    # Close communication with the database
    cur.close()
    conn.close()
