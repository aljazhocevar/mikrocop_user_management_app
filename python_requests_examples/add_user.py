import subprocess
import sys

try:
    import requests
except ImportError:
    print("installing requests")
    subprocess.check_call([sys.executable, "-m", "pip", "install", "requests"])
    import requests


url = "http://localhost:5000/api/users"
headers = {
    "Content-Type": "application/json",
    "X-Api-Key": "testApiKey"
}
data = {
    "userName": "testuser",
    "fullName": "Test User",
    "email": "test@example.com",
    "mobile": "+38640123456",
    "language": "sl",
    "culture": "sl-SI",
    "password": "mySecret123"
}

response = requests.post(url, json=data, headers=headers)

print(response.status_code)
try:
    print(response.json())
except ValueError:
    print(response.text)
