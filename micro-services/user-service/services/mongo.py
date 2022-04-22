from pymongo import MongoClient
from bson.json_util import dumps
from bson.objectid import ObjectId

class mongo_service:
    def __init__(self):
        self.client = MongoClient('mongodb://host.docker.internal:27017/',
        username='root',
        password='root')
        
        self.db = self.client['user_service']
        self.collection = self.db['users']

    def insert_user(self, user):
        self.collection.insert_one(user)

    def update_user(self, _id, user):
        self.collection.update_one({"_id": ObjectId(_id)}, {"$set": user})

    def get_users(self):
        cursor = self.collection.find()
        return dumps(cursor)
    
    def delete_user(self, _id):
        self.collection.delete_one({"_id": ObjectId(_id)})