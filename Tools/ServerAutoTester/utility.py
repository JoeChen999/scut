from protocols import PBInt64_pb2
from struct import pack, unpack


def pb_int64_to_long(pb):
    assert isinstance(pb, PBInt64_pb2.PBInt64)
    return unpack('q', pack('ii', pb.Low, pb.High))[0]


def long_to_pb_int64(num):
    assert isinstance(num, long)
    pb = PBInt64_pb2.PBInt64()
    mystr = pack('q', num)
    pb.Low = unpack('i', mystr[0:4])[0]
    pb.High = unpack('i', mystr[4:8])[0]
    return pb

if __name__ == '__main__':
    for i in (-1L, 0L, 1L, 9999999999999999L, -9999999999999999L):
        pb = long_to_pb_int64(i)
        a = pb_int64_to_long(pb)
        assert a == i
