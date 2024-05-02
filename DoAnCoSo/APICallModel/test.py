import requests

url = "http://localhost:8000/predict"
payload = 'text'
response = requests.post(url, json=payload)
print(response.json())