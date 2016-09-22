__author__ = 'ChenBiao'
import sys, time
import re
import os, shutil

regSplice = '[\t]'


class ModelGenerator:
    def __init__(self, filePath, serverType, isDataTable):
        self.__FilePath = filePath
        self.__ServerType = serverType
        self.__IsDataTable = isDataTable
        self.__KeyName = []
        self.__KeyType = []

    def __read_data_table(self):
        index = 0
        for line in open(self.__FilePath, 'r'):
            index += 1
            if index == 1:
                continue
            elif index == 2:
                dtLine = line.strip()
                self.__KeyName = re.split(regSplice, dtLine)
                del self.__KeyName[0]
                del self.__KeyName[1]
            elif index == 3:
                dtLine = line.strip()
                self.__KeyType = re.split(regSplice, dtLine)
                del self.__KeyType[0]
                del self.__KeyType[1]
            elif index > 3:
                break

    def __write_model_script(self):
        dt_file = os.path.split(self.__FilePath)[1]
        script_file_name = "DT"+os.path.splitext(dt_file)[0]
        dt_string = ""
        if self.__IsDataTable:
            dt_string = ", IDataTable"
        script_file = open("output\\" + script_file_name + ".cs", 'w+')
        # write namespace
        script_file.write(
            "using Genesis.GameServer.CommonLibrary;\nusing ProtoBuf;\nusing System;\nusing ZyGames.Framework.Cache.Generic;\nusing ZyGames.Framework.Model;\n\n")
        script_file.write("namespace Genesis.GameServer.%sServer\n" % self.__ServerType)
        # write class attribute add construct
        script_file.write("{\n\t[Serializable, ProtoContract]\n\tpublic class %s : MemoryEntity%s\n\t{\n" % (
            script_file_name, dt_string))
        if self.__IsDataTable:
            script_file.write("\t\tprivate MemoryCacheStruct<%s> %sCache = new MemoryCacheStruct<%s>();\n\n" % (
                script_file_name, script_file_name, script_file_name))
        script_file.write("\t\tpublic %s()\n\t\t{\n\n\t\t}\n\n" % script_file_name)
        # write properties
        property_index = 0;
        for key in self.__KeyName:
            is_primary_key = ''
            if property_index == 0:
                is_primary_key = "(true)"
            script_file.write(
                "\t\t[ProtoMember(%s), EntityField%s]\n\t\tpublic %s %s\n\t\t{\n\t\t\tget;\n\t\t\tset;\n\t\t}\n\n" % (
                    property_index + 1, is_primary_key, self.__KeyType[property_index], self.__KeyName[property_index]))
            property_index += 1
        # write funcs
        if self.__IsDataTable:
            script_file.write(
                "\t\tpublic void ParseRow(string[] rowData)\n\t\t{\n\t\t\tint index = 0;\n\t\t\tindex++;\n\t\t\t%s = int.Parse(rowData[index++]);\n\t\t\tindex++;\n" %
                self.__KeyName[0])
            for i in range(1, len(self.__KeyName)):
                if (self.__KeyType[i] == "string"):
                    script_file.write("\t\t\t%s = rowData[index++];\n" % (self.__KeyName[i]))
                else:
                    script_file.write(
                        "\t\t\t%s = %s.Parse(rowData[index++]);\n" % (self.__KeyName[i], self.__KeyType[i]))
            script_file.write("\n\t\t\t%sCache.TryAdd(Id.ToString(), this);\n\t\t}\n" % script_file_name)
        script_file.write("\t}\n}")
        script_file.close()
        script_file = open("output\\" + script_file_name + ".cs", 'r')
        content = script_file.read().replace("\t", "    ")
        script_file.close()
        script_file = open("output\\" + script_file_name + ".cs", 'w')
        script_file.write(content)

    def run(self):
        if os.path.exists("output"):
            shutil.rmtree("output")
        time.sleep(0.5)
        os.mkdir("output")
        if os.path.isdir(self.__FilePath):
            for files in os.walk(self.__FilePath):
                for f in files[2]:
                    if os.path.splitext(f)[1] == ".txt":
                        self.__FilePath = self.__FilePath + "\\" + f
                        self.__read_data_table()
                        self.__write_model_script()
                        self.__FilePath = os.path.split(self.__FilePath)[0]
        else:
            self.__read_data_table()
            self.__write_model_script()


def print_help():
    print "Wrong type of Args:\n[1]filePath:your datatable file path(data table file or a folder of data table files is allowed)\n"
    print "[2]serverType:Room|Lobby\n"
    # print "[3]isDataTable:[optional]will generate data table model when values 'DataTable'\n"
    # print "example:\npython ModelGenerator.py E:\code\server\dt Lobby DataTable\n"
    print "python ModelGenerator.py E:\code\server\model Lobby"
    exit()


if __name__ == '__main__':
    if len(sys.argv) != 3:
        print_help()
    filePath = sys.argv[1]
    serverType = sys.argv[2]
    isDataTable = True

    mg = ModelGenerator(filePath, serverType, isDataTable)
   # mg = ModelGenerator("E:\\code\\server\\dt", "Lobby", False)
    mg.run()





