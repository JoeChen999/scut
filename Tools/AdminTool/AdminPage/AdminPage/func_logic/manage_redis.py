__author__ = 'ChenBiao'
import redis,commands
from pb2py import *
import manage_datatable

#redis_host = 'localhost'
key_prefix = "$Genesis.GameServer.LobbyServer."
redis_host = 'redis.genesis.antinc.cn'
m_redis = redis.Redis(redis_host,6379)

def get_keys():
    allkeys = m_redis.keys(key_prefix + "*")
    keys = []
    for key in allkeys:
        piece = key.split('.')
        keys.append(piece[len(piece) - 1])
    return keys

def do_flush(key, field):
    m_redis.hdel(key_prefix + key, field)
    status, output = commands.getstatusoutput("mono SeverContracter.exe 2 " + key)
    retval = ""
    if not status == 0:
        retval += "flush faied\n"
    else:
        retval += "flush success\n"
    retval += output
    return retval


def do_view(key, field):
    data = m_redis.hget(key_prefix + key, field)
    if not data:
        return ""
    model_object = None
    exec("model_object = %s_pb2.%s()" % (key, key))
    model_object.ParseFromString(data)
    values = model_object.__str__()
    items = []
    lines = values.split('\n')
    for line in lines:
        if line.find(':') < 0:
            continue
        kv = line.split(':')
        items.append(kv[0])
        items.append(kv[1])
    retstr = manage_datatable.to_table(items,2)
    return retstr

if __name__ == "__main__":
    print do_view("Player", "1")