import socket
import struct
from pb2py import CLPacketHead_pb2, LCPacketHead_pb2
from ConfigUtils import ConfigUtils

conf = ConfigUtils()
address = (conf.get("lobby_server"), 9001)
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect(address)


def send(action_id, request):
    head = CLPacketHead_pb2.CLPacketHead()
    head.MsgId = 0
    head.ActionId = action_id
    head.SessionId = ""
    head.UserId = 0
    head_buff = head.SerializeToString()
    tmp_head = struct.pack("i%ds" % len(head_buff), len(head_buff), head_buff)
    req_buff = request.SerializeToString()
    buff_stream = struct.pack("i%ds%ds" % (len(tmp_head), len(req_buff)), len(tmp_head)+len(req_buff), tmp_head, req_buff)
    try:
        s.send(buff_stream)
    except:
        s.connect(address)
        s.send(buff_stream)
    while True:
        size_buffer = s.recv(4)
        buff_size = struct.unpack("i", size_buffer)
        res = s.recv(buff_size[0])
        head_size = struct.unpack("i", res[:4])
        res_data = res[head_size[0] + 4:]
        res_head = LCPacketHead_pb2.LCPacketHead()
        res_head.ParseFromString(res[4: head_size[0] + 4])
        if res_head.ActionId == action_id:
            break
    return res_data
