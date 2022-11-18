using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArgumentDescription : MonoBehaviour
{
    [Header("Attachments")]
    public TMP_Text descriptionBox;

     Artifact artifact;

    public void SetArtifact(Artifact a)
    {
        artifact = a;

        descriptionBox.text = artifact.description;
    }
}
