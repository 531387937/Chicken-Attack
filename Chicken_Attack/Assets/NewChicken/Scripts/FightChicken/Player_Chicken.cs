using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public class Player_Chicken : MonoBehaviour
{
    public GameObject[] ga;

    // Start is called before the first frame update
    void Start()
    {
        //按照鸡的种类生成鸡
        if (GameSaveNew.Instance.playerChicken != null)
        {
            if(GameSaveNew.Instance.playerChicken.Pos.y > -6.5f && GameSaveNew.Instance.playerChicken.Pos.y < 6.5f)
            {
                GameObject a = Instantiate(ga[(int)GameSaveNew.Instance.playerChicken.Type], GameSaveNew.Instance.playerChicken.Pos, new Quaternion(0, 0, 0, 1));
                a.AddComponent<MyFightChicken>();
                a.GetComponent<MyFightChicken>().self = GameSaveNew.Instance.playerChicken;
            }
            else
            {
                GameObject a = Instantiate(ga[(int)GameSaveNew.Instance.playerChicken.Type], new Vector3(Random.Range(-5.5f,5.5f),GameSaveNew.Instance.playerChicken.Pos.y, GameSaveNew.Instance.playerChicken.Pos.z), new Quaternion(0, 0, 0, 1));
                a.AddComponent<MyFightChicken>();
                a.GetComponent<MyFightChicken>().self = GameSaveNew.Instance.playerChicken;
            }
           
        }    

        //遍历小鸡
        if (GameSaveNew.Instance.PD.Chick != null)
        {
            for (int i = 0; i < GameSaveNew.Instance.PD.Chick.Count; i++)
            {
                if(GameSaveNew.Instance.PD.Chick[i].Pos.x >- 6.5f && GameSaveNew.Instance.PD.Chick[i].Pos.x < 6.5f)
                {
                    GameObject b = Instantiate(ga[4], GameSaveNew.Instance.PD.Chick[i].Pos, new Quaternion(0, 0, 0, 1));
                    b.AddComponent<Chick>();
                    b.GetComponent<Chick>().self = GameSaveNew.Instance.PD.Chick[i];
                }
                else
                {
                    GameObject b = Instantiate(ga[4], new Vector3(Random.Range(-5.5f, 5.5f), GameSaveNew.Instance.PD.Chick[i].Pos.y, GameSaveNew.Instance.PD.Chick[i].Pos.z), new Quaternion(0, 0, 0, 1));
                    b.AddComponent<Chick>();
                    b.GetComponent<Chick>().self = GameSaveNew.Instance.PD.Chick[i];
                }
            }
        }

        //遍历退役鸡
        if (GameSaveNew.Instance.PD.OldChicken != null)
        {
            for (int i = 0; i < GameSaveNew.Instance.PD.OldChicken.Count; i++)
            {
                if (!GameSaveNew.Instance.PD.OldChicken[i].OutSide)
                {
                    if (GameSaveNew.Instance.PD.OldChicken[i].Pos.x > -6.5f && GameSaveNew.Instance.PD.OldChicken[i].Pos.x < 6.5f)
                    {
                        GameObject b = Instantiate(ga[(int)GameSaveNew.Instance.PD.OldChicken[i].Type], GameSaveNew.Instance.PD.OldChicken[i].Pos, new Quaternion(0, 0, 0, 1));
                        b.AddComponent<MyFightChicken>();
                        b.GetComponent<MyFightChicken>().self = GameSaveNew.Instance.PD.OldChicken[i];
                    }
                    else
                    {
                        GameObject b = Instantiate(ga[(int)GameSaveNew.Instance.PD.OldChicken[i].Type], new Vector3(Random.Range(-5.5f, 5.5f), GameSaveNew.Instance.PD.OldChicken[i].Pos.y, GameSaveNew.Instance.PD.OldChicken[i].Pos.z), new Quaternion(0, 0, 0, 1));
                        b.AddComponent<MyFightChicken>();
                        b.GetComponent<MyFightChicken>().self = GameSaveNew.Instance.PD.OldChicken[i];
                    }
                }
            }
        }

        //快排区分每只鸡的前后位置
        GameObject[] G = GameObject.FindGameObjectsWithTag("FightChicken");
        
        QuickSortArray(G, 0, G.Length - 1);

        for(int i = 0; i < G.Length; i++)
        {
            G[i].gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Chicken";
            G[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder = G.Length - i;
        }

        StartCoroutine(SaveGame());
    }

    IEnumerator SaveGame()
    {
        yield return new WaitForSeconds(5);
        GameSaveNew.Instance.SaveAllData();
        Debug.Log("保存");
        StartCoroutine(SaveGame());
    }

    /// <summary>
    /// 快速排序的方法
    /// </summary>
    /// <param name="array">数组</param>
    /// <param name="start">数组起始位置</param>
    /// <param name="end">数组终止位置</param>
    void QuickSortArray(GameObject[] array, int start, int end)
    {
        //若数组中数小于等于0直接返回
        if (start >= end) return;
        //定义一个基准值
        GameObject pivot = array[start];
        //定义2个索引指向数组的而开头和结束
        int left = start;
        int right = end;
        //按照从小到大的排序，直到2数相遇结束排序
        while (left < right)
        {
            //第一轮比较
            //把所有left右边的数都和基准值比较,获得最左边数在排序后位于数组中的位置（索引）
            while (left < right && array[right].GetComponent<Transform>().position.y >= pivot.GetComponent<Transform>().position.y)
            {
                right--;
            }
            //将该数放到数组中的该位置
            array[left] = array[right];
            //第二轮比较
            //把所有left右边的数都和基准值比较,获得最左边数在排序后位于数组中的位置（索引）
            while (left < right && array[left].GetComponent<Transform>().position.y <= pivot.GetComponent<Transform>().position.y)
            {
                left++;
            }
            //将该数放到数组中的该位置
            array[right] = array[left];
        }
        //将2轮比较之后的数组的起始值再赋为基准值（已经得到最大值，并在最后一位）
        array[left] = pivot;
        //递归该方法（每次剔除一个排好的数）
        QuickSortArray(array, start, left - 1);
        QuickSortArray(array, left + 1, end);
    }

    public void RefreshChickenWithOutRetireChichen()
    {
        //按照鸡的种类生成鸡
        if (GameSaveNew.Instance.playerChicken != null)
        {
            if (GameSaveNew.Instance.playerChicken.Pos.y > -6.5f && GameSaveNew.Instance.playerChicken.Pos.y < 6.5f)
            {
                GameObject a = Instantiate(ga[(int)GameSaveNew.Instance.playerChicken.Type], GameSaveNew.Instance.playerChicken.Pos, new Quaternion(0, 0, 0, 1));
                a.AddComponent<MyFightChicken>();
                a.GetComponent<MyFightChicken>().self = GameSaveNew.Instance.playerChicken;
            }
            else
            {
                GameObject a = Instantiate(ga[(int)GameSaveNew.Instance.playerChicken.Type], new Vector3(Random.Range(-5.5f, 5.5f), GameSaveNew.Instance.playerChicken.Pos.y, GameSaveNew.Instance.playerChicken.Pos.z), new Quaternion(0, 0, 0, 1));
                a.AddComponent<MyFightChicken>();
                a.GetComponent<MyFightChicken>().self = GameSaveNew.Instance.playerChicken;
            }

        }

        //遍历小鸡
        if (GameSaveNew.Instance.PD.Chick != null)
        {
            for (int i = 0; i < GameSaveNew.Instance.PD.Chick.Count; i++)
            {
                if (GameSaveNew.Instance.PD.Chick[i].Pos.x > -6.5f && GameSaveNew.Instance.PD.Chick[i].Pos.x < 6.5f)
                {
                    GameObject b = Instantiate(ga[4], GameSaveNew.Instance.PD.Chick[i].Pos, new Quaternion(0, 0, 0, 1));
                    b.AddComponent<Chick>();
                    b.GetComponent<Chick>().self = GameSaveNew.Instance.PD.Chick[i];
                }
                else
                {
                    GameObject b = Instantiate(ga[4], new Vector3(Random.Range(-5.5f, 5.5f), GameSaveNew.Instance.PD.Chick[i].Pos.y, GameSaveNew.Instance.PD.Chick[i].Pos.z), new Quaternion(0, 0, 0, 1));
                    b.AddComponent<Chick>();
                    b.GetComponent<Chick>().self = GameSaveNew.Instance.PD.Chick[i];
                }
            }
        }

        //快排区分每只鸡的前后位置
        GameObject[] G = GameObject.FindGameObjectsWithTag("FightChicken");

        QuickSortArray(G, 0, G.Length - 1);

        for (int i = 0; i < G.Length; i++)
        {
            G[i].gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Chicken";
            G[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder = G.Length - i;
        }

    }
    
    public void RefreshChicken()
    {
        //按照鸡的种类生成鸡
        if (GameSaveNew.Instance.playerChicken != null)
        {
            if (GameSaveNew.Instance.playerChicken.Pos.y > -6.5f && GameSaveNew.Instance.playerChicken.Pos.y < 6.5f)
            {
                GameObject a = Instantiate(ga[(int)GameSaveNew.Instance.playerChicken.Type], GameSaveNew.Instance.playerChicken.Pos, new Quaternion(0, 0, 0, 1));
                a.AddComponent<MyFightChicken>();
                a.GetComponent<MyFightChicken>().self = GameSaveNew.Instance.playerChicken;
            }
            else
            {
                GameObject a = Instantiate(ga[(int)GameSaveNew.Instance.playerChicken.Type], new Vector3(Random.Range(-5.5f, 5.5f), GameSaveNew.Instance.playerChicken.Pos.y, GameSaveNew.Instance.playerChicken.Pos.z), new Quaternion(0, 0, 0, 1));
                a.AddComponent<MyFightChicken>();
                a.GetComponent<MyFightChicken>().self = GameSaveNew.Instance.playerChicken;
            }

        }

        //遍历小鸡
        if (GameSaveNew.Instance.PD.Chick != null)
        {
            for (int i = 0; i < GameSaveNew.Instance.PD.Chick.Count; i++)
            {
                if (GameSaveNew.Instance.PD.Chick[i].Pos.x > -6.5f && GameSaveNew.Instance.PD.Chick[i].Pos.x < 6.5f)
                {
                    GameObject b = Instantiate(ga[4], GameSaveNew.Instance.PD.Chick[i].Pos, new Quaternion(0, 0, 0, 1));
                    b.AddComponent<Chick>();
                    b.GetComponent<Chick>().self = GameSaveNew.Instance.PD.Chick[i];
                }
                else
                {
                    GameObject b = Instantiate(ga[4], new Vector3(Random.Range(-5.5f, 5.5f), GameSaveNew.Instance.PD.Chick[i].Pos.y, GameSaveNew.Instance.PD.Chick[i].Pos.z), new Quaternion(0, 0, 0, 1));
                    b.AddComponent<Chick>();
                    b.GetComponent<Chick>().self = GameSaveNew.Instance.PD.Chick[i];
                }
            }
        }

        //遍历退役鸡
        if (GameSaveNew.Instance.PD.OldChicken != null)
        {
            for (int i = 0; i < GameSaveNew.Instance.PD.OldChicken.Count; i++)
            {
                if (!GameSaveNew.Instance.PD.OldChicken[i].OutSide)
                {
                    if (GameSaveNew.Instance.PD.OldChicken[i].Pos.x > -6.5f && GameSaveNew.Instance.PD.OldChicken[i].Pos.x < 6.5f)
                    {
                        GameObject b = Instantiate(ga[(int)GameSaveNew.Instance.PD.OldChicken[i].Type], GameSaveNew.Instance.PD.OldChicken[i].Pos, new Quaternion(0, 0, 0, 1));
                        b.AddComponent<MyFightChicken>();
                        b.GetComponent<MyFightChicken>().self = GameSaveNew.Instance.PD.OldChicken[i];
                    }
                    else
                    {
                        GameObject b = Instantiate(ga[(int)GameSaveNew.Instance.PD.OldChicken[i].Type], new Vector3(Random.Range(-5.5f, 5.5f), GameSaveNew.Instance.PD.OldChicken[i].Pos.y, GameSaveNew.Instance.PD.OldChicken[i].Pos.z), new Quaternion(0, 0, 0, 1));
                        b.AddComponent<MyFightChicken>();
                        b.GetComponent<MyFightChicken>().self = GameSaveNew.Instance.PD.OldChicken[i];
                    }
                }
            }
        }

        //快排区分每只鸡的前后位置
        GameObject[] G = GameObject.FindGameObjectsWithTag("FightChicken");

        QuickSortArray(G, 0, G.Length - 1);

        for (int i = 0; i < G.Length; i++)
        {
            G[i].gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Chicken";
            G[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder = G.Length - i;
        }
    }

    public void ShowRetire()
    {
        //遍历退役鸡
        if (GameSaveNew.Instance.PD.OldChicken != null)
        {
            for (int i = 0; i < GameSaveNew.Instance.PD.OldChicken.Count; i++)
            {
                if (!GameSaveNew.Instance.PD.OldChicken[i].OutSide)
                {
                    if (GameSaveNew.Instance.PD.OldChicken[i].Pos.x > -6.5f && GameSaveNew.Instance.PD.OldChicken[i].Pos.x < 6.5f)
                    {
                        GameObject b = Instantiate(ga[(int)GameSaveNew.Instance.PD.OldChicken[i].Type], GameSaveNew.Instance.PD.OldChicken[i].Pos, new Quaternion(0, 0, 0, 1));
                        b.AddComponent<MyFightChicken>();
                        b.GetComponent<MyFightChicken>().self = GameSaveNew.Instance.PD.OldChicken[i];
                    }
                    else
                    {
                        GameObject b = Instantiate(ga[(int)GameSaveNew.Instance.PD.OldChicken[i].Type], new Vector3(Random.Range(-5.5f, 5.5f), GameSaveNew.Instance.PD.OldChicken[i].Pos.y, GameSaveNew.Instance.PD.OldChicken[i].Pos.z), new Quaternion(0, 0, 0, 1));
                        b.AddComponent<MyFightChicken>();
                        b.GetComponent<MyFightChicken>().self = GameSaveNew.Instance.PD.OldChicken[i];
                    }
                }
            }
        }

        //快排区分每只鸡的前后位置
        GameObject[] G = GameObject.FindGameObjectsWithTag("FightChicken");

        QuickSortArray(G, 0, G.Length - 1);

        for (int i = 0; i < G.Length; i++)
        {
            G[i].gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Chicken";
            G[i].gameObject.GetComponent<SpriteRenderer>().sortingOrder = G.Length - i;
        }
    }

 }