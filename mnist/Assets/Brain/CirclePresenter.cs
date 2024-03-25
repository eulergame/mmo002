using UnityEngine;
namespace X.HotFix.Games.Brain
{
	public class CirclePresenter : MonoBehaviour
	{
		[SerializeField] float Angle;
		[SerializeField] Transform Text;
		private void Start()
		{
			var a = transform.localEulerAngles;
			a.z = Angle;
			transform.localEulerAngles = a;
			Text.eulerAngles = Vector3.zero;
		}
	}
}

