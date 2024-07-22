# gRPC
grpc communicate using a binary string its much more efficient than using json or XML
grpc stant for remote percedure call 
At first we have a .proto file that define a contract between server and client.
we have : "syntax = "proto3";" It means that we use the latest version of prototiles 
message is a kind of model in csharp that has  : 
message Person {
  string name = 1;
  int32 id = 2;
  bool has_ponycopter = 3;
}

In gRPC (gRPC Remote Procedure Call), a message is a fundamental component of the protocol and is used to define the structure of the data being exchanged between a client and a server. Messages are defined using Protocol Buffers (protobuf), which is a language-agnostic binary serialization format developed by Google.

service Greeter {
  // Sends a greeting
  rpc SayHello (HelloRequest) returns (HelloReply);
}
we are gonna take in one type and return another type . in this example : we say "hey remote server say hello "
