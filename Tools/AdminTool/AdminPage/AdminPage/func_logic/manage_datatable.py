import hashlib
import gzip
import json
import os
import MySQLdb
from db_manager import DBManager
from ConfigUtils import ConfigUtils


dt_path = "/var/game/LobbyServer/DataTables/"
#dt_path = "E:\code\server\gameserver\LobbyServer\DataTables\\"

User = "root"
PWD = "antinc2014"
Host = 'localhost'
Database = 'game'
Table = "dataTableCRC32Codes"

def do_update(file):
    name = file.name
    content = file.read()
    if name == "BadWords.txt":
        load_bad_words(content)
    f = open(dt_path + name, "w")
    f.write(content)
    f.close()
    return do_reload(name)

def load_bad_words(content):
    sql = "insert into SensitiveWord values"
    index = 1
    for line in content.split('\n'):
        line = line.strip()
        if line == "":
            continue
        sql += "(%s, '%s')," % (index, line)
        index += 1
    sql = sql[:-1]
    db = DBManager().get_db_cursor("master")
    db.execute("truncate SensitiveWord")
    db.execute("set names utf8")
    db.execute(sql)
    DBManager().get_db("master").commit()

def do_reload(filename):
    dtname = filename.replace(".txt", "")
    m_conn = MySQLdb.Connect(host=Host, user=User, passwd=PWD)
    m_conn.select_db(Database)
    m_cursor = m_conn.cursor()
    m_cursor.execute("INSERT INTO %s(`Name`) VALUES ('%s') ON DUPLICATE KEY UPDATE LatestVersion = LatestVersion + 1"%(Table, dtname))
    m_conn.commit()
    m_cursor.close()
    m_conn.close()
    return "Upload Success"


def refresh_datatable(serverid):
    filedict = {"Data": []}
    config = ConfigUtils()
    for root, dirs, files in os.walk(config.get("root_path") + "/Download/" + serverid):
        for file in files:
            try:
                if file.index(".gz") > 0:
                    continue
                if file.index("hashCode") > 0:
                    continue
            except:
                pass
            filename = root + "/" + file
            f = open(filename, "r")
            content = f.read()
            g =gzip.GzipFile(filename="", mode="wb", compresslevel=9, fileobj=open(filename+".gz", "wb"), mtime=0)
            g.write(content)
            g.close()
            zfile = open(filename+".gz", "rb")
            m = hashlib.md5()
            m.update(zfile.read())
            md5 = m.hexdigest()
            zfile.close()
            index = -1
            try:
                index = filename.index("DataTable")
                filetype = "DataTable"
            except ValueError:
                try:
                    index = filename.index("Dictionary")
                    filetype = "Dictionary"
                except ValueError:
                    continue
            finally:
                name = filename[index:].replace("\\", "/")
            if index < 0:
                continue
            filedict["Data"].append({"Name": name, "Hash": md5, "Type": filetype})
            f.close()
    f = open("".join((config.get("root_path"), "Download/", serverid, "/hashCode.txt")), "w")
    s = json.dumps(filedict)
    f.write(s)
    print s
    return s


def get_datatables():
    datatables = []
    for root ,dirs, files in os.walk(dt_path):
        for file in files:
            datatables.append(file)
    return to_table(datatables, 5, True)


def view_datatable(name):
    f = open(dt_path + name)
    rowcount = 0
    i = 0
    datalist = []
    for line in f.readlines():
        line = line.decode('gbk')
        lineData = line.split('\t')
        if i == 0:
            rowcount = len(lineData)
        for data in lineData:
            datalist.append(data)
    return to_table(datalist, rowcount)


def to_table(itemlist, rowcount, islink=False):
    i = 0
    tableStr = '<table border="2" style="color:#7a8f9f"><tr>'
    for item in itemlist:
        if i == rowcount:
            tableStr += '</tr><tr>'
            i = 0
        if islink:
            tableStr += '<td><a href="?name=%s">%s</a></td>'%(item, item)
        else:
            tableStr += '<td>%s</td>'%(item)
        i += 1
    tableStr += '</tr></table>'
    return tableStr


if __name__ == "__main__":
    refresh_datatable()