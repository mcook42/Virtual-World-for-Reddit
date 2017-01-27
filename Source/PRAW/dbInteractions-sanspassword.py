#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
For interactions with the database on the server side.
"""

import psycopg2
import time

__author__ = "Matt Cook"
__version__ = "1.0.0"
__email__ = "mattheworion.cook@gmail.com"


def insert_subreddit(in_tuple):
    """Insert tuple of subreddit info into redditserver reddit.subreddit table"""
    try:
        # Connect to an existing database
        conn = psycopg2.connect("dbname=redditserver user=pg13 password=I'mNotDumbEnoughToPostThePassword_lol")
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
       subscribers int NOT NULL,
       time_updated bigint NOT NULL,
       PRIMARY KEY(full_name)
       );
    """

    # Pass data to fill a query placeholders and let Psycopg perform
    # the correct conversion (no more SQL injections!)
    # Note: Uses the tuple passed in to insert values
    try:
        cur.execute("""INSERT INTO reddit.subreddit (full_name, created, description, display_name, public_description,
                        language, over18, public_traffic, accounts_active, subscribers, time_updated)
                        VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, %s, %s)""", in_tuple)
        conn.commit()

    except psycopg2.IntegrityError:
        conn.rollback()
        # If the data is already in the table, update the value
        one_day_ago = time.time() - 86400
        update_tuple = (in_tuple[8], in_tuple[9], in_tuple[10], in_tuple[0], one_day_ago)

        cur.execute(
            """UPDATE reddit.subreddit SET accounts_active=%s, subscribers=%s, time_updated=%s WHERE full_name=%s AND
            time_updated < %s;""", update_tuple)
        conn.commit()

    # Close communication with the database
    cur.close()
    conn.close()

