using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build.Player;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    static public DatabaseManager instance;

    // declare databaseManager script as instance. 

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        } else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    public List<Item> itemList = new List<Item>(); // list of the item that used in the game.
    public List<Item> inventoryList = new List<Item>(); // list of the item that in the inventory. 


    void Start()
    {
        itemList.Add(new Item(1001, "하트", "누군가를 깊이 생각하는 마음.", Item.ItemType.Heart));
        itemList.Add(new Item(2001, "배터리", "전자기기를 작동시키는 에너지.", Item.ItemType.UseAt));
        itemList.Add(new Item(3001, "동전", "백원으로 오락실도 갈 수 있고 캡슐 뽑기도 할 수 있다.", Item.ItemType.UseAt));
        itemList.Add(new Item(3002, "성냥", "무언가에 불을 붙일 때 쓰는 도구.", Item.ItemType.UseAt));
        itemList.Add(new Item(3003, "사용한 연기튜브", "쓰레기는 쓰레기통에.", Item.ItemType.UseAt));
        itemList.Add(new Item(3004, "회전 레버", "어딘가에서 떨어진 부품같다.", Item.ItemType.UseAt));
        itemList.Add(new Item(3005, "열쇠", "자물쇠를 열 수 있는 열쇠.", Item.ItemType.UseAt));
        itemList.Add(new Item(4001, "핸드폰", "강아지 친구 귀여운 쿼카 케이스.", Item.ItemType.UseAt));
        inventoryList.Add(itemList[0]); // Add heart item into the inventory when it start. 
    }

}
