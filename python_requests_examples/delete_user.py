import subprocess
import sys

try:
    import requests
except ImportError:
    print("Installing requests...")
    subprocess.check_call([sys.executable, "-m", "pip", "install", "requests"])
    import requests

user_id = "f2769b3b-745c-4c58-ab79-cc7a71c0397f"

url = f"http://localhost:5000/api/users/{user_id}"
headers = {
    "X-Api-Key": "testApiKey"
}

response = requests.delete(url, headers=headers)

print("Status code:", response.status_code)
if response.status_code == 204:
    print("User deleted successfully")
else:
    try:
        print(response.json())
    except ValueError:
        print(response.text)
