import db_manager, commands
from ConfigUtils import ConfigUtils
from pb2py import CLServerCommand_pb2, LCServerCommand_pb2
import RequestSender

db = db_manager.DBManager()
conf = ConfigUtils()
Database = conf.get('game_db')
Table = "configs"


def get_configs(serverid):
    m_conn = db.get_db(Database)
    m_conn.select_db(Database)
    m_cursor = m_conn.cursor()
    retlist = []
    m_cursor.execute("select * from " + Table + " order by Name")
    for config in m_cursor.fetchall():
        retlist.append((config[0], config[1]))
    m_cursor.close()
    m_conn.close()
    return retlist


def edit_configs(request):
    m_conn = db.get_db(Database)
    m_cursor = m_conn.cursor()
    keys = request.getlist('keys[]')
    values = request.getlist('values[]')
    for field, value in request.items():
        if field == 'keys[]':
            if "values[]" not in request:
                return False
            if not len(keys) == len(values):
                return False
            for i in range(len(keys)):
                m_cursor.execute("insert into %s (`Name`,`Value`) values ('%s','%s')" % (Table, keys[i], values[i]))
        elif field == 'values[]':
            continue
        else:
            m_cursor.execute("update %s set Value='%s' where Name='%s'" % (Table, value, field))
    m_conn.commit()
    m_cursor.close()
    m_conn.close()
    req = CLServerCommand_pb2.CLServerCommand()
    req.Type = 3
    res_stream = RequestSender.send(101, req)
    response = LCServerCommand_pb2.LCServerCommand()
    response.ParseFromString(res_stream)
    return response.Success

if __name__ == "__main__":
    print get_configs()
