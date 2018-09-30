from flask import Flask, request, render_template
from flask_sqlalchemy import SQLAlchemy
import os
from datetime import datetime
import json

basedir = os.path.abspath(os.path.dirname(__file__))

app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///' + os.path.join(basedir, 'ex.db')
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
db = SQLAlchemy(app)

class User(db.Model):
	#__tablename__ = 'user'
	id = db.Column(db.Integer, primary_key=True)
	username=db.Column(db.String(32), nullable=False)

class Acceleration(db.Model):
	#__tablename__ = 'accelerations'
	id = db.Column(db.Integer, primary_key=True)
	accelTime = db.Column(db.Date)
	accelY =db.Column(db.Float)

	def __repr__(self):
		return f'<Accceleration {self.accelTime} {self.accelY}>'

@app.route('/', methods=['GET','POST'])
def addAcceleration():
	if request.method == 'POST':
		data = request.get_json()
		accelData = data['accelData']
		temp = None
		for a in accelData:
			temp = Acceleration(accelTime = datetime.fromtimestamp(a['time']), accelY = a['accelY'])
			db.session.add(temp)
			db.session.commit()
		return str(temp)

@app.route("/show", methods=['GET'])
def showAccelerations():
	queryres = Acceleration.query.all()
	for a in queryres:
		print(a.accelY)
	return render_template('Accels.html', accelerations = queryres)