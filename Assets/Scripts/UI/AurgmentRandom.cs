using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AurgmentRandom : MonoBehaviour
{
	[SerializeField] Transform artifacts;
	public GameObject AugmentationUI;

	[SerializeField] Image arg1;
	[SerializeField] Image arg2;
	[SerializeField] Image arg3;

	private List<Artifact> list = new List<Artifact>();
	int[] selectedArtifact;

    void OnEnable()
	{
		Time.timeScale = 0f;

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
		arg1.transform.parent.GetComponent<ArgumentDescription>().SetArtifact(list[selectedArtifact[0]]);
		arg2.sprite = list[selectedArtifact[1]].icon;
		arg2.transform.parent.GetComponent<ArgumentDescription>().SetArtifact(list[selectedArtifact[1]]);
		arg3.sprite = list[selectedArtifact[2]].icon;
		arg3.transform.parent.GetComponent<ArgumentDescription>().SetArtifact(list[selectedArtifact[2]]);
	}

    private void Start()
    {
		PlayerController.UnlockControls(true);
	}

	public void ButtonPushed(int choice)
    {
		ArtifactSelector.ActiveArtifacts.Add(selectedArtifact[choice]);
		ArtifactSelector.UpdateArtifacts();
		AugmentationUI.SetActive(false);
		PlayerController.UnlockControls(false);
		Time.timeScale = 1f;
	}
}
