using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatchBox : MonoBehaviour
{
    public UnityEvent<List<Block>> recipient;

    List<Block> catchBlocks = new List<Block> { };
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block")
        {
            Debug.Log("Block catch");
            Debug.Log("Name = " + other.name);
            catchBlocks.Add(other.gameObject.GetComponent<Block>());
            Debug.Log("0");
            if (sendCatchBlocks != null) { StopCoroutine(sendCatchBlocks); }
            sendCatchBlocks = StartCoroutine(SendCatchBlocks());
            Debug.Log("1");
        }
        
    }

    private Coroutine sendCatchBlocks;
    IEnumerator SendCatchBlocks()
    {
        yield return new WaitForFixedUpdate();
        Debug.Log("2");
        recipient.Invoke(catchBlocks);
        Debug.Log("3");
        catchBlocks.Clear();
        Debug.Log("4");
    }
}
