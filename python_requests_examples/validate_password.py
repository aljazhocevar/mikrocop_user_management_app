import subprocess
import sys

try:
    import requests
except ImportError:
    print("installing requests...")
    subprocess.check_call([sys.executable, "-m", "pip", "install", "requests"])
    import requests

user_id = "99ad21c2-2ea9-4e37-b5c8-3db9494d7683"
base_url = "http://localhost:5000/api/users"
headers = {
    "X-Api-Key": "testApiKey",
    "Content-Type": "application/json"
}

#password_to_test = "P@ssw0rd" 
password_to_test = "mySecret123"
payload = {"password": password_to_test}

validate_resp = requests.post(f"{base_url}/{user_id}/validate-password",
                              headers=headers, json=payload)

print("Validate password status code:", validate_resp.status_code)
try:
    print(validate_resp.json())
except ValueError:
    print(validate_resp.text)
