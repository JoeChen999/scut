import os
import sys

path = '/var/game/AdminTool/AdminPage'

if path not in sys.path:

    sys.path.insert(0, '/var/game/AdminTool/AdminPage')

os.environ['DJANGO_SETTINGS_MODULE'] = 'AdminPage.settings'

from django.core.wsgi import get_wsgi_application

application = get_wsgi_application()