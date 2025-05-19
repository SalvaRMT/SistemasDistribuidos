# Dockerfile
FROM python:3.11-slim

WORKDIR /app

# Instala dependencias
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Copia el proto y genera los stubs DIRECTO en /
COPY protos/trainer.proto protos/
RUN python -m grpc_tools.protoc \
    --proto_path=protos \
    --python_out=. \
    --grpc_python_out=. \
    protos/trainer.proto

# Copia código de la app
COPY repository.py server.py ./

EXPOSE 50051
CMD ["python", "server.py"]
