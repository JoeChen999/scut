__author__ = 'ChenBiao'
from django.contrib.admin import widgets
from django import forms


class PostForm(forms.Form):
    date = forms.DateTimeField(required=True, label='time', widget=widgets.AdminDateWidget())


class BIForm(forms.Form):
    beginTime = forms.DateTimeField(required=True, label='beginTime', widget=widgets.AdminDateWidget())
    endTime = forms.DateTimeField(required=True, label='endTime', widget=widgets.AdminDateWidget())


class MailForm(forms.Form):
    StartDate = forms.DateTimeField(required=True, label='StartDate', widget=widgets.AdminDateWidget())
    StartTime = forms.DateTimeField(required=False, label='StartTime', widget=widgets.AdminTimeWidget())
    ExpireDate = forms.DateTimeField(required=False, label='ExpireDate', widget=widgets.AdminDateWidget())
    ExpireTime = forms.DateTimeField(required=False, label='ExpireTime', widget=widgets.AdminTimeWidget())
