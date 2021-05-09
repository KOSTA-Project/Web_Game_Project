# Web_Game_Project
using ASP.NET   
끝말잇기 만들어 보기       

1) 클라이언트가 끝말잇기 클릭시 해당 세션 정보를 서버에 전송 // List<Socket> word_players   
2) 2개의 세션이 들어올 때 까지 대기상태 wait 
3) 서버에서 word_player에 소켓이 2개가되면 끝말있기 클래스, word_class의 게임을 쓰레드로 실행시키기.
4) 단어 정보는 한국어기초사전 https://krdict.korean.go.kr/openApi/openApiInfo API를 사용
5) html에서 5초의 입력 대기시간을 줄지, 서버측에서 5초를 카운트할지는 정해야함.


