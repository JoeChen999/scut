# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: PBInt64.proto

from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import descriptor_pb2
# @@protoc_insertion_point(imports)




DESCRIPTOR = _descriptor.FileDescriptor(
  name='PBInt64.proto',
  package='',
  serialized_pb='\n\rPBInt64.proto\"$\n\x07PBInt64\x12\x0b\n\x03Low\x18\x01 \x02(\x05\x12\x0c\n\x04High\x18\x02 \x02(\x05')




_PBINT64 = _descriptor.Descriptor(
  name='PBInt64',
  full_name='PBInt64',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='Low', full_name='PBInt64.Low', index=0,
      number=1, type=5, cpp_type=1, label=2,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='High', full_name='PBInt64.High', index=1,
      number=2, type=5, cpp_type=1, label=2,
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
  serialized_start=17,
  serialized_end=53,
)

DESCRIPTOR.message_types_by_name['PBInt64'] = _PBINT64

class PBInt64(_message.Message):
  __metaclass__ = _reflection.GeneratedProtocolMessageType
  DESCRIPTOR = _PBINT64

  # @@protoc_insertion_point(class_scope:PBInt64)


# @@protoc_insertion_point(module_scope)
