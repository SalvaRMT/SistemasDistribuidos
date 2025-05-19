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

    def _to_proto(self, doc):
        resp = trainer_pb2.TrainerResponse(
            id=str(doc["_id"]),
            name=doc["name"],
            age=doc["age"]
        )
        ts = Timestamp()
        ts.FromDatetime(doc["birthdate"])
        resp.birthdate.CopyFrom(ts)
        for m in doc.get("medals", []):
            resp.medals.add(region=m["region"], type=m["type"])
        return resp

    def get(self, id_):
        doc = self.collection.find_one({"_id": ObjectId(id_)})
        return self._to_proto(doc) if doc else None

    def get_by_name(self, name):
        doc = self.collection.find_one({"name": name})
        return self._to_proto(doc) if doc else None

    def get_all(self):
        return [self._to_proto(d) for d in self.collection.find()]

    def create(self, proto_req):
        new = {
            "name": proto_req.name,
            "age": proto_req.age,
            "birthdate": proto_req.birthdate.ToDatetime(),
            "medals": [{"region": m.region, "type": m.type} for m in proto_req.medals],
        }
        result = self.collection.insert_one(new)
        new["_id"] = result.inserted_id
        return self._to_proto(new)

    def update(self, id_, proto_req):
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
        result = self.collection.delete_one({"_id": ObjectId(id_)})
        return result.deleted_count > 0
