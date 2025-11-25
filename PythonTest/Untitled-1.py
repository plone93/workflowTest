from fastapi import FastAPI

app = FastAPI()

@app.get("/testls")
def read_root():
    return {"message": "Hello FastAPI!"}
