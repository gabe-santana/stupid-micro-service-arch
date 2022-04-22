import logging

import azure.functions as func
from services.mongo import mongo_service

def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')

    post = req.get_json()
    ms = mongo_service()
    ms.insert_post(post)

    return func.HttpResponse("Post created")


        
