import sqlite3
conn = sqlite3.connet('ex.db')

c = conn.curson()
c.execute('''CREATE TABLE user 
			 (id INTEGER PRIMARY KEY ASC, username varchar(32) NOT NULL''')
c.execute('''CREATE TABLE accelerations  
			 (id INTEGER PRIMARY KEY ASC, accelY real, accelTime Datetime''')

conn.commit()
conn.close()
