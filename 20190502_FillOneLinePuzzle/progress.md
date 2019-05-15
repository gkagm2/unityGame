20190513
- 로딩화면구현 완료-
- 타일 초기화 완료-
- LevelStageInfo 성공했는지 안했는지 구현-
- StageScreen에서 스테이지를 깨면 UI바꾸기 구현 완료-
- 성공 팝업 창 뒤로가기, 홈으로가기버튼 세부 구조 구현 완료-


20190514
성공하면 성공 창이 뜬다.(0)
  성공창이 뜨면 현재 레벨과 스테이지를 가져온다.(0)
  현재 LevelStageInfo에 isSuccess를 참으로 바꾼다.(0)
  
성공 창에서 다음 스테이지 버튼을 눌렀을 경우
  
  다음 스테이지가 있는지 확인한다 
  있으면 
    현재 스테이지 정보를 다음 스테이지 값으로 바꾼다. (currenetStage) (0)
    현재 GameObject 타일을 안보이게 하고 (0)
    다음 GameObject 타일을 보이게 한다.(0)
    
StageInfo.cs 
- 색깔별 클릭 가능하게 만들기(0)
  분홍색 이미지는 클릭 가능함. currentStage, currentLevel과 같다면 Next 이미지로 바꾼다.(0)
  currentStage,currentLevel과 누르는 level과 stage가 같다면 Next로 이미지로 바꾸며. 클릭가능함.(0)
  LevelStageInfo에서 isSuccess가 true인 스테이지는 클릭 가능함.(0)
  LevelStageInfo에서 isSuccess가 false인 스테이지는 클릭 불가능함.(0)
  
  playerinfo에서 currentLevel, stage를 이용하여 그 이전의 숫자는 isSuccess를 true로 그 이후는 false로 바꿈.(0)
  
  
스테이지를 깨면 LevelStageInfo에서 isSuccess를 true로 바꿔야 함.(0)
  해당 스테이지의 current level과 stage를 LevelStageInfo 인스턴스 배열에서 찾는다.(0)
  찾아서 isSuccess로 바꾼다.(0)
  




TileControl.cs에서  
처음.start부분에서 (시작할 때) 자식 객체들중에 체크된 타일을 찾는다.(0)
만약 체크된 타일이 1개이고 찾았으면 그 타일을 스택에 집어넣는다.(0)

Updated에서
모든 타일들이 터치가 되었는지 검사를 한다.(0)
체크된 개수가 2개 이상이면(0)
체크된 타일이 있으면  스택에 들어가 있는지 검사를 한다.(0)
스택에 들어가 있으면 무시하고(0)
스택에 들어가있지 않으면 스택에 그 타일의 Object(이름)를 넣는다.(0)

camera .cs
누른 타일이 현재 스택 맨 위에 있는 타일과 충돌하는 타일이면 색을 바꿈(0)
check가 된 타일이면 stack에 눌렀던 타일이 나오기 전까지 pop한다.{TileHit에서 구현함}(0)

20190515
이전 스테이지를 깬 후 다음 스테이지에 넘어간 다음 메뉴에서 스테이지를 누를 경우 screenStage가 안보이는 문제 해결(0)
다음 레벨과 스테이지 창을 보여줘야 한다(0)
스테이지를 하는 도중 스테이지 창으로 나갔다가 다시 들어오면 스택에 쌓여있던 문제 해결함. (0)
깬 스테이지의 별을 업데이트 한다. (0)
레벨에 있는 스테이지를 모두 다 깨면 레벨 화면으로 가가 한다.(0)


-TODO-

다음 레벨이 Unlock되게 한다.
	Unlock되게 하려면 star개수가 꽉 채어졌으면 

아직 깨지 않은 스테이지는 누를 수 없게 만든다.

진도 정보를 PlayerInfo.cs 에  myProgressLevel, myProgressStage로 만듬.
  처음이면 초기화 ()
  처음이 아니면 불러온다()
  스테이지를 깰 때마다 PlayerPrefabs에 저장하며 myProgS,L에 저장한다.

사용자의 진도 정보를 저장한다. LevelStageInfo를 저장해야 할 듯 어떻게 할 지..
 isSuccess로 쉽게 볼 수 있을 듯.

