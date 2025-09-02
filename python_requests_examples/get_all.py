import subprocess
import sys

try:
    import requests
except ImportError:
    print("Installing requests...")
    subprocess.check_call([sys.executable, "-m", "pip", "install", "requests"])
    import requests

url = f"http://localhost:5000/api/users/"
headers = {
    "X-Api-Key": "testApiKey"
}

response = requests.get(url, headers=headers)

print("Status code:", response.status_code)
try:
    print(response.json())
except ValueError:
    print(response.text)
