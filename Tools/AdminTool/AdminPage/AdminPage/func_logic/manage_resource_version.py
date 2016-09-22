import redis
import json
from xml.dom import minidom
from ConfigUtils import ConfigUtils

configs = ConfigUtils()
if configs.get("redis_pwd"):
    RedisClient = redis.Redis(host=configs.get("redis_host"), port=configs.get("redis_port"), db=0, password=configs.get("redis_pwd"))
else:
    RedisClient = redis.Redis(host=configs.get("redis_host"), port=configs.get("redis_port"), db=0)
GameResourceVersionKey = "Game_Resource_Version"
GameVersionKey = "Game_Version_Info"
GameServerKey = "Game_Server_Host"
DataFilePath = configs.get("root_path") + "data/"


def get_resource_data():
    resource_data = {}
    version_data = {}
    server_list = RedisClient.hgetall(GameServerKey)
    if len(server_list) == 0 or not server_list["UpdateServer"] \
            or not server_list["CheckServerListUri"] or not server_list["LoginUri"]:
        server_list = load_game_server()
    resource_data["UpdateServer"] = server_list["UpdateServer"]
    resource_data["CheckServerListUri"] = server_list["CheckServerListUri"]
    resource_data["LoginUri"] = server_list["LoginUri"]
    game_version = RedisClient.hgetall(GameVersionKey)
    if len(game_version) == 0:
        game_version = load_game_version()
    for k, v in game_version.items():
        if k == "LatestVersion":
            resource_data["LatestVersion"] = v
        else:
            version_data[k] = {}
            info = json.loads(v)
            for key, value in info.items():
                version_data[k][key] = value
    return resource_data, version_data


def edit_resource_data(key, value):
    if key == "LatestVersion":
        RedisClient.hset(GameVersionKey, "LatestVersion", value)
    else:
        if RedisClient.hget(GameServerKey, key):
            RedisClient.hset(GameServerKey, key, value)


def edit_version_data(key1, key2, value):
    string = RedisClient.hget(GameVersionKey, key1)
    if not string:
        return
    data = json.loads(string)
    data[key2] = value
    string = json.dumps(data)
    RedisClient.hset(GameVersionKey, key1, string)


def get_platform_name(platform):
    if platform == "WindowsEditor" or platform == "WindowsPlayer":
        return "StandaloneWindows"
    elif platform == "OSXEditor" or platform == "OSXPlayer":
        return "StandaloneOSXIntel"
    elif platform == "IPhonePlayer":
        return "iOS"
    elif platform == "Android":
        return "Android"
    elif platform == "WP8Player":
        return "WP8Player"
    elif platform == "MetroPlayerX86" or platform == "MetroPlayerX64" or platform == "MetroPlayerARM":
        return "WSAPlayer"
    else:
        return ""


def get_platform_path(platform):
    if platform == "WindowsEditor" or platform == "WindowsPlayer":
        return "windows"
    elif platform == "OSXEditor" or platform == "OSXPlayer":
        return "osx"
    elif platform == "IPhonePlayer":
        return "ios"
    elif platform == "Android":
        return "android"
    elif platform == "WP8Player":
        return "winphone"
    elif platform == "MetroPlayerX86" or platform == "MetroPlayerX64" or platform == "MetroPlayerARM":
        return "winstore"
    else:
        return ""


def get_resource_version_name(applicable_game_version, latest_internal_resource_version_string):
    if not applicable_game_version or not latest_internal_resource_version_string:
        return ""
    split_applicable_game_version = applicable_game_version.split('.')
    if not len(split_applicable_game_version) == 3:
        return ""
    return "%s_%s_%s_%s" % (split_applicable_game_version[0], split_applicable_game_version[1],
                            split_applicable_game_version[2],latest_internal_resource_version_string)


def load_game_resource_version(filename, gameversion):
    doc = minidom.parse(DataFilePath + filename)
    root = doc.documentElement
    game_resource_version = {
        "ApplicableGameVersion": root.getAttribute("ApplicableGameVersion"),
        "LatestInternalResourceVersion": root.getAttribute("LatestInternalResourceVersion")
    }
    for node in root.childNodes:
        if node.nodeType == node.ELEMENT_NODE:
            game_resource_version[node.nodeName] = {}
            game_resource_version[node.nodeName]["Length"] = node.getAttribute("Length")
            game_resource_version[node.nodeName]["HashCode"] = node.getAttribute("HashCode")
            game_resource_version[node.nodeName]["ZipLength"] = node.getAttribute("ZipLength")
            game_resource_version[node.nodeName]["ZipHashCode"] = node.getAttribute("ZipHashCode")
    retval = json.dumps(game_resource_version)
    RedisClient.hset(GameResourceVersionKey, gameversion, retval)
    return retval


