import grpc
import trainer_pb2
import trainer_pb2_grpc
from concurrent import futures
import time
import datetime
import uuid
from google.protobuf.timestamp_pb2 import Timestamp

class TrainerServiceServicer(trainer_pb2_grpc.TrainerServiceServicer):
    """Implementación del servicio TrainerService."""
    
    def GetTrainer(self, request, context):
        """
        Implementación del método GetTrainer que retorna un entrenador
        basado en el ID proporcionado.
        """
        print(f"Solicitud recibida para GetTrainer con ID: {request.id}")
        
        # Crear timestamp para la fecha de nacimiento y creación
        birth_timestamp = Timestamp()
        birth_timestamp.FromDatetime(datetime.datetime.utcnow())
        
        created_timestamp = Timestamp()
        created_timestamp.FromDatetime(datetime.datetime.utcnow())
        
        # Crear respuesta con datos estáticos (hardcodeados)
        response = trainer_pb2.TrainerResponse(
            id=str(uuid.uuid4()),
            name="Salvador Rojas",
            age=21,
            birthdate=birth_timestamp,
            created_at=created_timestamp
        )
        
        # Agregar medallas
        gold_medal = trainer_pb2.Medals(region="MX", type=trainer_pb2.MedalType.GOLD)
        silver_medal = trainer_pb2.Medals(region="MX", type=trainer_pb2.MedalType.SILVER)
        response.medals.extend([gold_medal, silver_medal])
        
        return response
    
    def GetTrainerByName(self, request, context):
        """
        Implementación del método GetTrainerByName que retorna un entrenador
        basado en el ID proporcionado (aunque debería ser por nombre según la intención).
        """
        print(f"Solicitud recibida para GetTrainerByName con ID: {request.id}")
        
        # Para mantener la equivalencia con la implementación C#,
        # usamos la misma lógica que en GetTrainer
        # En una implementación real, buscaríamos por nombre
        
        # Crear timestamp para la fecha de nacimiento y creación
        birth_timestamp = Timestamp()
        birth_timestamp.FromDatetime(datetime.datetime.utcnow())
        
        created_timestamp = Timestamp()
        created_timestamp.FromDatetime(datetime.datetime.utcnow())
        
        # Crear respuesta con datos estáticos (hardcodeados)
        response = trainer_pb2.TrainerResponse(
            id=str(uuid.uuid4()),
            name="Salvador Rojas",
            age=21,
            birthdate=birth_timestamp,
            created_at=created_timestamp
        )
        
        # Agregar medallas
        gold_medal = trainer_pb2.Medals(region="MX", type=trainer_pb2.MedalType.GOLD)
        silver_medal = trainer_pb2.Medals(region="MX", type=trainer_pb2.MedalType.SILVER)
        response.medals.extend([gold_medal, silver_medal])
        
        return response

def serve():
    """Inicia el servidor gRPC."""
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    trainer_pb2_grpc.add_TrainerServiceServicer_to_server(TrainerServiceServicer(), server)
    
    # Escuchar en el puerto 50051
    server.add_insecure_port('[::]:50051')
    server.start()
    
    print("Servidor gRPC iniciado. Escuchando en el puerto 50051...")
    
    try:
        # Mantener el servidor en ejecución
        while True:
            time.sleep(86400)  # Un día en segundos
    except KeyboardInterrupt:
        server.stop(0)
        print("Servidor detenido.")

if __name__ == '__main__':
    serve()