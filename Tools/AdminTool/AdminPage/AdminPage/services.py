from django.http import HttpResponse
from func_logic import *
import json


def player_register(request):
    if request.method == "POST":
        res = {"ok": manage_player.vi (request.POST["AccountName"], request.POST["Password"])}
        return HttpResponse(json.dumps(res))
    else:
        res = {"ok": False}
        return HttpResponse(json.dumps(res))


def player_login(request):
    res = {"AuthorizedCode": 0, "AccountName": request.POST["AccountName"], "LoginKey": request.POST["Password"] + "123"}
    return HttpResponse(json.dumps(res))


def player_check(request):
    if request.method == "POST":
        res = {"ok": manage_player.player_register(request.POST["userId"], request.POST["token"])}
        return HttpResponse(json.dumps(res))
    else:
        res = {"ok": False}
        return HttpResponse(json.dumps(res))


def update_server_load(request):
    sl = manage_server_list.ManageServerList()
    return HttpResponse(sl.update_server_load(request))


def get_server_list_in_json(request):
    sl = manage_server_list.ManageServerList()
    return HttpResponse(sl.get_server_list_in_json())


def datatable_download(request):
    if request.method == "GET":
        c = ConfigUtils.ConfigUtils()
        filename = "".join((c.get("root_path"), "Download/", request.GET["serverid"], "/DataTable/", request.GET["name"], ".txt.gz"))
        f = open(filename, "r")
        return HttpResponse(f.read())


def dictionary_download(request):
    if request.method == "GET":
        c = ConfigUtils.ConfigUtils()
        filename = "".join((c.get("root_path"), "Download/", request.GET["serverid"], "/Dictionary/", request.GET["language"], "/", request.GET["name"], ".txt.gz"))
        f = open(filename, "r")
        return HttpResponse(f.read())


def get_hash(request):
    if request.method == "GET":
        c = ConfigUtils.ConfigUtils()
        filename = "".join((c.get("root_path"), "Download/", request.GET["serverid"], "/hashCode.txt"))
        f = open(filename, "r")
        return HttpResponse(f.read())


def refresh_hash(request):
    if request.method == "GET":
        return HttpResponse(manage_datatable.refresh_datatable(request.GET["serverid"]))


def reload_resource_file(request):
    return HttpResponse("")


def get_version_data(request):
    if request.method == "POST":
        data = manage_resource_version.get_version_data(request.POST["Platform"], request.POST["GameVersion"])
        return HttpResponse(json.dumps(data))
    else:
        data = manage_resource_version.get_version_data("IPhonePlayer", "0.1.0")
        return HttpResponse(json.dumps(data))


def clear_version_data(request):
    if request.method == "GET":
        manage_resource_version.clear_resource_version_data(request.GET["version"])
        return HttpResponse("success")
    return HttpResponse("failed")
