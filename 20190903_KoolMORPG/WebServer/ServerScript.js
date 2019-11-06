var express = require('express'); // express 모듈 로드
var bodyParser = require("body-parser"); // 바디파서 모듈로드
var mysqlConfig = require("./mysqlConfig.js"); // mysql 설정파일 로드
var app = express(); // express 앱 생성
var mysql = require("mysql"); // mysql모듈 로드
var conn = mysql.createConnection(mysqlConfig); // mysql 접속생성

app.use(bodyParser.json()); // 바디파서의 json() 사용
app.use(bodyParser.urlencoded({ extended: true })); // 바디파서의 url인코딩 사용.
console.log("실행~~");

//1 서버와 연결이 되는지 확인한다.
app.post('/ConnectionCheck', function(req, res, err){
	if(req !== null){
		if(req.body.CheckMessage === "connect"){
			res.send("success");
		} else {
			res.send("fail");
		}
	}
});

//2 유저의 이름이 존재하는지 체크한다.
app.post('/checkUserNameExist', function(req, res, err){
	if(req !== null){
		if(typeof(req.body.Name) != "undefined"){
			var getUserNameCountQuery = "SELECT Name FROM User WHERE Name = ?";
			var param = req.body.Name;
			conn.query(getUserNameCountQuery, param, function(err, row, fields){
				if(typeof(row[0]) != "undefined"){
					res.send("1"); //exist
				} else{
					res.send("0"); //not exist
				}
			});
		} else {
			res.send(err);
		}
	}
});

//3 새로운 유저를 만든다.
app.post('/createNewUser', function(req, res, err){
	if(req !== null){ // request가 null이 아니라면
		if(req.body.Name!==null){
			if(typeof(req.body.User_Id) != "undefined"){ // User_Id가 있는 상태에서 새로운 유저를 만든다.
				var query = "INSERT INTO User VALUES(?,?)"; // Name, User_Id
				var param =[Name = req.body.Name, User_Id = req.body.User_Id];
				conn.query(query, param, function(err, row, fields){
					if(!err){
						res.send("success");
					}else{
						res.send(err);
					}
				});
			} else{ // User_Id가 없는 상태에서 새로운 유저를 만든다.
				var query = "INSERT INTO User(Name) VALUES(?)";
				var param = [Name = req.body.Name];

				conn.query(query, param, function(err, row, fields){
					if(!err){
						res.send("success");
					}else{
						res.send(err);
					}
				});
			}
		}
	}
});

//4 유저 데이터에 GPGS ID를 추가한다.
app.post('/addUserId', function(req, res, err){
	if(req !== null) { // request가 null이 아니라면
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.User_Id) != "undefined"){
			var findQuery = "SELECT Name FROM User WHERE Name = ?";
			conn.query(findQuery,[req.body.Name], function(err, row, fields){
				if(typeof(row[0]) != "undefined"){
					var query6 = "UPDATE User SET User_Id = ? WHERE Name = ?";
					var param5 = [User_Id = req.body.User_Id, Name = req.body.Name];
					conn.query(query6, param5, function(err, row, fields){
						if(!err){
							if(row.length === 0){
								res.send("0");
							} else {
								res.send("success");	
							}
							
						}else{
							res.send("fail");
						}
					});
				} else {
					res.send("fail");
				}
			});
		} else{
			res.send(err);
		}
	}
});

//5 유저의 캐릭터가 존재하는지 체크한다.
app.post('/checkUserCharacterExist', function(req, res, err){
	if(req !== null){
		console.log("sdfaS");
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			var query = "SELECT COUNT(User_Name) FROM UserCharacter WHERE User_Name = ? AND Character_Type = ?";
			var param = [User_Name = req.body.Name, Character_Type =req.body.Character_Type];
			conn.query(query, param, function(err, row, fields){
				if(!err){
					console.log("1111");
					res.send(row)
				} else{
					console.log("2222");
					res.send(err);
				}
			});
		} else {
			res.send(err);
		}
	}
});


