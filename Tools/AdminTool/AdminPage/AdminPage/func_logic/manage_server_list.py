import MySQLdb
import json
from db_manager import DBManager
from ConfigUtils import ConfigUtils

Table = "ServerList"


def enum(**enums):
    return type('Enum', (), enums)

ServerProperties = enum(Id=0, Name=1, Description=2, Load=3, Status=4, Address=5, Port=6)


class ManageServerList:
    def __init__(self):
        self.configs = ConfigUtils()
        db = DBManager()
        self.conn = db.get_db(self.configs.get("master_db"))
        self.cursor = self.conn.cursor()

    def get_server_list(self):
        retlist = []
        self.cursor.execute("set names utf8")
        self.cursor.execute("select * from " + Table)
        for ln in self.cursor.fetchall():
            retlist.append((ln[0], ln[1], ln[2], ln[3], ln[4], ln[5], ln[6], ln[7], ln[8], ln[9], ln[10], ln[11]))
        return retlist

    def get_server_info(self, Id):
        self.cursor.execute("set names utf8")
        self.cursor.execute("select * from %s where Id='%s'" % (Table, Id))
        ln = self.cursor.fetchone()
        return ln[0], ln[1], ln[2], ln[3], ln[4], ln[5], ln[6], ln[7], ln[8], ln[9], ln[10], ln[11]

    def edit_server_list(self, id, name, description, status, address, port, tabid, recommended, dictionaryurl, datatableurl, metafileurl):
        self.conn.set_character_set('utf8')
        self.cursor.execute("set names utf8")
        self.cursor.execute('SET CHARACTER SET utf8;')
        self.cursor.execute('SET character_set_connection=utf8;')
        rowCount = self.cursor.execute("update %s set Name='%s',Description='%s',Status='%s',Address='%s',Port='%s',TabId=%s,Recommended=%s,DictionaryDownloadUrl='%s',DataTableDownloadUrl='%s',DownloadMetaDataUrl='%s' where Id=%s"%(Table, name, description, status, address, port, tabid, recommended, dictionaryurl, datatableurl, metafileurl, id))
        if rowCount:
            self.conn.commit()
            serverlistStr = self.get_server_list_in_json()
            f = open(self.configs.get("root_path") + "data/serverlist.txt", "w")
            f.write(serverlistStr)
            return True
        return False

    def add_server_list(self, id, name, description, status, address, port, tabid, recommended, dictionaryurl, datatableurl, metafileurl):
        name = MySQLdb.escape_string(name)
        description = MySQLdb.escape_string(description)
        address = MySQLdb.escape_string(address)
        self.conn.set_character_set('utf8')
        self.cursor.execute("set names utf8")
        self.cursor.execute('SET CHARACTER SET utf8;')
        self.cursor.execute('SET character_set_connection=utf8;')
        rowCount = self.cursor.execute("insert into %s(`Id`,`Name`,`Description`,`Load`,`Status`,`Address`,`Port`,`TabId`,`Recommended`,`DictionaryDownloadUrl`,`DataTableDownloadUrl`,`DownloadMetaDataUrl`) values (%s,'%s','%s',0,%s,'%s','%s',%s,%s,'%s','%s','%s')" % (Table, id, name, description, status, address, port, tabid, recommended, dictionaryurl, datatableurl,metafileurl))
        if rowCount:
            self.conn.commit()
            serverlistStr = self.get_server_list_in_json()
            f = open(self.configs.get("root_path") + "data/serverlist.txt", "w")
            f.write(serverlistStr)
            return True
        return False

    def update_server_load(self, request):
        if request.META.has_key('HTTP_X_FORWARDED_FOR'):
            ip = request.META['HTTP_X_FORWARDED_FOR']
        else:
            ip = request.META['REMOTE_ADDR']
        serverId = request.GET['sid']
        self.cursor.execute("set names utf8")
        self.cursor.execute("select * from %s where Id='%s'" % (Table, serverId))
        server = self.cursor.fetchone()
        if not server[ServerProperties.Address] == ip:
            return "failed"
        rowCount = self.cursor.execute("update %s set `Load`=%s where Id=%s" % (Table, request.GET['load'], request.GET['sid']))
        if rowCount:
            self.conn.commit()
        return "success"

    def get_server_list_in_json(self):
        serverlist = self.get_server_list()
        retlist = []
        keylist = ["Id", "Name", "Description", "Status", "Load", "HostOrIPString", "Port", "TabId", "Recommended", "DictionaryDownloadUrl","DataTableDownloadUrl", "DownloadMetaDataUrl"]
        for server in serverlist:
            retlist.append(dict(zip(keylist, server)))
        retdict = {"Data": retlist}
        return json.dumps(retdict)

