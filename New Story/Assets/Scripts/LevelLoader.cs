using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject loading;
    public float transitionTime = 1f;
    public Animator animator;
    private AsyncOperation async;
    public void LoadNextLevel(string levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex));
    }
    
    IEnumerator LoadLevel(string levelindex)
    {
        loading.SetActive(true);
        animator.SetTrigger("isOpen");
        yield return new WaitForSeconds(1);
        async = SceneManager.LoadSceneAsync(levelindex);
        async.allowSceneActivation = true;
        loading.SetActive(true);
    }
}
