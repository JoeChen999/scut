#coding:utf-8
from django.shortcuts import render, render_to_response
from django.http import HttpResponseRedirect, HttpResponse
from django.contrib.auth.decorators import login_required
from func_logic import *
from forms import *

@login_required(login_url="/login/")
def index(request):
    return render(request, "index.html")
# Create your views here.


@login_required(login_url="/login/")
def upload(request):
    info = ""
    if request.method == "POST":
        if request.FILES:
            uploaded_file = request.FILES["file"]
            info = manage_datatable.do_update(uploaded_file)
            info = info.decode('gbk')
    return render_to_response('upload.html', {'info': info})


@login_required(login_url="/login/")
def view_datatable(request):
    datatables = manage_datatable.get_datatables()
    content = ""
    if request.GET:
        content = manage_datatable.view_datatable(request.GET["name"])
    return render_to_response("viewDatatable.html",{"datatables": datatables, "content": content })


@login_required(login_url="/login/")
def game_config(request):
    info = ""
    if request.method == "GET" and "serverid" in request.GET:
        serverid = request.GET["serverid"]
    if request.method == "POST":
        serverid = request.POST["serverid"]
        if manage_configs.edit_configs(request.POST):
            info = "success"
        else:
            info = "failed"
    configtable = manage_configs.get_configs(serverid)
    return render_to_response("gameconfig.html", {"configtable": configtable, "serverid": serverid, "info": info})


@login_required(login_url="/login/")
def view_local_notifications(request):
    info = ""
    action = request.GET.get('action')
    ln = manage_local_notifications.manage_local_notifications()
    if action:
        if action == 'active':
            if ln.active_notification(request.GET.get('id')):
                info = "active success"
            else:
                info = "active failed"
        elif action == 'inactive':
            if ln.inactive_notification(request.GET.get('id')):
                info = "inactive success"
            else:
                info = "inactive failed"
    lntable = ln.get_notifications()
    return render_to_response("localnotifications.html", {"lntable": lntable, "info": info})


@login_required(login_url="/login/")
def edit_local_notifications(request):
    ln = manage_local_notifications.manage_local_notifications()
    if request.method == "GET":
        key = request.GET.get('id')
        if key:
            name, time, message = ln.get_notification_info(key)
            return render_to_response("editnotification.html", {"name": name, "time": time, "message": message})
        else:
            return render_to_response("editnotification.html")
    if request.method == "POST":
        name = request.POST["Name"]
        time = request.POST["Time"]
        message = request.POST["Message"]
        action = request.POST["action"]
        if action == "edit":
            ln.edit_notification(name, time, message)
        else:
            ln.add_notification(name, time, message)
    return HttpResponseRedirect('/localNotifications/manage')


@login_required(login_url="/login/")
def bad_words(request):
    info = ""
    return render_to_response("badwords.html",{"info":info})


@login_required(login_url="/login/")
def client_performance_log(request):
    info = ""
    return render_to_response("clientperformancelog.html",{"info":info})


@login_required(login_url="/login/")
def client_performance_log_record(request):
    return True


@login_required(login_url="/login/")
def client_error_log(request):
    return render_to_response("clienterrorlog.html")


@login_required(login_url="/login/")
def server_error_log(request):
    form = PostForm(request.POST)
    info = ""
    if request.method == "POST":
        dateString = request.POST["date"].replace("-", "")
        info = manage_log.show_server_logs(dateString)
    return render_to_response("servererrorlog.html", {"form": form, "info": info})


@login_required(login_url="/login/")
def bi_log(request):
    tables = manage_bi_log.get_bi_log_type()
    logs = []
    selected_type = ""
    sql = ""
    if request.method == "POST":
        selected_type = request.POST["type"]
        sql = request.POST["sql"]
        logs = manage_bi_log.get_bi_logs(selected_type, sql)
        manage_bi_log.write_to_temp_file(logs)
    return render_to_response("bilog.html", {"tables": tables, "logs": logs, "selectedType": selected_type, "sql": sql})


@login_required(login_url="/login/")
def view_online_player_count(request):
    count = online_player_count.get_online_player_count()
    return render_to_response("onlineplayercount.html", {"count": count})


@login_required(login_url="/login/")
def view_server_list(request):
    info = ""
    sl = manage_server_list.ManageServerList()
    sltable = sl.get_server_list()
    return render_to_response("serverlist.html", {"sltable": sltable, "info": info})


@login_required(login_url="/login/")
def view_mails(request):
    info = ""
    action = request.GET.get('action')
    m = manage_mails.manage_mails()
    if action and action == "delete":
        m.delete_mail(request.GET.get('id'))
        return HttpResponseRedirect('/mails/view')
    page = request.GET.get('page')
    if not page:
        page = 1
    mail_table = m.get_mails(page)
    return render_to_response("viewmails.html", {"m_table": mail_table, "info": info, "pre_page": page - 1, "next_page": page + 1, "is_final": len(mail_table) < 200})


@login_required(login_url="/login/")
def add_mail(request):
    info = ""
    m = manage_mails.manage_mails()
    if request.method == "GET":
        return render_to_response("editmails.html", {"info": info})
    if request.method == "POST":
        message = request.POST["Message"]
        attachType = request.POST["AttachType"]
        attachCount = request.POST["AttachCount"]
        startTime = request.POST["StartTime"]
        expireTime = request.POST["ExpireTime"]
        toUser = request.POST["ToUser"]
        if m.add_mail(message, attachType, attachCount, startTime, expireTime, toUser):
            info = "success"
        else:
            info = "failed"
    return render_to_response("editmails.html", {"info": info})


@login_required(login_url="/login/")
def resource_manager(request):
    info = ""
    if request.method == "POST":
        info = "success"
        for value in request.POST:
            keys = value.split("_")
            if len(keys) == 1:
                manage_resource_version.edit_resource_data(keys[0], request.POST[value])
            elif len(keys) == 2:
                manage_resource_version.edit_version_data(keys[0], keys[1], request.POST[value])
    resource_data, version_data = manage_resource_version.get_resource_data()
    return render_to_response("versiondata.html", {"resource_data": resource_data, "version_data": version_data, "info": info})
