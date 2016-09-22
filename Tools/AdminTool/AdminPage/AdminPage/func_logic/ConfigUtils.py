import json
from singleton import singleton
ConfigPath = "/var/game/AdminTool/AdminPage/config.json"


@singleton
class ConfigUtils:
    def __init__(self):
        self.configs = json.load(open(ConfigPath, "r"))

    def get(self, name):
        if name in self.configs:
            return self.configs[name]
        else:
            return None

