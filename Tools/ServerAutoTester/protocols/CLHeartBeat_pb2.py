# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: 1001_CLHeartBeat.proto

from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import descriptor_pb2
# @@protoc_insertion_point(imports)


import PBInt64_pb2


DESCRIPTOR = _descriptor.FileDescriptor(
  name='1001_CLHeartBeat.proto',
  package='',
  serialized_pb='\n\x16\x31\x30\x30\x31_CLHeartBeat.proto\x1a\rPBInt64.proto\"+\n\x0b\x43LHeartBeat\x12\x1c\n\nClientTime\x18\x01 \x02(\x0b\x32\x08.PBInt64')




_CLHEARTBEAT = _descriptor.Descriptor(
  name='CLHeartBeat',
  full_name='CLHeartBeat',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='ClientTime', full_name='CLHeartBeat.ClientTime', index=0,
      number=1, type=11, cpp_type=10, label=2,
      has_default_value=False, default_value=None,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  options=None,
  is_extendable=False,
  extension_ranges=[],
  serialized_start=41,
  serialized_end=84,
)

_CLHEARTBEAT.fields_by_name['ClientTime'].message_type = PBInt64_pb2._PBINT64
DESCRIPTOR.message_types_by_name['CLHeartBeat'] = _CLHEARTBEAT

class CLHeartBeat(_message.Message):
  __metaclass__ = _reflection.GeneratedProtocolMessageType
  DESCRIPTOR = _CLHEARTBEAT

  # @@protoc_insertion_point(class_scope:CLHeartBeat)


# @@protoc_insertion_point(module_scope)