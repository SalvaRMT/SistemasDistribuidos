from concurrent import futures
import grpc
import os

from google.protobuf import empty_pb2
import trainer_pb2
import trainer_pb2_grpc

from repository import TrainerRepository

class TrainerService(trainer_pb2_grpc.TrainerServiceServicer):
    def __init__(self):
        self.repo = TrainerRepository()

    def GetTrainer(self, request, context):
        proto = self.repo.get(request.id)
        if not proto:
            context.abort(grpc.StatusCode.NOT_FOUND, "Trainer not found")
        return proto

    def GetTrainerByName(self, request, context):
        proto = self.repo.get_by_name(request.name)
        if not proto:
            context.abort(grpc.StatusCode.NOT_FOUND, "Trainer not found")
        return proto

    def GetAllTrainers(self, request, context):
        all_trs = self.repo.get_all()
        return trainer_pb2.TrainerListResponse(trainers=all_trs)

    def CreateTrainer(self, request, context):
        return self.repo.create(request)

    def UpdateTrainer(self, request, context):
        updated = self.repo.update(request.id, request)
        if not updated:
            context.abort(grpc.StatusCode.NOT_FOUND, "Trainer not found")
        return updated

    def DeleteTrainer(self, request, context):
        ok = self.repo.delete(request.id)
        if not ok:
            context.abort(grpc.StatusCode.NOT_FOUND, "Trainer not found")
        return empty_pb2.Empty()

def serve():
    port = os.getenv("GRPC_PORT", "50051")
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    trainer_pb2_grpc.add_TrainerServiceServicer_to_server(TrainerService(), server)
    server.add_insecure_port(f"[::]:{port}")
    server.start()
    print(f"gRPC server listening on port {port}")
    server.wait_for_termination()

if __name__ == "__main__":
    serve()
