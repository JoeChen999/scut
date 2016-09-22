import MySQLdb
from singleton import singleton
from ConfigUtils import ConfigUtils


@singleton
class DBManager:
    def __init__(self):
        self.configs = ConfigUtils()
        self.__conn = MySQLdb.Connect(host=self.configs.get("mysql_host"), user=self.configs.get("mysql_user"),
                                      passwd=self.configs.get("mysql_password"))

    def get_db(self, db):
        try:
            self.__conn.select_db(db)
        except:
            self.__conn = MySQLdb.Connect(host=self.configs.get("mysql_host"), user=self.configs.get("mysql_user"),
                                          passwd=self.configs.get("mysql_password"))
            self.__conn.select_db(db)
        finally:
            return self.__conn
