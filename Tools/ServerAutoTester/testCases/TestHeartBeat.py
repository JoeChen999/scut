__author__ = 'ChenBiao'

from BaseTestCase import BaseTestCase
from protocols import CLHeartBeat_pb2, LCHeartBeat_pb2
import RequestSender
import utility

class TestHeartBeat(BaseTestCase):
    def __init__(self):
        BaseTestCase.__init__(self)
        self.name = __name__
        self.request = CLHeartBeat_pb2.CLHeartBeat()
        self.response = LCHeartBeat_pb2.LCHeartBeat()

    def set_up(self):
        self.request.ClientTime.Low = 0
        self.request.ClientTime.High = 0

    def execute(self):
        res = RequestSender.send(1001, self.request)
        self.response.ParseFromString(res)

        servertime = utility.pb_int64_to_long(self.response.ServerTime)
        BaseTestCase.assert_greater(self, servertime, 0, "Wrong server time.")
        clienttime = utility.pb_int64_to_long(self.response.ClientTime)
        BaseTestCase.assert_equal(self, clienttime, 0, "Wrong client time.")
