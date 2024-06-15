using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death_Screen : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {

        if (SaveScript.health <= 0.0f)
        {
            if (SaveScript.uniqe_features_index == 3 && Time.time - SaveScript.time_of_uniqe_feature_activasion > SaveScript.uniqe_features_index_CD)
            {
                SaveScript.time_of_uniqe_feature_activasion = Time.time;
                SaveScript.health = 0.5f;
            }
            else
            {
                SaveScript.is_invisible = true;
                animator.SetTrigger("death");
                StartCoroutine(Wait_before_Basic_Scene());
                SaveScript.health = 1.0f;
            }

        }
    }

    IEnumerator Wait_before_Basic_Scene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
