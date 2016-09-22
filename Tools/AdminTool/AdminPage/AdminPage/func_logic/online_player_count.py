__author__ = 'ChenBiao'

import commands


def get_online_player_count():
    status, output = commands.getstatusoutput("mono SeverContracter.exe 4 1")
    if status > 0:
        print output
        return "get online player count failed"
    else:
        return output

if __name__ == "__main__":
    get_online_player_count()
