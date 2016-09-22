__author__ = 'ChenBiao'
from Color import Color
import traceback


def print_trace_back():
    stack = traceback.extract_stack()
    Color.print_blue_text(stack[1][0] + "\nline:" + str(stack[1][1]) + "\n" + stack[1][3])


class BaseTestCase:
    def __init__(self):
        self.success = True
        pass

    def set_up(self):
        pass

    def prepare(self):
        Color.print_blue_text("Start Running TestCase: %s\n" % self.name)

    def execute(self):
        pass

    def ending(self):
        if self.success:
            Color.print_green_text("OKAY\n")
        else:
            Color.print_red_text("FAIL\n")

    def dispose(self):
        pass

    def assert_equal(self, case1, case2, message):
        if case1 == case2:
            return True
        else:
            Color.print_red_text("%s::::Assert %s is equal to %s.\n" % (message, str(case1), str(case2)))
            self.success = False
            print_trace_back()
            return False

    def assert_not_equal(self, case1, case2, message):
        if not case1 == case2:
            return True
        else:
            Color.print_red_text("%s::::Assert %s isn't equal to %s.\n" % (message, str(case1), str(case2)))
            self.success = False
            print_trace_back()
            return False

    def assert_greater(self, case1, case2, message):
        if case1 > case2:
            return True
        else:
            Color.print_red_text("%s::::Assert %s is greater than %s.\n" % (message, str(case1), str(case2)))
            self.success = False
            print_trace_back()
            return False
