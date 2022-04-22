from pymongo import MongoClient
from bson.json_util import dumps
from bson.objectid import ObjectId

class mongo_service:
    def __init__(self):
        self.client = MongoClient('mongodb://host.docker.internal:27017/',
        username='root',
        password='root')
        
        self.db = self.client['post_service']
        self.collection = self.db['posts']

    def insert_post(self, post):
        self.collection.insert_one(post)

    def update_post(self, _id, post):
        self.collection.update_one({"_id": ObjectId(_id)}, {"$set": post})

    def get_posts(self):
        cursor = self.collection.find()
        return dumps(cursor)
    
    def delete_post(self, _id):
        self.collection.delete_one({"_id": ObjectId(_id)})