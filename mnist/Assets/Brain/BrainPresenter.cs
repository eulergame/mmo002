using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
namespace X.HotFix.Games.Brain
{
	public class BrainPresenter : MonoBehaviour
	{
		[SerializeField] private List<Button> buttonsNumber =new List<Button>();
		[SerializeField] private Button buttonClear;
		[SerializeField] private Button buttonEnter;
		[SerializeField] private Button buttonTips;
		[SerializeField] private TextMeshProUGUI textMeshProUGUIAnswer;
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

		// Update is called once per frame
		void Update()
		{

		}
	}
}