//6 새로운 캐릭터 데이터의 정보를 추가한다.
app.post('/addNewCharacterData', function(req, res, err){
	if(req !== null){
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			// Name, Character_Type, HP, ATK, DEF, SPD_MOV, SPD_ATK, Gold, Crystal, EXP, Level, Stamina
			var query = "INSERT INTO UserCharacter VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
			var param = [User_Name = req.body.Name,
						Character_Type = req.body.Character_Type,
						HP = req.body.HP,
						ATK = req.body.ATK,
						DEF =req.body.DEF,
						SPD_MOV = req.body.SPD_MOV,
						SPD_ATK = req.body.SPD_ATK,
						Gold = req.body.Gold,
						Crystal = req.body.Crystal,
						EXP = req.body.EXP,
						Level = req.body.Level,
						Stamina = req.body.Stamina];
			conn.query(query, param, function(err, row, fields){
				if(!err){
					res.send("success");
				} else {
					res.send(err);
				}
			});
		} else {
			res.send(err);
		}
	}
});

//7 캐릭터의 데이터를 업데이트 한다.
app.post('/updateCharacterData', function(req, res, err){
	if(req !== null){
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			var query = "UPDATE UserCharacter SET HP = ?, ATK = ?, DEF = ?, SPD_MOV = ?, SPD_ATK = ?, Gold = ?, Crystal = ?, EXP = ?, Level = ?, Stamina = ? WHERE User_Name = ? AND Character_Type = ?";
			var param = [
						HP = req.body.HP,
						ATK = req.body.ATK,
						DEF =req.body.DEF,
						SPD_MOV = req.body.SPD_MOV,
						SPD_ATK = req.body.SPD_ATK,
						Gold = req.body.Gold,
						Crystal = req.body.Crystal,
						EXP = req.body.EXP,
						Level = req.body.Level,
						Stamina = req.body.Stamina,
						User_Name = req.body.Name,
						Character_Type = req.body.Character_Type];
			conn.query(query, param, function(err, row, fields){
				if(!err){
					res.send("success");
				} else {
					res.send("fail");
				}
			});
		} else {
			res.send(err);
		}
	}
});


//8 유저의 모든 장비 아이템 정보를 불러온다.
app.post('/getEquipmentItemsOfUser', function(req, res, err){
	if(req !== null){
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			var query = "SELECT Item_Id,Name,Tier,Equipment_Type,ATK,DEF,SPD_MOV,SPD_ATK,Gold,Tendency,Reinforcement,Image_Number FROM EquipmentItem WHERE User_Name = ? AND Character_Type = ?";
			var param = [Name = req.body.Name, Character_Type = req.body.Character_Type];
			conn.query(query, param, function(err, row, fields){
				if(!err){
					if(row.length === 0){
						res.send("0");	
					} else{
						res.send(row);
					}
				} else {
					res.send("fail");
				}
			});
		} else {
			res.send("form fail");
		}
	}
});

//9 유저의 장비 아이템 하나를 추가하여 서버로 저장한다.
app.post('/saveEquipmentItemOfUser', function(req, res, err){
	if(req !== null){
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			// item_id<=생략, User_Name, Character_Type, Name, Tier, Equipment_Type, ATK, DEF, SPD_MOV, SPD_ATK, Gold, Tendency, Reinforcement, Image_Number
			var query = "INSERT INTO EquipmentItem VALUES (DEFAULT, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
			var attack = parseFloat(req.body.ATK);
			var defence = parseFloat(req.body.DEF);
			var moveSpeed = parseFloat(req.body.SPD_MOV);
			var attackSpeed = parseFloat(req.body.SPD_ATK);
			var param = [User_Name = req.body.Name, 
						 Character_Type = req.body.Character_Type, 
						 Name = req.body.Item_Name, 
						 Tier = req.body.Tier, 
						 Equipment_Type = req.body.Equipment_Type, 
						 ATK = attack, 
						 DEF = defence, 
						 SPD_MOV = moveSpeed, 
						 SPD_ATK = attackSpeed, 
						 Gold = req.body.Gold, 
						 Tendency = req.body.Tendency, 
						 Reinforcement = req.body.Reinforcement, 
						 Image_Number = req.body.Image_Number];
			conn.query(query, param, function(err, row, fields){
				if(!err) {
					res.send(row);
				}
				else {
					res.send(err);
				}
			});
		}
	}
});

