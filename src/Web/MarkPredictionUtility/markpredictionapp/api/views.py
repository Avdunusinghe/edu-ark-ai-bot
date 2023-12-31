from django.shortcuts import render
from django.views.decorators.csrf import csrf_exempt
# Create your views here.
from django.http import HttpResponse
import pickle
import json
def index(request):
    return HttpResponse("Hello, world. You're at the polls index.")

# Define a function to make predictions using the saved model

def predict_marks(request):
    # Load the trained model from the pickle file
    with open("api/sinhala-model.pkl", "rb") as file:
        model = pickle.load(file)

    # Create a new input data point for prediction
    new_input_data = [[95, 78, 96, 63, 78, 1200, 80]]

    # Use the loaded model to make predictions on the new data
    pred_new = model.predict(new_input_data)

    # Return the prediction as an HTTP response
    return HttpResponse(pred_new)

# Define a function to make predictions using the saved model
@csrf_exempt
def student_performance_analyze(request):
    if request.method == 'POST':
        received_data = json.loads(request.body)
        # Load the trained model from the pickle file
        with open("api/sinhala-model-dev.pkl", "rb") as file:
            model = pickle.load(file)

        # Assuming that the POST data contains the input values as a list of integers
        input_data = received_data['input_data']
       
        # Convert input data to a list of integers
        new_input_data = [int(val) for val in input_data]

      

        # Reshape the data if necessary, depending on  model 2D array
        new_input_data = [new_input_data] 
        
        # Use the loaded model to make predictions on the new data
        pred_new = model.predict(new_input_data)

        # Return the prediction as an HTTP response
        return HttpResponse(pred_new)
    else:
        return HttpResponse("Invalid request method. Use POST.")