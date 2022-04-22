import json
import logging

import azure.functions as func
from services.mongo import mongo_service

def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')

    ms = mongo_service()
    results = ms.get_posts()

    return func.HttpResponse(results, headers={
        'Content-Type': 'application/json'
    })


        
