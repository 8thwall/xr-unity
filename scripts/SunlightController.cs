using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

public class SunlightController : MonoBehaviour {

  // How often the sun's Altitude/Azumuth be recalculated (seconds)
  public int shadowRefreshSeconds = 60;

  private double lat, lon;
  private double sunAltitude, sunAzimuth;
  private float initialNorthOffset = 0;

	IEnumerator Start () {

    // Enable the mobile device GPS and compass
		Input.location.Start(); 
		Input.compass.enabled = true;

		if (Input.location.isEnabledByUser) { // if mobile device GPS is enabled

			// Wait until Location Service initializes
			int maxWait = 10;
			while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
				yield return new WaitForSeconds (1);
				maxWait--;
			}

			// If Location Service doesn't initialize in time
			if (maxWait < 1) {
				Debug.Log ("Location Service Initialization Timed Out");
				yield break;
			}

			if (Input.location.status == LocationServiceStatus.Running) {
				// AdjustLightAngle() returns Azimuth value based on the assumption
				// you are facing North. Need to account for initial phone orientation
				initialNorthOffset = Input.compass.trueHeading;
				StartCoroutine (AdjustLightAngle());
			} else {
				Debug.Log ("Unable to determine device location");
			}
		}
	}
		

	IEnumerator AdjustLightAngle() {
		while (true) {

			// Refresh latitude/longitude
			lat = Input.location.lastData.latitude;
			lon = Input.location.lastData.longitude;

			// Refresh Sun position
			SunPosition.Position sunPos = SunPosition.CalculateSunPosition(DateTime.Now, lat, lon);
			sunAltitude = sunPos.Altitude;
			sunAzimuth = sunPos.Azimuth;

			double xRot = sunAltitude;
      // Adjust to account for initial orientation of phone
			double yRot = sunAzimuth - initialNorthOffset + 180;

			// Adjust rotation angle of directional light (i.e. sun)
			transform.rotation = Quaternion.Euler((float)xRot, (float)yRot, 0.0f);

			yield return new WaitForSeconds( shadowRefreshSeconds );
		}
	}
}

public static class SunPosition  {
	public struct Position {
		public double Altitude {get;set;}
		public double Azimuth {get;set;}
	}

	private const double Deg2Rad = Math.PI / 180.0;
	private const double Rad2Deg = 180.0 / Math.PI;

	public static Position CalculateSunPosition(
		DateTime dateTime, double latitude, double longitude) {

		// Convert to UTC
		dateTime = dateTime.ToUniversalTime();

		// Number of days from J2000.0.
		double julianDate = 367 * dateTime.Year -
			(int)((7.0 / 4.0) * (dateTime.Year + 
				(int)((dateTime.Month + 9.0) / 12.0))) +
			(int)((275.0 * dateTime.Month) / 9.0) +
			dateTime.Day - 730531.5;

		double julianCenturies = julianDate / 36525.0;

		// Sidereal Time
		double siderealTimeHours = 6.6974 + 2400.0513 * julianCenturies;

		double siderealTimeUT = siderealTimeHours +
			(366.2422 / 365.2422) * (double)dateTime.TimeOfDay.TotalHours;

		double siderealTime = siderealTimeUT * 15 + longitude;

		// Refine to number of days (fractional) to specific time.
		julianDate += (double)dateTime.TimeOfDay.TotalHours / 24.0;
		julianCenturies = julianDate / 36525.0;

		// Solar Coordinates
		double meanLongitude = CorrectAngle(Deg2Rad *
			(280.466 + 36000.77 * julianCenturies));

		double meanAnomaly = CorrectAngle(Deg2Rad *
			(357.529 + 35999.05 * julianCenturies));

		double equationOfCenter = Deg2Rad * ((1.915 - 0.005 * julianCenturies) * 
			Math.Sin(meanAnomaly) + 0.02 * Math.Sin(2 * meanAnomaly));

		double elipticalLongitude =
			CorrectAngle(meanLongitude + equationOfCenter);

		double obliquity = (23.439 - 0.013 * julianCenturies) * Deg2Rad;

		// Right Ascension
		double rightAscension = Math.Atan2(
			Math.Cos(obliquity) * Math.Sin(elipticalLongitude),
			Math.Cos(elipticalLongitude));

		double declination = Math.Asin(
			Math.Sin(rightAscension) * Math.Sin(obliquity));

		// Horizontal Coordinates
		double hourAngle = CorrectAngle(siderealTime * Deg2Rad) - rightAscension;

		if (hourAngle > Math.PI) {
			hourAngle -= 2 * Math.PI;
		}

		double altitude = Math.Asin(Math.Sin(latitude * Deg2Rad) *
			Math.Sin(declination) + Math.Cos(latitude * Deg2Rad) *
			Math.Cos(declination) * Math.Cos(hourAngle));

		// Nominator and denominator for calculating Azimuth
		// angle. Needed to test which quadrant the angle is in.
		double aziNom = -Math.Sin(hourAngle);
		double aziDenom =
			Math.Tan(declination) * Math.Cos(latitude * Deg2Rad) -
			Math.Sin(latitude * Deg2Rad) * Math.Cos(hourAngle);

		double azimuth = Math.Atan(aziNom / aziDenom);

		if (aziDenom < 0) // In 2nd or 3rd quadrant
		{
			azimuth += Math.PI;
		}
		else if (aziNom < 0) // In 4th quadrant
		{
			azimuth += 2 * Math.PI;
		}

		return new Position{ Altitude =altitude * Rad2Deg ,Azimuth =azimuth * Rad2Deg };
	}

	private static double CorrectAngle(double angleInRadians) {
		if (angleInRadians < 0) {
			return 2 * Math.PI - (Math.Abs(angleInRadians) % (2 * Math.PI));
		}
		else if (angleInRadians > 2 * Math.PI) {
			return angleInRadians % (2 * Math.PI);
		}
		else {
			return angleInRadians;
		}
	}
}
