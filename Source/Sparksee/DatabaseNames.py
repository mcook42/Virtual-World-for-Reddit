import lib.sparksee as sparksee

__author__ = "Caleb Whitman"
__version__ = "1.0.0"
__email__ = "calebrwhitman@gmail.com"

#DatabaseNames.py
#This class holds a bunch of fields for the database name and attributes.
#For now we are only adding the display name.
subredditAttributes = [(u"display_name",sparksee.DataType.STRING,sparksee.AttributeKind.UNIQUE)]

#              ("created",sparksee.DataType.INTEGER,sparksee.AttributeKind.INDEXED),
#              ("description",sparksee.DataType.TEXT,sparksee.AttributeKind.BASIC),
#              ("public_description",sparksee.DataType.TEXT,sparksee.AttributeKind.BASIC),
#              ("language",sparksee.DataType.STRING,sparksee.AttributeKind.INDEXED),
#              ("over18",sparksee.DataType.BOOLEAN,sparksee.AttributeKind.INDEXED),
#              ("public_traffic",sparksee.DataType.BOOLEAN,sparksee.AttributeKind.INDEXED),
#              ("accounts_active",sparksee.DataType.INTEGER,sparksee.AttributeKind.INDEXED),
#              ("subscribers",sparksee.DataType.INTEGER,sparksee.AttributeKind.INDEXED),
#              ("time_updated",sparksee.DataType.INTEGER,sparksee.AttributeKind.INDEXED)]

edgeAttributes = [ (u"weight",sparksee.DataType.INTEGER,sparksee.AttributeKind.INDEXED)]

dbNameGDB = u"Map.gdb"
dbName = u"Map"

subredditType = u"SUBREDDIT"
edgeType = u"Edge"


