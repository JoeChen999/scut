import paramiko
import os
import socket
import struct
import json
import urllib2
import CLPacketHead_pb2, LCPacketHead_pb2, CLServerCommand_pb2, LCServerCommand_pb2


def send(s, action_id, request):
    head = CLPacketHead_pb2.CLPacketHead()
    head.MsgId = 0
    head.ActionId = action_id
    head.SessionId = ""
    head.UserId = 0
    head_buff = head.SerializeToString()
    tmp_head = struct.pack("i%ds" % len(head_buff), len(head_buff), head_buff)
    req_buff = request.SerializeToString()
    buff_stream = struct.pack("i%ds%ds" % (len(tmp_head), len(req_buff)), len(tmp_head)+len(req_buff), tmp_head, req_buff)
    s.send(buff_stream)
    while True:
        size_buffer = s.recv(4)
        buff_size = struct.unpack("i", size_buffer)
        res = s.recv(buff_size[0])
        head_size = struct.unpack("i", res[:4])
        res_data = res[head_size[0] + 4:]
        res_head = LCPacketHead_pb2.LCPacketHead()
        res_head.ParseFromString(res[4: head_size[0] + 4])
        if res_head.ActionId == action_id:
            break
    return res_data


def push(serverid):
    transport = paramiko.Transport((configs["LobbyHost"], 22))
    transport.connect(username=configs["UserName"], password=configs["PassWord"])
    sftp = paramiko.SFTPClient.from_transport(transport)
    for lobby_table in configs["LobbyServer"]:
        local = ".\\DataTable\\" + lobby_table + ".txt"
        try:
            sftp.put(local, configs["LobbyDirectory"] + lobby_table + ".txt")
        except Exception, e:
            print e
        print "upload %s to remote Lobby %s" % (local, configs["LobbyDirectory"])

    transport = paramiko.Transport((configs["RoomHost"], 22))
    transport.connect(username=configs["UserName"], password=configs["PassWord"])
    sftp = paramiko.SFTPClient.from_transport(transport)
    for room_table in configs["RoomServer"]:
        local = ".\\DataTable\\" + room_table + ".txt"
        try:
            sftp.put(local, configs["RoomDirectory"] + room_table + ".txt")
        except Exception, e:
            print e
        print "upload %s to remote Room %s" % (local, configs["RoomDirectory"])

    transport = paramiko.Transport((configs["MasterHost"], 22))
    transport.connect(username=configs["UserName"], password=configs["PassWord"])
    sftp = paramiko.SFTPClient.from_transport(transport)
    for root, dirs, files in os.walk(".\\DataTable"):
        for f in files:
            local = ".\\DataTable\\" + f
            try:
                sftp.put(local, configs["ClientDirectory"] + serverid + "/" + "DataTable/" + f)
            except Exception, e:
                print e
            print "upload %s to remote Client %s" % (local, configs["ClientDirectory"])
    for root, dirs, files in os.walk(".\\Dictionary"):
        for f in files:
            local = root + "\\" + f
            try:
                remote_dir = root[2:].replace("\\", "/")
                sftp.put(local, configs["ClientDirectory"] + serverid + "/" + remote_dir + "/" + f)
            except Exception, e:
                print e
            print "upload %s to remote Client %s" % (local, configs["ClientDirectory"])


def reload(server_type):
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    if server_type == 1:
        server_name = "Lobby"
        address = (configs["LobbyHost"], configs["LobbyPort"])
    else:
        server_name = "Room"
        address = (configs["RoomHost"], configs["RoomPort"])
    s.connect(address)
    response = LCServerCommand_pb2.LCServerCommand()
    request = CLServerCommand_pb2.CLServerCommand()
    request.Type = 1
    res = send(s, 101, request)
    response.ParseFromString(res)
    if response.Success:
        print server_name + " server reload success"
    else:
        print server_name + " server reload failed"


def refresh_client(serverid):
    response = urllib2.urlopen(configs["RefreshClientUrl"] + "?serverid=" + serverid)
    print response.read()


if __name__ == "__main__":
    envnum = raw_input("choose environment (1.internal, 0.out):")
    if envnum == "1":
         env = "internal"
    elif envnum == "0":
         env = "out"
    else:
        pass
    serverid = raw_input("input serverId:")


    ConfigFile = ".\\TableConfigs_" + env + serverid + ".json"
    configs = json.load(open(ConfigFile, "r"))
    push(serverid)
    reload(1)
    reload(2)
    refresh_client(serverid)
    os.system('pause')