//10 유저의 장비 아이템을 삭제한다.
app.post('/deleteEquipmentItemOfUser', function(req, res, err){
	if(req !== null){
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			var query = "DELETE FROM EquipmentItem WHERE Item_Id = ? AND Character_Type = ? AND User_Name = ?";
			var param = [Item_Id = req.body.Item_Id, Character_Type = req.body.Character_Type, User_Name = req.body.Name];
			conn.query(query, param, function(err, row, fields){
				if(!err) {
					res.send(row);
				} else {
					res.send(err);
				}
			});
		} else{
			res.send("fail");
		}
	}
});

//11 유저의 소비용 아이템 정보를 모두 불러온다.
app.post('/getConsumableItemsOfUser', function(req, res, err){
	if(req !== null){
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			var query = "SELECT Item_Id, Name, Type, Gold, Image_Number FROM ConsumableItem WHERE User_Name = ? AND Character_Type = ?";
			var param = [User_Name = req.body.Name, Character_Type = req.body.Character_Type];
			conn.query(query, param, function(err, row, fields){
				if(!err){
					if(row.length === 0){
						res.send("0");
					} else{
						res.send(row);	
					}
				} else {
					res.send("fail");
				}
			});
		} else {
			res.send("form fail");
		}
	}
});

//12 유저의 소비용 아이템 정보를 추가한다.
app.post('/addConsumableItemsOfUser', function(req, res, err){
	if(req != null){
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			// User_Name, Character_Type, Name, Type, Gold, Image_Number
			var query = "INSERT INTO ConsumableItem VALUES(DEFAULT, ?, ?, ?, ?, ?, ?)";
			var param = [User_Name = req.body.Name, 
						 Character_Type = req.body.Character_Type, 
						 Name = req.body.Item_Name,
						 Type = req.body.Type,
						 Gold = req.body.Gold,
						 Image_Number = req.body.Image_Number];
			conn.query(query, param, function(err, row, fields){
				if(!err){
					res.send(row);
				} else {
					res.send(err);
				}
			});
		}
	}
});

//13 유저의 소비용 아이템 정보를 삭제한다.
app.post('/deleteConsumableItemOfUser', function(req, res, err){
	if(req != null){
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			var query = "DELETE FROM ConsumableItem WHERE Item_Id = ? AND User_Name = ? AND Character_Type = ?";
			var param = [
				Item_Id = req.body.Item_Id,
				User_Name = req.body.Name,
				Character_Type = req.body.Character_Type
			];
			conn.query(query, param, function(err, row, fields){
				if(!err){
					res.send(row);
				} else {
					res.send(err);
				}
			});
		}
	}
});

//14 유저의 캐릭터 정보를 가져온다.
app.post('/getCharacterData', function(req, res, err){
	if(req != null){
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			var query = "SELECT * FROM UserCharacter WHERE User_Name = ? AND Character_Type = ?";
			var param = [User_Name = req.body.Name, Character_Type = req.body.Character_Type];
			
			conn.query(query, param, function(err, row, fields){
				if(!err){
					res.send(row);
				} else {
					res.send(err);
				}
			});
		}
	}
});

//15 유저의 현재 장비한 아이템을 불러온다.
app.post('/LoadCurrentEquippedItems', function(req, res, err){
	if(req != null){
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			var query = "SELECT Equipped_Item_Id, Equipment_Type FROM EquipmentItemInUse WHERE User_Name = ? AND Character_Type = ?";
			var param = [User_Name = req.body.Name,
						Character_Type = req.body.Character_Type];
			conn.query(query, param, function(err, row, fields){
				if(!err){
					res.send(row);
				} else {
					res.send(err);
				}
			});
		}
	}
});



