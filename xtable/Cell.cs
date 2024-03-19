using TMPro;
using UnityEngine;
namespace X.HotFix.Games.XTable
{
	public class Cell : MonoBehaviour
	{
		[SerializeField] TextMeshPro textMeshPro;
		internal void SetNumber(string content)
		{
			textMeshPro.text = content;
		}
	}
}
