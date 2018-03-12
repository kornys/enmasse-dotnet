# enmasse-dotnet
test dotnet client connection to enmasse

### Compile
```
cd enmasse-dotnet
dotnet build
```

### Run
```
dotnet bin/Debug/netcoreapp2.0/enmasse-dotnet.dll amqps://user:password@<enmasse-messaging -route>:443 queue-name
```
