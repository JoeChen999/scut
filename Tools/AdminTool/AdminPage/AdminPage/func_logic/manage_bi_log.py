__author__ = 'ChenBiao'
import MySQLdb
import datetime

User = "root"
PWD = "antinc2014"
Host = 'localhost'
Database = 'bilog'


def get_bi_log_type():
    tables = []
    m_conn = MySQLdb.Connect(host=Host, user=User, passwd=PWD)
    m_conn.select_db(Database)
    m_cursor = m_conn.cursor()
    m_cursor.execute("show tables")
    for table in m_cursor.fetchall():
        tables.append(table[0])
    m_cursor.close()
    m_conn.close()
    return tables


def get_bi_logs(type, sql_str):
    ret_table = []
    m_conn = MySQLdb.Connect(host=Host, user=User, passwd=PWD)
    m_conn.select_db(Database)
    m_cursor = m_conn.cursor()
    sql = sql_str.strip()
    if sql == "":
        m_cursor.execute("desc " + type)
        ret_table.append([])
        for column in m_cursor.fetchall():
            ret_table[0].append(column[0])
        sql = "select * from %s limit 1000" % type
    m_cursor.execute(sql)
    for log in m_cursor.fetchall():
        column_list = []
        for column in log:
            if isinstance(column, datetime.datetime):
                column_list.append(column.strftime('%Y-%m-%d %H:%M:%S'))
            else:
                column_list.append(column)
        ret_table.append(column_list)
    m_cursor.close()
    m_conn.close()
    return ret_table


def write_to_temp_file(logs):
    f = open("AdminPage/static/tempbilogfile", "w")
    for log in logs:
        for column in log:
            f.write(str(column))
            f.write("\t")
        f.write("\n")
    f.close()


if __name__ == "__main__":
    tables = get_bi_logs("payment", "")
    print tables