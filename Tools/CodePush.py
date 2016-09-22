import paramiko
import os, sys
import time

LocalRoot = "E:\code\server\gameserver\\"
RemoteRoot = "/var/game/"
RemoteScriptFolder = "/var/game/"
LocalDir = LocalRoot
Hostname = 'lobby.server.genesis.antinc.cn'
Username = 'joe'
Password = 'joe232510'
FolderName = "LobbyServer"


def pushCode():
    folder_name = RemoteScriptFolder + FolderName
    transport = paramiko.Transport((Hostname, 22))
    transport.connect(username=Username, password=Password)
    sftp = paramiko.SFTPClient.from_transport(transport)
    for root, dirs, files in os.walk(LocalDir):
        remoteDir = folder_name + "/Script/" + root.replace(LocalDir, "").replace("\\", "/")
        for f in files:
            if os.path.splitext(f)[1] == "config":
                continue
            local_file = root + "\\" + f
            remote_file = remoteDir + "/" + f
            try:
                sftp.put(local_file, remote_file)
            except Exception, e:
                print e
            print "upload %s to remote %s" % (local_file, remote_file)
        for dir in dirs:
            remote_path = remoteDir + "/" + dir
            try:
                sftp.mkdir(remote_path)
                print "mkdir remote path %s" % remote_path
            except Exception, e:
                print dir + ": folder exists"


def printHelp():
    print "usage : python CodePush.py ServerType(Room|Lobby) ContentType(DataTale|Script)"

if __name__ == "__main__":
    if len(sys.argv) !=  3:
        printHelp()
        exit()

    if sys.argv[2] == "DataTable":
        folder += "\DataTables"
    elif sys.argv[2] == "Script":
        folder += "\Script"
    elif sys.argv[2] == "All":
        folder += ""
    else:
        printHelp()
        exit()

    if sys.argv[1] == "Lobby":
        LocalDir += "LobbyServer" + folder
        RemoteRoot += "LobbyServer"
        FolderName = "LobbyServer"
        Hostname = 'lobby.server.genesis.antinc.cn'
        pushCode()
    elif sys.argv[1] == "Room":
        LocalDir += "RoomServer" + folder
        RemoteRoot += "RoomServer"
        FolderName = "RoomServer"
        Hostname = 'room.server.genesis.antinc.cn'
        pushCode()
    elif sys.argv[1] == "Lobby2":
        LocalDir += "LobbyServer" + folder
        RemoteRoot += "LobbyServer"
        FolderName = "LobbyServer"
        Hostname = 'lobby.server2.genesis.antinc.cn'
        pushCode()
    elif sys.argv[1] == "World":
        LocalDir += "WorldServer" + folder
        RemoteRoot += "WorldServer"
        FolderName = "WorldServer"
        Hostname = 'master.genesis.antinc.cn'
        pushCode()
    elif sys.argv[1] == "All":
        LocalDir += "LobbyServer" + folder
        RemoteRoot += "LobbyServer"
        FolderName = "LobbyServer"
        Hostname = 'lobby.server.genesis.antinc.cn'
        pushCode()
        LocalDir += "RoomServer" + folder
        RemoteRoot += "RoomServer"
        FolderName = "RoomServer"
        Hostname = 'room.server.genesis.antinc.cn'
        pushCode()
        LocalDir += "LobbyServer" + folder
        RemoteRoot += "LobbyServer"
        FolderName = "LobbyServer"
        Hostname = 'lobby.server2.genesis.antinc.cn'
        pushCode()
        LocalDir += "WorldServer" + folder
        RemoteRoot += "WorldServer"
        FolderName = "WorldServer"
        Hostname = 'master.genesis.antinc.cn'
        pushCode()
    elif sys.argv[1] == "OutAll":
        LocalDir += "LobbyServer" + folder
        RemoteRoot += "LobbyServer"
        FolderName = "LobbyServer"
        Hostname = 'lobby.server.skfz.antinc.cn'
        pushCode()
        LocalDir += "RoomServer" + folder
        RemoteRoot += "RoomServer"
        FolderName = "RoomServer"
        Hostname = 'room.server.skfz.antinc.cn'
        pushCode()
        LocalDir += "LobbyServer" + folder
        RemoteRoot += "LobbyServer"
        FolderName = "LobbyServer"
        Hostname = 'lobby.server2.skfz.antinc.cn'
        pushCode()
        LocalDir += "WorldServer" + folder
        RemoteRoot += "WorldServer"
        FolderName = "WorldServer"
        Hostname = 'master.skfz.antinc.cn'
        pushCode()
    else:
        printHelp()
        exit()

