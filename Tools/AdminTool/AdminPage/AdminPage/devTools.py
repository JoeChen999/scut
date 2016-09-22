from django.shortcuts import render, render_to_response
from django.http import HttpResponseRedirect, HttpResponse
from django.contrib.auth.decorators import login_required
from func_logic import *
from forms import *


@login_required(login_url="/login/")
def redis_tool(request):
    info = ""
    keys = manage_redis.get_keys()
    if request.method == "POST":
        if request.POST and request.POST['field'] != "":
            if request.POST['action'] == "flush":
                info = manage_redis.do_flush(request.POST["key"], request.POST["field"])
                info = '<p style="color:#FF0000">%s</p>' % info
            else:
                info = manage_redis.do_view(request.POST["key"], request.POST["field"])
    return render_to_response("redistool.html", {"keys": keys, "info": info})


@login_required(login_url="/login/")
def view_player_info(request):
    return render_to_response("redistool.html")


@login_required(login_url="/login/")
def edit_server_list(request):
    sl = manage_server_list.ManageServerList()
    if request.method == "GET":
        Id = request.GET.get('id')
        if Id:
            sid, name, description, load, status, address, port, tabid, recommended, dictionaryurl, datatableurl, metafileurl = sl.get_server_info(Id)
            return render_to_response("editserverlist.html", {"id": sid, "name": name, "description": description, "status": status, "address": address, "port": port, "tabid": tabid, "recommended": recommended, "dictionaryurl": dictionaryurl, "datatableurl": datatableurl, "metafileurl": metafileurl})
        else:
            return render_to_response("editserverlist.html")
    if request.method == "POST":
        id = request.POST["Id"]
        name = request.POST["Name"]
        description = request.POST["Description"]
        status = request.POST["Status"]
        address = request.POST["Address"]
        port = request.POST["Port"]
        tabid = request.POST["TabId"]
        recommended = request.POST["Recommended"]
        dictionaryurl = request.POST["DictionaryUrl"]
        datatableurl = request.POST["DatatableUrl"]
        metafileurl = request.POST["MetaFileUrl"]
        action = request.POST["action"]
        if action == "edit":
            sl.edit_server_list(id, name, description, status, address, port, tabid, recommended, dictionaryurl, datatableurl, metafileurl)
        else:
            sl.add_server_list(id, name, description, status, address, port, tabid, recommended, dictionaryurl, datatableurl, metafileurl)
    return HttpResponseRedirect('/beTools/serverList/manage')