import subprocess
import sys

try:
    import requests
except ImportError:
    print("Installing requests...")
    subprocess.check_call([sys.executable, "-m", "pip", "install", "requests"])
    import requests

user_id = "17a10b6b-a6bd-48d2-a109-28e89e0e33e8"

url = f"http://localhost:5000/api/users/{user_id}"
headers = {
    "X-Api-Key": "testApiKey"
}

response = requests.get(url, headers=headers)

print("Status code:", response.status_code)
try:
    print(response.json())
except ValueError:
    print(response.text)
