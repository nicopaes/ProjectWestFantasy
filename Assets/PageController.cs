using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour 
{
	[Header("PAGINA")]
	public int PageNumber;

	[Space]
	[Header("CURRENT OBJ")]	
	public GameObject currentObj;


	[Space]
	[Header("LIST OF OBJECTS")]
	public List<GameObject> ListObj = new List<GameObject>();
	public int currentIndex = -1; // Object starts at -1

	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			AddToIndex();
			currentObj = ListObj[currentIndex];

			if(GetLayerName(currentObj) == "TYPE1") // SE É TIPO 1 
			{
				if(!IsOnState(currentObj,"CYCLE")) // NÃO ESTÁ NO CYCLE
				{
					PlayAnimation(currentObj,"INTRO"); // TOCA A INTRO
				}
			}
			else if(GetLayerName(currentObj) == "TYPE2")
			{
				if(!IsOnState(currentObj,"CYCLE")) // NÃO ESTÁ NO CYCLE
				{
					PlayAnimation(currentObj,"INTRO"); //TOCA A INTRO
				}
				else 
				{
					SetTrigger(currentObj,"TOEND"); //VAI PRO END
				}
			}
			else if(GetLayerName(currentObj) == "TYPE3")
			{
				PlayAnimation(currentObj,"INTRO"); //TOCA A INTRO
			}
		}
	}

	string GetLayerName(GameObject obj)
	{
		return obj.GetComponent<Animator>().GetLayerName(0);		
	}

	bool IsOnState(GameObject obj,string StateName)
	{
		return obj.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(StateName);
	}

	void PlayAnimation(GameObject obj,string StateName)
	{
		obj.GetComponent<Animator>().Play(StateName);
	}

	void SetTrigger(GameObject obj, string TriggerName)
	{
		obj.GetComponent<Animator>().SetTrigger(TriggerName);
	}

	void AddToIndex()
	{
		if(currentIndex < ListObj.Count -1) currentIndex++;
	}

	
}
