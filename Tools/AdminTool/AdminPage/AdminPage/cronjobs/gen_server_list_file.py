__author__ = 'ChenBiao'

import MySQLdb
import json

User = "root"
PWD = "antinc2014"
Host = 'localhost'
Database = 'master'
Table = "serverlist"

def gen_server_list_json():
    conn = MySQLdb.Connect(host=Host, user=User)#, passwd=PWD)
    conn.select_db(Database)
    cursor = conn.cursor()
    cursor.execute("set names utf8")
    cursor.execute("select * from " + Table)
    serverlist = []
    for ln in cursor.fetchall():
        serverlist.append((ln[0], ln[1], ln[2], ln[3], ln[4], ln[5], ln[6]))
    cursor.close()
    cursor.close()
    return json.dumps(serverlist)

if __name__== "__main__":
    f = open("../static/serverlist.txt", "w")
    f.write(gen_server_list_json())
    f.close()
