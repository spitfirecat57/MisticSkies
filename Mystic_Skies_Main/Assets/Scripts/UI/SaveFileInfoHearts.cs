using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SaveFileInfoHearts : MonoBehaviour
{
	public int saveFileIndex;

	void Start()
	{
		// TODO: This whole thing
//		int numPieces = PlayerManager.GetSaveFileHealth(saveFileIndex);
//		int numWhole = numPieces / 4;
//		int numQuarters = numPieces % 4;
//		for(int i = 0; i < numWhole; ++i)
//		{
//			GameObject imageHolder = GameObject.Instantiate(UIManager.UIImageObject) as GameObject;
//			imageHolder.transform.SetParent(gameObject.transform, false);
//			Image img = imageHolder.GetComponent<Image>();
//			img.sprite = UIManager.heartWholeSprite;
//		}
//		if(numQuarters > 0)
//		{
//			GameObject imageHolder = GameObject.Instantiate(UIManager.UIImageObject) as GameObject;
//			imageHolder.transform.SetParent(gameObject.transform, false);
//			Image img = imageHolder.GetComponent<Image>();
//			if(numQuarters == 1)
//			{
//				img.sprite = UIManager.heartQuarterSprite;
//			}
//			else if(numQuarters == 2)
//			{
//				img.sprite = UIManager.heartHalfSprite;
//			}
//			else
//			{
//				img.sprite = UIManager.heartThreeQuarterSprite;
//			}
//		}
	}
}
