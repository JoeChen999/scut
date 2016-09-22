from db_manager import DBManager
import MySQLdb

Database = 'game-db'
Table = "localNotifications"

class manage_local_notifications:
    def __init__(self):
        db = DBManager()
        self.conn = db.get_db(Database)
        self.cursor = self.conn.cursor()

    def get_notifications(self):
        retlist = []
        self.cursor.execute("select * from " + Table)
        for ln in self.cursor.fetchall():
            retlist.append((ln[0],ln[1], ln[2], ln[3]))
        return retlist

    def get_notification_info(self, key):
        self.cursor.execute("select * from %s where Name='%s'"%(Table, key))
        ln = self.cursor.fetchone()
        return ln[0], ln[1], ln[2]

    def add_notification(self, name, time, message):
        content = MySQLdb.escape_string(message)
        rowCount = self.cursor.execute("insert into %s(`Name`,`Time`,`Message`) values ('%s','%s','%s')" % (Table, name, time, content))
        if rowCount:
            self.conn.commit()
            return True
        return False

    def edit_notification(self, name, time, message):
        rowCount = self.cursor.execute("update %s set Time='%s',Message='%s' where Name='%s'"%(Table, time, message, name))
        if rowCount:
            self.conn.commit()
            return True
        return False

    def active_notification(self, key):
        if self.cursor.execute("update %s set Status=1 where Name='%s'" % (Table, key)) == 0:
            return False
        self.conn.commit()
        return True

    def inactive_notification(self, key):
        if self.cursor.execute("update %s set Status=0 where Name='%s'" % (Table, key)) == 0:
            return False
        self.conn.commit()
        return True