# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: PlayerHeros.proto

from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import descriptor_pb2
# @@protoc_insertion_point(imports)




DESCRIPTOR = _descriptor.FileDescriptor(
  name='PlayerHeros.proto',
  package='',
  serialized_pb='\n\x11PlayerHeros.proto\",\n\x0bPlayerHeros\x12\x0e\n\x06UserId\x18\x01 \x02(\x05\x12\r\n\x05Heros\x18\x02 \x02(\t')




_PLAYERHEROS = _descriptor.Descriptor(
  name='PlayerHeros',
  full_name='PlayerHeros',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='UserId', full_name='PlayerHeros.UserId', index=0,
      number=1, type=5, cpp_type=1, label=2,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      options=None),
    _descriptor.FieldDescriptor(
      name='Heros', full_name='PlayerHeros.Heros', index=1,
      number=2, type=9, cpp_type=9, label=2,
      has_default_value=False, default_value=unicode("", "utf-8"),
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
  serialized_start=21,
  serialized_end=65,
)

DESCRIPTOR.message_types_by_name['PlayerHeros'] = _PLAYERHEROS

class PlayerHeros(_message.Message):
  __metaclass__ = _reflection.GeneratedProtocolMessageType
  DESCRIPTOR = _PLAYERHEROS

  # @@protoc_insertion_point(class_scope:PlayerHeros)


# @@protoc_insertion_point(module_scope)