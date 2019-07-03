### 20190630
+ 플레이어 네비 구현
+ 마우스 클릭 시 그 지점으로 이동 구현
+ 마우스 휠을 굴리거나 a,d 를 누르면 화면이동 구현
+ Target Object를 클릭 시 상호작용하도록 함
+ 게임 메인화면 씬 토대만 구현
+ 플레이어 캐릭터 Asset 가져옴.
+ 캐릭터가 움직이면 걷는 애니메이션 구현
+ item 상호작용 오브젝트 생성
+ item 리트스 구현
+ Inventory 구현

### 20190701
+ Iventory UI 구현.
+ 아이템을 먹으면 inventory에 적재, item의 remove버튼을 누르면 제거되는 기능 구현
+ i와 b를 누르면 inventory가 꺼졌다 켰지는 기능 구현
+ inventory 클릭 시 캐릭터가 움직이지 않게 함. (inventory만 클릭하게 됨)
+ 장비 슬롯 구현
+ 같은 부위에 있는 장비 착용 시 swap 기능 구현
+ player info UI 체력, 마나 바 업데이트

### 20190702
+ Monster LayerMask 생성
+ PlayerStats 만듬 장비 착용 시 데미지 방어구 연동
+ enemy에가 다가가면 player를 쫒아오는 기능 구현 (NavMashAgent)사용.
+ Enemy 체력, 공격력, 방어력 설정. 및 플레이어가 가까이에 오면 멈춰서 방향만 바꿈.

### 20190703
+ enemy는 player에게 데미지 주기, player는 enemy를 클릭하면  데미지 주기 기능 추가
+ player animator를 위한 클래스 작성

### 20190704
+ shield, sword, chest armor, legs armor, helmet 에셋 가져오고 ,이름, 설정 바꿈
