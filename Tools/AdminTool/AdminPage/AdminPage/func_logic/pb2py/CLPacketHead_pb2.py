# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: CLPacketHead.proto

from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import descriptor_pb2
# @@protoc_insertion_point(imports)




DESCRIPTOR = _descriptor.FileDescriptor(
  name='CLPacketHead.proto',
  package='',
  serialized_pb='\n\x12\x43LPacketHead.proto\"R\n\x0c\x43LPacketHead\x12\r\n\x05MsgId\x18\x01 \x02(\x05\x12\x10\n\x08\x41\x63tionId\x18\x02 \x02(\x05\x12\x11\n\tSessionId\x18\x03 \x02(\t\x12\x0e\n\x06UserId\x18\x04 \x02(\x05')




_CLPACKETHEAD = _descriptor.Descriptor(
  name='CLPacketHead',
  full_name='CLPacketHead',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='MsgId', full_name='CLPacketHead.MsgId', index=0,
      number=1, type=5, cpp_type=1, label=2,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='ActionId', full_name='CLPacketHead.ActionId', index=1,
      number=2, type=5, cpp_type=1, label=2,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='SessionId', full_name='CLPacketHead.SessionId', index=2,
      number=3, type=9, cpp_type=9, label=2,
      has_default_value=False, default_value=unicode("", "utf-8"),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='UserId', full_name='CLPacketHead.UserId', index=3,
      number=4, type=5, cpp_type=1, label=2,
      has_default_value=False, default_value=0,
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
  serialized_start=22,
  serialized_end=104,
)

DESCRIPTOR.message_types_by_name['CLPacketHead'] = _CLPACKETHEAD

class CLPacketHead(_message.Message):
  __metaclass__ = _reflection.GeneratedProtocolMessageType
  DESCRIPTOR = _CLPACKETHEAD

  # @@protoc_insertion_point(class_scope:CLPacketHead)


# @@protoc_insertion_point(module_scope)
