from fastapi.testclient import TestClient
from src.main import app

client = TestClient(app)

def test_hello():
    response = client.get("/hello")
    assert response.status_code == 200
    assert response.json() == {"message": "Hello World"}

def test_add():
    response = client.post("/add", params={"a": 3, "b": 4})
    assert response.status_code == 200
    assert response.json() == {"result": 7}

def test_protected_route_success():
    # 1) 먼저 토큰 발급
    token_res = client.post(
        "/token",
        data={"username": "admin", "password": "1234"},
    )
    token = token_res.json()["access_token"]

    # 2) Authorization 헤더 포함하여 /protected 접근
    protected_res = client.get(
        "/protected",
        headers={"Authorization": f"Bearer {token}"}
    )

    assert protected_res.status_code == 200
    assert protected_res.json() == {"message": "Hello, admin!"}


def test_protected_route_fail():
    # 잘못된 토큰
    response = client.get(
        "/protected",
        headers={"Authorization": "Bearer INVALID.TOKEN.VALUE"}
    )
    assert response.status_code == 401