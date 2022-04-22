import json
import logging

import azure.functions as func
from services.mongo import mongo_service

def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')
    _id = req.params.get('id')

    ms = mongo_service()
    ms.delete_post(_id)

    return func.HttpResponse("Post deleted")


        
