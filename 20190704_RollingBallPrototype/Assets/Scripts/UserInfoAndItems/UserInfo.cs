using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo
{
    public string id = "jang";
    public int scoreFromTheGame = 0;
    public int topScore = 0;
    public int coin = 0;
    public int coinFromTheGame = 0;

    public struct UserDate
    {
        public int day;
        public int month;
        public int year;
    }

    public int revivalCount = 0; // 부활한 횟수
    public int numNeededForRevivalItem; // 부활에 필요한 아이템 개수

    // TODO : 추가해봐.
    // Item
    public int revivalItem = 0;
    public int protectedItem = 0;

}