from fastapi import FastAPI, HTTPException, Depends
from fastapi.security import OAuth2PasswordBearer, OAuth2PasswordRequestForm
from jose import jwt, JWTError
from datetime import datetime, timedelta

app = FastAPI()

SECRET_KEY = "TEST_SECRET_KEY_EXAMPLE"   # 테스트용
ALGORITHM = "HS256"


@app.get("/hello")
def read_root():
    return {"message": "Hello World"}

@app.post("/add")
def add_numbers(a: int, b: int):
    return {"result": a + b}

oauth2_scheme = OAuth2PasswordBearer(tokenUrl="token")

# ⏳ JWT 생성 함수
def create_access_token(data: dict, expires_minutes: int = 30):
    to_encode = data.copy()
    expire = datetime.utcnow() + timedelta(minutes=expires_minutes)
    to_encode.update({"exp": expire})
    return jwt.encode(to_encode, SECRET_KEY, algorithm=ALGORITHM)

# 🔐 보호된 엔드포인트 인증 함수
def verify_token(token: str = Depends(oauth2_scheme)):
    try:
        payload = jwt.decode(token, SECRET_KEY, algorithms=[ALGORITHM])
        return payload
    except JWTError:
        raise HTTPException(status_code=401, detail="Invalid token")


@app.post("/token")
def login(form_data: OAuth2PasswordRequestForm = Depends()):
    # 예제: username=admin, password=1234
    if form_data.username != "admin" or form_data.password != "1234":
        raise HTTPException(status_code=401, detail="Incorrect credentials")

    access_token = create_access_token({"sub": form_data.username})
    return {"access_token": access_token, "token_type": "bearer"}

@app.get("/protected")
def protected_route(payload: dict = Depends(verify_token)):
    return {"message": f"Hello, {payload['sub']}!"}