# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: 1000_LCLoginServer.proto

from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import descriptor_pb2
# @@protoc_insertion_point(imports)




DESCRIPTOR = _descriptor.FileDescriptor(
  name='1000_LCLoginServer.proto',
  package='',
  serialized_pb='\n\x18\x31\x30\x30\x30_LCLoginServer.proto\"7\n\rLCLoginServer\x12\x12\n\nAuthorized\x18\x01 \x02(\x08\x12\x12\n\nNewAccount\x18\x02 \x02(\x08')




_LCLOGINSERVER = _descriptor.Descriptor(
  name='LCLoginServer',
  full_name='LCLoginServer',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='Authorized', full_name='LCLoginServer.Authorized', index=0,
      number=1, type=8, cpp_type=7, label=2,
      has_default_value=False, default_value=False,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='NewAccount', full_name='LCLoginServer.NewAccount', index=1,
      number=2, type=8, cpp_type=7, label=2,
      has_default_value=False, default_value=False,
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
  serialized_start=28,
  serialized_end=83,
)

DESCRIPTOR.message_types_by_name['LCLoginServer'] = _LCLOGINSERVER

class LCLoginServer(_message.Message):
  __metaclass__ = _reflection.GeneratedProtocolMessageType
  DESCRIPTOR = _LCLOGINSERVER

  # @@protoc_insertion_point(class_scope:LCLoginServer)


# @@protoc_insertion_point(module_scope)
