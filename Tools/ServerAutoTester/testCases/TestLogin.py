__author__ = 'ChenBiao'

from BaseTestCase import BaseTestCase
from protocols import CLLoginServer_pb2, LCLoginServer_pb2
import RequestSender


class TestLogin(BaseTestCase):
    def __init__(self):
        BaseTestCase.__init__(self)
        self.name = __name__
        self.request = CLLoginServer_pb2.CLLoginServer()
        self.response = LCLoginServer_pb2.LCLoginServer()

    def set_up(self):
        self.request.AccountName = "test"
        self.request.LoginKey = ""

    def execute(self):
        res = RequestSender.send(1000, self.request)
        self.response.ParseFromString(res)
        BaseTestCase.assert_equal(self, self.response.Authorized, True, "authorize failed")
        BaseTestCase.assert_equal(self, self.response.NewAccount, True, "not a new account")

