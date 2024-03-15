using UnityEngine;

public class SolarSystem : MonoBehaviour
{
	[SerializeField] private GameObject orbitTemplate;
	private void Start()
	{
		foreach (var x in GameData.planets)
		{
			var o = GameObject.Instantiate(orbitTemplate);
			o.SetActive(true);
			o.name = x.Key.ToString();
			o.transform.localScale = Vector3.one * x.Value.RadiusInKilometers * 0.0001f;
			var orbit = o.GetComponent<Orbit>();
			orbit.OribitPeriod = x.Value.OrbitPeriodInDays * 0.01f;
			orbit.SemiMajorAxis = x.Value.SemiMajorAxisInAU;
		}
	}
}
