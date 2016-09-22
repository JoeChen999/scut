__author__ = 'ChenBiao'
import testCases
import unittest


runner = unittest.TextTestRunner()
for case in testCases.__all__:
    for i in xrange(case[1]):
        case[0].set_up()
        case[0].prepare()
        case[0].execute()
        case[0].ending()
        case[0].dispose()
