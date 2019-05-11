using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChickenRun : MonoBehaviour
{
    private bool OutSide = true;

    enum WalkMode
    {
        Left = 0,
        Right = 1
    }

    WalkMode mode;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "QiZiNewChicken")
        {
            this.gameObject.GetComponent<Animator>().SetBool("Run", true);
        }
        if (this.gameObject.transform.position.x >= 0)
        {
            mode = WalkMode.Right;
        }
        else if (this.gameObject.transform.position.x < 0)
        {
            mode = WalkMode.Left;
        }
        StartCoroutine(RandomChange());
        StartCoroutine(EatSomthing());
    }

    private void Update()
    {
        switch (mode)
        {
            case WalkMode.Left:
                this.transform.localEulerAngles = new Vector3(0,180,0);
                break;
            case WalkMode.Right:
                this.transform.localEulerAngles = Vector3.zero;
                break;
        }
    }

    IEnumerator EatSomthing()
    {
        yield return new WaitForSeconds(Random.Range(1,3));
        if (OutSide)
        {
            if (Random.Range(0, 5) == 0)
            {
                this.gameObject.GetComponent<Animator>().SetBool("Eat", true);
            }
            else
            {
                this.gameObject.GetComponent<Animator>().SetBool("DoRun", true);
            }
        }
        StartCoroutine(EatSomthing());
    }

    IEnumerator RandomChange()
    {
        yield return new WaitForSeconds(Random.Range(6, 20));
        if (OutSide)
        {
            if (Random.Range(0, 2) == 0)
            {
                mode = WalkMode.Left;
                
            }
            else
            {
                mode = WalkMode.Right;
            }
        }
        StartCoroutine(RandomChange());
    }

    void Run(float length)
    {
        switch (mode)
        {
            case WalkMode.Left:
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - length, this.gameObject.transform.position.y, 0);
                break;
            case WalkMode.Right:
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + length, this.gameObject.transform.position.y, 0);
                break;
        }
    }

    void FinishEat()
    {
        if (SceneManager.GetActiveScene().name == "QiZiNewChicken")
        {
            this.gameObject.GetComponent<Animator>().SetBool("Eat", false);
        }
    }

    void FinishRun()
    {
        if (SceneManager.GetActiveScene().name == "QiZiNewChicken")
        {
            this.gameObject.GetComponent<Animator>().SetBool("Run", false);
            this.gameObject.GetComponent<Animator>().SetBool("DoRun", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Edge" && OutSide)
        {
            OutSide = false;
            if (mode == WalkMode.Right)
            {
                mode = WalkMode.Left;
            }
            else if (mode == WalkMode.Left)
            {
                mode = WalkMode.Right;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Edge")
        {
            OutSide = true;
        }
    }

}
