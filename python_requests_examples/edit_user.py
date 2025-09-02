import subprocess
import sys

try:
    import requests
except ImportError:
    print("installing requests")
    subprocess.check_call([sys.executable, "-m", "pip", "install", "requests"])
    import requests

user_id = "858FE20C-3B4F-4184-A216-641198EB3B76"

url = f"http://localhost:5000/api/users/{user_id}"
headers = {
    "Content-Type": "application/json",
    "X-Api-Key": "testApiKey"
}

update_data = {
    "fullName": "new user fullName",
    "email": "updated@example.com",
    # "password": "newPassword123" 
}

response = requests.put(url, json=update_data, headers=headers)

print(response.status_code)
if response.status_code != 204:  # 204 = No Content
    try:
        print(response.json())
    except ValueError:
        print(response.text)
