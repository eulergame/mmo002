using UnityEngine;
using UnityEngine.UI;
using UniRx;
namespace X.HotFix.Games.Sort
{
	public class SortCanvasPresenter : MonoBehaviour
	{
		[SerializeField] private Button buttonSort;
		private void Start()
		{
			buttonSort.onClick.AddListener(() => {
				MessageBroker.Default.Publish(new Event.Sort { });
			});
		}
	}
}

