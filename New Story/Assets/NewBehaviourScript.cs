using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private void Start()
    {
        StartCoroutine(time());
    }
    IEnumerator time()
    {
        text.text = Mathf.CeilToInt(1f / Time.deltaTime).ToString();
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(time());
    }

}
