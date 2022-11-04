using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AurgmentRandom : MonoBehaviour
{
	[SerializeField] Transform artifacts;

	[SerializeField] Image arg1;
	[SerializeField] Image arg2;
	[SerializeField] Image arg3;

	private List<Artifact> list = new List<Artifact>();
	int[] selectedArtifact;

    void OnEnable()
	{
		foreach (Transform artifact in artifacts)
		{
			list.Add(artifact.GetComponent<Artifact>());
		}

		int selectedArtCount = 0;
		
		selectedArtifact = new int[3];

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
			if (ArtifactSelector.ActiveArtifacts.Contains(x))
			{
				continue;
			}
			selectedArtifact[selectedArtCount] = x;
			selectedArtCount++;
		}

		arg1.sprite = list[selectedArtifact[0]].icon;
		arg2.sprite = list[selectedArtifact[1]].icon;
		arg3.sprite = list[selectedArtifact[2]].icon;
	}

    private void Start()
    {
		PlayerController.ReleaseControl(true);
	}

	public void ButtonPushed(int choice)
    {
		ArtifactSelector.ActiveArtifacts.Add(selectedArtifact[choice]);
		ArtifactSelector.UpdateArtifacts();
	}
}
