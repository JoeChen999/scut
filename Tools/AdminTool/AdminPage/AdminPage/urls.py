"""AdminPage URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/1.8/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  url(r'^$', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  url(r'^$', Home.as_view(), name='home')
Including another URLconf
    1. Add an import:  from blog import urls as blog_urls
    2. Add a URL to urlpatterns:  url(r'^blog/', include(blog_urls))
"""
import django.contrib.auth.views
from django.conf.urls import include, url
from django.contrib import admin
from django.views import static
from func_logic import ConfigUtils
import views, services, devTools


configs = ConfigUtils.ConfigUtils()
urlpatterns = [
    #default
    url(r'^$', views.index, name='home'),
    url(r'^login/$', django.contrib.auth.views.login, {'template_name': 'login.html'}),
    url(r'^static/(?P<path>.*)', static.serve, {'document_root': configs.get("root_path") + 'AdminPage/static'}),
    url(r'^data/(?P<path>.*)', static.serve, {'document_root': configs.get("root_path") + '/data'}),
    #url(r'^Download/(?P<path>.*)', 'django.views.static.serve', {'document_root': './Download'}),
    url(r'^admin/', include(admin.site.urls)),
    #service
    url(r'^update/server/load$', services.update_server_load, name="server load update"),
    url(r'^get/server/list$', services.get_server_list_in_json, name="server load update"),
    url(r'^masterserver/player/register$', services.player_register, name="player register"),
    url(r'^masterserver/player/login$', services.player_login, name="player register"),
    url(r'^masterserver/player/check$', services.player_check, name="player register"),
    url(r'^Download/DataTable$', services.datatable_download, name="datatable_download"),
    url(r'^Download/Dictionary$', services.dictionary_download, name="dictionary_download"),
    url(r'^Get/DataFile/HashCode$', services.get_hash, name="get_hash"),
    url(r'^refresh/DataFile/HashCode$', services.refresh_hash, name="refresh_hash"),
    url(r'^version/data$', services.get_version_data, name="get_version_data"),
    url(r'^clear/version/data$', services.clear_version_data, name="clear_version_data"),
    #logic page
    url(r'^manageDataTable/upload/$', views.upload, name="upload datatable"),
    url(r'^manageDataTable/view/$', views.view_datatable, name="view datatable"),
    url(r'^beTools/redis$', devTools.redis_tool, name="redis tool"),
    url(r'^beTools/serverList/manage$', views.view_server_list, name="server list manage tool"),
    url(r'^beTools/serverList/edit$', devTools.edit_server_list, name="edit server list tool"),
    url(r'^gameconfigs$',views.game_config, name="game config tool"),
    url(r'^localNotifications/manage$', views.view_local_notifications, name="manage local notifications"),
    url(r'^localNotifications/edit$', views.edit_local_notifications, name="edit&add local notifications"),
    url(r'^Log/client/performance$', views.client_performance_log, name="client performance log"),
    url(r'^Log/client/error$', views.client_error_log, name="client error log"),
    url(r'^Log/server/error$', views.server_error_log, name="server error log"),
    url(r'^Log/BI$', views.bi_log, name="bi log"),
    url(r'^online/player/count$', views.view_online_player_count, name="onling player count"),
    url(r'^mails/view$', views.view_mails, name='view mails'),
    url(r'^mails/edit$', views.add_mail, name='edit mails'),
    url(r'^player/view$', devTools.view_player_info, name='view player info'),
    url(r'^manage/resource/version$', views.resource_manager, name='manage resource version'),
]
