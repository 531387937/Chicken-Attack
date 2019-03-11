using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Black_White : MonoBehaviour
{
    public GameObject White_Block;
    public GameObject Black_Block;
    private ArrayList blocks;
    public GameObject BlockArea;
    public GameObject[] Dispear_UI;
   public float timer;
    private bool GameStart=false;
    public int MaxBlock;
    private int leftBlock;
    public GameObject ShowTime;
    private int a;
    public BattleSystem BS;
    // Start is called before the first frame update
    void Start()
    {
        leftBlock = MaxBlock;
        a = MaxBlock;
    }

    // Update is called once per frame
    void Update()
    {
        ShowTime.GetComponent<Text>().text = timer.ToString("f2");
        if(GameStart)
        {
            timer += Time.deltaTime;
        }
        if(leftBlock==0)
        {
            GameStart = false;
            print(timer);
            leftBlock--;
            BS.SendMessage("RoundEnd", timer);
            foreach (GameObject Dispear in Dispear_UI)
            {
                Dispear.SetActive(true);
            }
        }
    }
    public void StartGame()
    {
        foreach(GameObject Dispear in Dispear_UI)
        {
            Dispear.SetActive(false);
        }
        MaxBlock = a;
        leftBlock = MaxBlock;
        timer = 0;
        GameStart = true;
        blocks = new ArrayList();
        for(int i = 0;i<=3;++i)
        {
            
            AddBlock(i);
        }
    }
    void AddBlock(int rowIndex)
    {
        int a = MaxBlock;
        if(MaxBlock==0)
        {
            return;
        }
        MaxBlock--;
        int WhiteBlockNum = Random.Range(0, 2);
        for(int i = 0;i<2;i++)
        {
            GameObject o;
            if(i==WhiteBlockNum)
            {
                o = Instantiate(White_Block) as GameObject;
            }
            else
            {
                o = Instantiate(Black_Block) as GameObject;
            }
            o.transform.parent = BlockArea.transform;
            Block b = o.GetComponent<Block>();
            b.SetPosition(i, rowIndex);
            blocks.Add(b);
        }
    }
    public void SelectBlock(Block block)
    {
        if(block.num==0)
        {
            if(block.CanTouch)
            {
                leftBlock--;
                for(int i = 0;i<blocks.Count;i++)
                {
                    Block b = (Block)blocks[i];
                    b.MoveDown();

                    if (b.num == -1)
                    {
                        blocks.RemoveAt(i);
                        Destroy(b.gameObject);
                        i--;
                    }
                }
                AddBlock(3);
            }
            else
            {
                timer += 3f;
            }
        }
        else
        {
            timer += 3f;
        }
    }
}
