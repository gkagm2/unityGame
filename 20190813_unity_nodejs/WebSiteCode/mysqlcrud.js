var express = require('express'); // express 모듈 로드
var bodyParser = require("body-parser"); // 바디파서 모듈로드
var mysqlConfig = require("./mysqlConfig.js"); // mysql 설정파일 로드
var app = express(); // express 앱 생성
var mysql = require("mysql"); // mysql모듈 로드
var conn = mysql.createConnection(mysqlConfig); // mysql 접속생성

app.use(bodyParser.json()); // 바디파서의 json() 사용
app.use(bodyParser.urlencoded({ extended: true })); // 바디파서의 url인코딩 사용.
console.log("실행~~");
// 주소/new로 들어오는 경우의 처리...

// Insert Query
app.post('/new', function(req, res, err){
	if(req !== null){ // request(요청)이 null이 아니라면...
		console.log("1실행~~");
		if(parseInt(req.body.Name) !== null){ // 요청은 데이터의 Name이 null이 아니라면...
			console.log("2실행~~");
			var newQuery = "INSERT INTO UserInfoTable( Name, GId, Gold, Score, Level) VALUES(?, ?, ?, ?, ?)";
			var param = [ Name = req.body.Name, GId = req.body.GId, Gold = req.body.Gold, Score = req.body.Score, Level = req.body.Level ];
			console.log("3실행~~");
			conn.query(newQuery, param, function(err, row, fields){
				if(!err){
					
					console.log("Data INSERT Success!!"); // insert 성공 시 메세지
					var resultQuery = "SELECT LAST_INSERT_ID();";
					conn.query(resultQuery, function(err, row, fields){
						if(!err){
							res.send(row[0]);
						} else {
							console.log(err);
						}
					});
				} else {
					console.log("error : " + err);
					res.send(err);
				}
			});
		} else {
			res.send(err);
		}
	} else {
		res.send(err);
	}
});

app.post('/checkId', function(req, res, err) {
	if(req !== null){
		var checkIdQuery = "SELECT UserInfoTable.Key FROM UserInfoTable WHERE UserInfoTable.Name = ?";
		var param = [Name = req.body.Name];
		conn.query(checkIdQuery, param, function(err, row, fields){
			if(!err){
				if(row.length === 0){
					res.send("NotExist");
				} else {
					res.send(row);
				}
			} else {
				res.send(err);
			}
		});
	} else {
		res.send(err);
	}
});


// Select Query
app.post('/select', function(req, res, err){
	console.log("[[1]]");
	if(req !== null) { // request(요청)이 null이 아니라면...
		console.log("[[2]]");
		var selectQuery = "SELECT * FROM UserInfoTable WHERE UserInfoTable.Key = ?";
		var param = [Key = req.body.Key];
		console.log("[[3]]");
		conn.query(selectQuery, param, function(err, row, fields){
			console.log("[[4]]");
			if(!err){
				console.log("[[5]]");
				if(row.length === 0){
					console.log("Key is Null");
					res.send("Key is Null")
				} else {
					console.log("select success[[6]]");
					res.send(row);
				}
			} else {
				res.send(err);
			}
		});
	} else {
		res.send(err);
	}
});

// Update Query
app.post('/update', function(req, res, err){
	if(req !== null){
		var updateQuery = "UPDATE UserInfoTable SET Gold = ?, Score = ? WHERE UserInfoTable.Key = ?";
		var param = [ Gold = req.body.Gold, Score = req.body.Score, Key = req.body.Key];
		conn.query(updateQuery, param, function(err, row, fields){
			if(!err){
				res.send("Update Complete");
			} else {
				res.send(err);
			}
		});
	} else {
		res.send(err);
	}
});

// Delete Query
app.post('/delete', function(req, res, err){
	console.log("delete 1");
	if(req !== null){
		console.log("delete 2");
		var deleteQuery = "DELETE FROM UserInfoTable WHERE UserInfoTable.Key = ?";
		var param = [Key = req.body.Key];
		console.log("delete 3");
		conn.query(deleteQuery, param, function(err, row, fields){
			console.log("delete 4");
			if(!err){
				console.log("delete 5");
				if(row.length === 0){
					console.log("Key is Null");
					res.send("Key is Null");
				} else {
					console.log("delete success");
					res.send(row);
				}
			} else {
				res.send(err);
			}
		});
	} else {
		res.send(err);
	}
});

app.listen(3001);
console.log('CRUD listening on port 3001');