using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction_manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timer());
    }

   private IEnumerator timer()
    {
        yield return new WaitForSeconds(10f);
        this.gameObject.SetActive(false);
    }
}
