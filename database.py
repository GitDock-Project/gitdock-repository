from sqlalchemy import create_engine
from urllib.parse import quote_plus

def get_connection():
    password = quote_plus("maryam@123")

    engine = create_engine(
        f"mysql+pymysql://root:{password}@localhost/ai_reminder1"
    )

    return engine