//16 유저의 현재 장비한 아이템을 저장한다.
app.post('/insertCurrentEquippedItem', function(req, res, err){
	if(req != null){
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			var query = "INSERT INTO EquipmentItemInUse VALUES(?, ?, ?, ?)";
			var param = [User_Name = req.body.Name,
						Character_Type = req.body.Character_Type,
						Equipped_Item_Id = req.body.Equipped_Item_Id,
						Equipment_Type = req.body.Equipment_Type];
			conn.query(query, param, function(err, row, fields){
				if(!err){
					res.send("success");
				} else{
					res.send("fail");
				}
			});
		}
	}
});

//17 유저가 장착한 아이템을 해제한 정보를 이용하여 삭제한다.
app.post('/deleteEquippedItem', function(req, res, err){
	if(req != null){
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			var query = "DELETE FROM EquipmentItemInUse WHERE Equipment_Type = ? AND User_Name = ? AND Character_Type = ?";
			var param = [ Equipment_Type = req.body.Equipment_Type,
						  User_Name = req.body.Name,
						  Character_Type = req.body.Character_Type];
			conn.query(query, param, function(err, row, fields){
				if(!err){
					res.send("1");
				} else {
					res.send("0");
				}
			});
		}
	}
});

//18 유저의 현재 장비한 아이템이 있는지 검사한다.
app.post('/checkCurrentEquippedItem', function(req, res, err){
	if(req != null){
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			var query = "SELECT * FROM EquipmentItemInUse WHERE User_Name = ? AND Character_Type = ? AND Equipment_Type = ?";
			var param = [User_Name = req.body.Name,
						 Character_Type = req.body.Character_Type,
						 Equipment_Type = req.body.Equipment_Type];
			conn.query(query, param, function(err, row, fields){
				if(!err){
					if(row.length === 1){
						res.send("1");	
					}else{
						res.send("0");
					}
					
				} else {
					res.send(err);
				}
			});
		} else{
			res.send(err);
		}
	}
});
			

// 19 캐릭터의 탐험 스테이지들의 정보를 가져온다.
app.post('/loadExploreStagesInformation', function(req, res, err){
	if(req != null){
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			var query = "SELECT Stage_Number, Star_Count FROM ExploreStage WHERE User_Name = ? AND Character_Type = ?";
			var param = [User_Name = req.body.Name,
						Character_Type = req.body.Character_Type];
			conn.query(query, param, function(err, row, fields){
				if(!err){
					res.send(row);
				} else{
					res.send(err);
				}
			});
		}
	}
});

// 20. 처음 시작 시 탐험 스테이지를 초기화한다.
app.post('/SetExploreStage', function(req, res, err){
	if(req != null){
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			// 스테이지의 정보를 초기화
			var query = "INSERT INTO ExploreStage VALUES(?, ?, ?, ?)";
			var param = [User_Name = req.body.Name,
						Character_Type = req.body.Character_Type,
						Stage_Number = req.body.Stage_Number,
						Star_Count = req.body.Star_Count];

			conn.query(query, param, function(err, row, fields){
				if(!err){
					res.send("suceess" + row);
				} else{
					res.send(err);
				}
			});
		}
	}
});


// 21. 스테이지의 정보를 업데이트 한다. 
app.post('/UpdateExploreStage', function(req, res, err){
	if(req != null){
		if(typeof(req.body.Name) != "undefined" && typeof(req.body.Character_Type) != "undefined"){
			var query = "UPDATE ExploreStage SET Star_Count = ? WHERE User_Name = ? AND Character_Type = ? AND Stage_Number = ?";
			var param = [Star_Count = req.body.Star_Count,
						User_Name = req.body.Name,
						Character_Type = req.body.Character_Type,
						Stage_Number = req.body.Stage_Number];
			conn.query(query, param, function(err, row, fields){
				if(!err){
					res.send("success " + row);
				} else{
					res.send("fail " + err);
				}
			});
		}
	}
});

app.listen(15111, function () {
  console.log('Example app listening on port 15111!');
});
