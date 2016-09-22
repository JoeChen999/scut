__author__ = 'ChenBiao'

from db_manager import DBManager
import MySQLdb
import hashlib, time, random

Database = "master"
Table = "Users"


def player_register(account, password):
    db = DBManager().get_db_cursor(Database)
    account_name = MySQLdb.escape_string(account)
    m = hashlib.md5()
    m.update(password)
    pwd = m.hexdigest()
    src = str(time.time()) + str(random.randint(0, 10000))
    m.update(src)
    token = m.hexdigest()
    rowcount = db.execute("insert into %s(`Account`,`Password`,`Token`) values ('%s', '%s', '%s')" % (Table, account_name, pwd, token))
    if rowcount:
        return True
    return False


def player_login(account, password):
    db = DBManager().get_db_cursor(Database)
    account_name = MySQLdb.escape_string(account)
    m = hashlib.md5()
    m.update(password)
    pwd = m.hexdigest()
    db.execute("select * from %s where Account='%s'" % (Table, account_name))
    player = db.fetchone()
    if player and player[1] == pwd:
        src = str(time.time()) + str(random.randint(0, 10000))
        m.update(src)
        token = m.hexdigest()
        db.execute("update %s set Token = %s where Account='%s'" % (Table, token, account_name))
        return player[2], token
    return 0, ""


def player_check(userId, token):
    db = DBManager().get_db_cursor(Database)
    db.execute("select * from %s where UserId=%s" % (Table, userId))
    player = db.fetchone()
    if player and player[3] == token:
        return True
    return False


def get_player_info(user_name):
    db = DBManager().get_db_cursor(Database)
    db.execute("select * from %s where Name=%s" % (Table, user_name))
    player = db.fetchone()
    return player

if __name__ == "__main__":
    userId, token = player_login("test", "123")
    print userId
    print token