def load_game_server():
    doc = minidom.parse(DataFilePath + "Publisher.xml")
    root = doc.documentElement
    RedisClient.hset(GameServerKey, "CheckServerListUri",
                     root.getElementsByTagName("CheckServerListUri")[0].firstChild.data)
    RedisClient.hset(GameServerKey, "LoginUri", root.getElementsByTagName("LoginUri")[0].firstChild.data)
    doc = minidom.parse(DataFilePath + "UpdateServer.xml")
    root = doc.documentElement
    RedisClient.hset(GameServerKey, "UpdateServer", root.getElementsByTagName("Uri")[0].getAttribute("Value"))
    return RedisClient.hgetall(GameServerKey)


def load_game_version():
    doc = minidom.parse(DataFilePath + "GameVersion.xml")
    root = doc.documentElement
    RedisClient.hset(GameVersionKey, "LatestVersion", root.getAttribute("LatestVersion"))
    for node in root.childNodes:
        if node.nodeType == node.ELEMENT_NODE:
            allowed_version = {
                "Resource": node.getAttribute("Resource"),
                "VerifiedIOSInternalVersion": node.getAttribute("VerifiedIOSInternalVersion"),
                "VerifiedAndroidInternalVersion": node.getAttribute("VerifiedAndroidInternalVersion")
            }
            RedisClient.hset(GameVersionKey, node.getAttribute("Version"), json.dumps(allowed_version))
    return RedisClient.hgetall(GameVersionKey)


def clear_resource_version_data(version):
    RedisClient.hdel(GameResourceVersionKey, version)


def get_version_data(platform, version):
    platform_name = get_platform_name(platform)
    platform_path = get_platform_path(platform)
    version_data = {}
    server_list = RedisClient.hgetall(GameServerKey)
    if len(server_list) == 0 or not server_list["UpdateServer"] \
            or not server_list["CheckServerListUri"] or not server_list["LoginUri"]:
        server_list = load_game_server()
    update_server = server_list["UpdateServer"]
    server_list_server = server_list["CheckServerListUri"]
    login_server = server_list["LoginUri"]
    game_version = RedisClient.hgetall(GameVersionKey)
    if len(game_version) == 0:
        game_version = load_game_version()
    latest_version = game_version["LatestVersion"]
    if game_version[version]:
        info_str = game_version[version]
        info = json.loads(info_str)
        resource = info["Resource"]
        if platform_name == "iOS":
            version_data["VerifiedInternalApplicationVersion"] = int(info["VerifiedIOSInternalVersion"])
        elif platform_name == "Android":
            version_data["VerifiedInternalApplicationVersion"] = int(info["VerifiedAndroidInternalVersion"])
        else:
            version_data["VerifiedInternalApplicationVersion"] = 0
    version_data["ForceGameUpdate"] = False if resource else True
    version_data["LatestGameVersion"] = latest_version
    if not resource:
        return version_data
    game_resource_version_info = RedisClient.hget(GameResourceVersionKey, version)
    if not game_resource_version_info:
        game_resource_version_info = load_game_resource_version(resource, version)
    game_resource_version = json.loads(game_resource_version_info)
    version_data["LatestInternalResourceVersion"] = int(game_resource_version["LatestInternalResourceVersion"])
    resource_version_name = get_resource_version_name(game_resource_version["ApplicableGameVersion"],
                                                      game_resource_version["LatestInternalResourceVersion"])
    version_data["UpdatePrefixUri"] = "%s/%s/%s" % (update_server, resource_version_name, platform_path)
    version_data["VersionListHashCode"] = game_resource_version[platform_name]["HashCode"]
    version_data["VersionListZipLength"] = game_resource_version[platform_name]["ZipLength"]
    version_data["VersionListZipHashCode"] = game_resource_version[platform_name]["ZipHashCode"]
    version_data["CheckServerListUri"] = server_list_server
    version_data["LoginUri"] = login_server
    return version_data

if __name__ == "__main__":
    r, v = get_resource_data()
    print r
    print v
    #version = get_version_data("IPhonePlayer", "0.1.0")
    #print json.dumps(version)
