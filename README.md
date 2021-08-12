# Task 1
only task 1 using the Agility pack has been done. the better approach is that we using the AI based on ML and Onnx.
it needs some exceptions to be thrown manually in case of invalid html and for a patern which is null or invlid.
also the whole parsing class can be dynamic by storing the properties and apperns into DB.
this gives a better versioning too.
needs more unit tests.

## Run
docker-compose -f docker-compose.yml up




## Grpc
for grpc checking, you can use below command. fix proto **include folder** as your clones directory.
`grpcui -proto dataExtractor.proto -import-path "C:\projects\Test\fynd\API\02.Services\FyndParser\Fynd.Parser.Endpoint\Grpc\Proto" -plaintext localhost:5000`
we can add grpc reflectin too.

## Rest
the end point is http://localhost:5001/api/DataExtractor/Extract
according integration tests.
