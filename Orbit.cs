using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public enum ePlanet
{
	Mercury,
	Venus,
	Earth,
	Mars,
	Jupiter,
	Saturn,
	Uranus,
	Nepture
}
public struct PlanetInfo
{
	public float OrbitPeriodInDays;
	public float SemiMajorAxisInAU;//astronomical units
	public float RadiusInKilometers;
}
public static class GameData
{
	public static Dictionary<ePlanet, PlanetInfo> planets = new Dictionary<ePlanet, PlanetInfo> {
		{ ePlanet.Mercury, new PlanetInfo{OrbitPeriodInDays=88f,SemiMajorAxisInAU = 0.3871f,RadiusInKilometers=2440f} },
		{ ePlanet.Venus, new PlanetInfo{ OrbitPeriodInDays=225f,SemiMajorAxisInAU = 0.7233f,RadiusInKilometers=6052f } },
		{ ePlanet.Earth, new PlanetInfo{ OrbitPeriodInDays=365.26f,SemiMajorAxisInAU = 1.0f,RadiusInKilometers=6371f} },
		{ ePlanet.Mars, new PlanetInfo{ OrbitPeriodInDays=686.98f,SemiMajorAxisInAU=1.5273f,RadiusInKilometers=3390f} },
		{ ePlanet.Jupiter, new PlanetInfo{ OrbitPeriodInDays=4332.82f,SemiMajorAxisInAU= 5.2028f,RadiusInKilometers=69911f} },
		{ ePlanet.Saturn, new PlanetInfo{ OrbitPeriodInDays=10755.70f,SemiMajorAxisInAU= 9.5388f,RadiusInKilometers=58232f} },
		{ ePlanet.Uranus, new PlanetInfo{ OrbitPeriodInDays=30687.15f,SemiMajorAxisInAU= 19.1914f,RadiusInKilometers= 25362f} },
		{ ePlanet.Nepture, new PlanetInfo{ OrbitPeriodInDays=60190.03f,SemiMajorAxisInAU=30.0611f,RadiusInKilometers=24622f} },
	};
}
public class Orbit : MonoBehaviour
{
	public float m;
	public float OribitPeriod;
	public float SemiMajorAxis;
	[SerializeField] float initialAngle;
	[SerializeField] float angle;
	private float AngleSpeed => 2.0f * Mathf.PI / OribitPeriod;
	// Start is called before the first frame update
	void Start()
	{
		angle = initialAngle;
		Observable.EveryUpdate().Subscribe(x =>
		{
			angle += Time.deltaTime * AngleSpeed;
			var p = transform.position;
			p.x = SemiMajorAxis * Mathf.Cos(angle);
			p.y = SemiMajorAxis * Mathf.Sin(angle);
			transform.position = p;
		}).AddTo(gameObject);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
