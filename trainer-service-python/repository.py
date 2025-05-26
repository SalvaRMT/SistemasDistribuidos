import os
from pymongo import MongoClient
from bson.objectid import ObjectId
from google.protobuf.timestamp_pb2 import Timestamp
import trainer_pb2

class TrainerRepository:
    def __init__(self):
        conn = os.getenv("MONGODB_URI", "mongodb://localhost:27017")
        dbname = os.getenv("MONGODB_DB", "TrainerDb")
        coll = os.getenv("MONGODB_COLL", "Trainers")
        client = MongoClient(conn)
        self.collection = client[dbname][coll]

    def _to_timestamp(self, dt):
        ts = Timestamp()
        ts.FromDatetime(dt)
        return ts

    def _to_proto(self, doc):
        return trainer_pb2.TrainerResponse(
            id=str(doc.get("_id")),
            name=doc.get("name", ""),
            age=doc.get("age", 0),
            birthdate=self._to_timestamp(doc.get("birthdate")),
            medals=[
                trainer_pb2.Medals(region=m["region"], type=m["type"])
                for m in doc.get("medals", [])
            ]
        )

    def create(self, proto_req):
        """Inserta un nuevo Trainer y devuelve el proto."""
        doc = {
            "name": proto_req.name,
            "age": proto_req.age,
            "birthdate": proto_req.birthdate.ToDatetime(),
            "medals": [{"region": m.region, "type": m.type} for m in proto_req.medals],
        }
        result = self.collection.insert_one(doc)
        # Inyectamos el _id para convertirlo a proto
        doc["_id"] = result.inserted_id
        return self._to_proto(doc)

    def get(self, id_):
        """Busca un Trainer por su ObjectId."""
        doc = self.collection.find_one({"_id": ObjectId(id_)})
        if not doc:
            return None
        return self._to_proto(doc)

    def get_by_name(self, name):
        """Busca un Trainer por nombre (case-insensitive, coincidente exacto)."""
        doc = self.collection.find_one({
            "name": {"$regex": f"^{name}$", "$options": "i"}
        })
        if not doc:
            return None
        return self._to_proto(doc)

    def get_all(self):
        """Devuelve todos los Trainers como lista de protos."""
        return [self._to_proto(doc) for doc in self.collection.find()]

    def update(self, id_, proto_req):
        """Reemplaza el Trainer con los datos del proto y devuelve el proto actualizado."""
        updated = {
            "name": proto_req.name,
            "age": proto_req.age,
            "birthdate": proto_req.birthdate.ToDatetime(),
            "medals": [{"region": m.region, "type": m.type} for m in proto_req.medals],
        }
        result = self.collection.replace_one({"_id": ObjectId(id_)}, updated)
        if result.matched_count == 0:
            return None
        updated["_id"] = ObjectId(id_)
        return self._to_proto(updated)

    def delete(self, id_):
        """Borra el Trainer y devuelve True si existía."""
        result = self.collection.delete_one({"_id": ObjectId(id_)})
        return result.deleted_count > 0
