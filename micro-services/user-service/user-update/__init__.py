import logging

import azure.functions as func
from services.mongo import mongo_service

def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function processed a request.')

    user = req.get_json()
    user_id = req.params.get('id')

    ms = mongo_service()
    ms.update_user(user_id, user)

    return func.HttpResponse("Post updated", status_code=201)


        
