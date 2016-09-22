from distutils.core import setup
import py2exe

setup(console=["ServerPusher.py"],
      py_module=["pkg_resources"],
      data_files=["TableConfigs_internal1.json", "TableConfigs_internal2.json", "TableConfigs_out1.json"]
      )
