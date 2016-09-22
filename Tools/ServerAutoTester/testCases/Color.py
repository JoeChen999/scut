__author__ = 'ChenBiao'
import ctypes

STD_INPUT_HANDLE = -10
STD_OUTPUT_HANDLE = -11
STD_ERROR_HANDLE = -12

FOREGROUND_BLACK = 0x0
FOREGROUND_BLUE = 0x01  # text color contains blue.
FOREGROUND_GREEN = 0x02  # text color contains green.
FOREGROUND_RED = 0x04  # text color contains red.
FOREGROUND_INTENSITY = 0x08  # text color is intensified.

BACKGROUND_BLUE = 0x10  # background color contains blue.
BACKGROUND_GREEN = 0x20  # background color contains green.
BACKGROUND_RED = 0x40  # background color contains red.
BACKGROUND_INTENSITY = 0x80  # background color is intensified.


class Color(object):
    std_out_handle = ctypes.windll.kernel32.GetStdHandle(STD_OUTPUT_HANDLE)

    def __init__(self):
        pass

    @staticmethod
    def set_cmd_color(color, handle=std_out_handle):
        return ctypes.windll.kernel32.SetConsoleTextAttribute(handle, color)

    @staticmethod
    def reset_color():
        Color.set_cmd_color(FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE)

    @staticmethod
    def print_red_text(print_text):
        Color.set_cmd_color(FOREGROUND_RED | FOREGROUND_INTENSITY)
        print print_text
        Color.reset_color()

    @staticmethod
    def print_green_text(print_text):
        Color.set_cmd_color(FOREGROUND_GREEN | FOREGROUND_INTENSITY)
        print print_text
        Color.reset_color()

    @staticmethod
    def print_blue_text(print_text):
        Color.set_cmd_color(FOREGROUND_BLUE | FOREGROUND_INTENSITY)
        print print_text
        Color.reset_color()

    @staticmethod
    def print_red_text_with_blue_bg(print_text):
        Color.set_cmd_color(FOREGROUND_RED | FOREGROUND_INTENSITY | BACKGROUND_BLUE | BACKGROUND_INTENSITY)
        print print_text
        Color.reset_color()

if __name__ == "__main__":
    Color.print_red_text('red')
    Color.print_green_text('green')
    Color.print_blue_text('blue')
    Color.print_red_text_with_blue_bg('background')
