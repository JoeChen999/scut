__author__ = 'ChenBiao'

import MySQLdb

User = "root"
PWD = "antinc2014"
Host = 'localhost'
Database = 'game'
Table = "mails"

class manage_mails:
    def __init__(self):
        self.conn = MySQLdb.Connect(host=Host, user=User)#, passwd=PWD)
        self.conn.select_db(Database)
        self.cursor = self.conn.cursor()

    def __del__(self):
        self.cursor.close()
        self.conn.close()

    def get_mails(self, page):
        retlist = []
        self.cursor.execute("select * from %s order by Id desc limit %d,%d" % (Table, (int(page) - 1) * 200, 200))
        for m in self.cursor.fetchall():
            retlist.append((m[0], m[1], m[2], m[3], m[4], m[5], m[6]))
        return retlist

    def delete_mail(self, mid):
        sql = "delete from %s where Id=%s" % (Table, mid)
        print sql
        count = self.cursor.execute(sql)
        if count:
            self.conn.commit()
            return True
        else:
            return False

    def add_mail(self, message, attachType, attachCount, startTime, expireTime, toUser):
        count = self.cursor.execute("insert into %s(Message, AttachType, AttachCount, BeginTime, ExpireTime, ToUser) values ('%s', %s, %s, '%s', '%s', %s)"%(Table, message, attachType, attachCount, startTime, expireTime, toUser))
        if count:
            self.conn.commit()
            return True
        else:
            return False
