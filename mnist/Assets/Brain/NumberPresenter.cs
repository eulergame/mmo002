using UniRx;
using UnityEngine;
using UnityEngine.UI;
namespace X.HotFix.Games.Brain
{
	public class NumberPresenter : MonoBehaviour
	{
		[SerializeField] private int Number;
		private void Start()
		{
			GetComponent<Button>().onClick.AddListener(() =>
			{
				MessageBroker.Default.Publish(new Event.NumberClick { Number = Number });
			});
		}
	}
}

