using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AurgmentRandom : MonoBehaviour
{
    [SerializeField] Transform artifacts;


    
    private List<Artifact> list = new List<Artifact>();

    void OnEnable()
    {
        foreach (Transform artifact in artifacts)
        {
            list.Add(artifact.GetComponent<Artifact>());
        }

        int selectedArtCount = 0;
        
        int []selectedArtifact = new int[3];

        while (selectedArtCount < selectedArtifact.Length)
        {
            int x = Random.Range(0, list.Count);
            bool inArray = false;
            for (int i = 0;i < selectedArtCount; i++)
            {
                if (x == selectedArtifact[i])
                {
                    inArray = true;
                    break;
                }
            }
            if (inArray == true)
            {
                continue;
            }
        }
    }

    void Update()
    {

    }


}
