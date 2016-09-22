__author__ = 'ChenBiao'
import os
import MySQLdb

User = "root"
PWD = "antinc2014"
Host = 'localhost'
Database = 'game'
Table = "configs"
ErrorLogPath = "/var/game/LobbyServer/Log/Exception/"
#ErrorLogPath = "E:\\code\\server\\gameserver\\LobbyServer\\Log\\Exception\\"

def show_client_logs(postData):
    typecode = postData["type"]
    condition = postData["condition"]
    sql = "SELECT * FROM clientPerformanceLog where type = " + typecode
    if condition > 0:
        sql += " And " + condition
    m_conn = MySQLdb.Connect(host=Host, user=User, passwd=PWD)
    m_conn.select_db(Database)
    m_cursor = m_conn.cursor()
    logs = m_cursor.execute(sql)
    m_cursor.close()
    m_conn.close()


def show_server_logs(date):
    logTable = "<table border='2' style='color:#7a8f9f'><tr><th>Time</th><th>Stack Trace</th></tr>"
    for i in range(18, -1, -1):
        fileName = ErrorLogPath + date
        if i < 10:
            fileName += "0" + str(i) + ".txt"
        else:
            fileName += str(i) + ".txt"
        try:
            file = open(fileName, 'r')
            content = file.read().decode("gbk")
            logs = content.split("\n\n")
            for j in range(len(logs)-1, -1,-1):
                if logs[j].isspace():
                    continue
                log = logs[j].split("-Trace>>")
                time = log[0].replace("\n", "").replace("Time:", "")
                traceData = log[1]
                logTable += "<tr><td>%s</td><td><textarea rows='3' cols='100'>%s</textarea></td></tr>" % (time, traceData)
        except:
            continue
    logTable += "</table>"
    return logTable

if __name__ == "__main__":
    show_server_logs("20150909")