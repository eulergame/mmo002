using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
namespace X.HotFix.Games.Brain
{
	public class BrainPresenter : MonoBehaviour
	{
		[SerializeField] private Button buttonClear;
		[SerializeField] private Button buttonEnter;
		[SerializeField] private Button buttonTips;
		[SerializeField] private Button buttonFriend;
		[SerializeField] private Button buttonHome;
		[SerializeField] private TextMeshProUGUI textMeshProUGUIAnswer;
		[SerializeField] private TextMeshProUGUI textMeshProUGUIIQ;
		// Start is called before the first frame update
		void Start()
		{
			buttonTips.onClick.AddListener(() => {
				
			});
			buttonClear.onClick.AddListener(() => {
				textMeshProUGUIAnswer.text = string.Empty;
			});
			MessageBroker.Default.Receive<Event.NumberClick>().Subscribe(x => {
				textMeshProUGUIAnswer.text += x.Number;	
			});
		}
	}
}

