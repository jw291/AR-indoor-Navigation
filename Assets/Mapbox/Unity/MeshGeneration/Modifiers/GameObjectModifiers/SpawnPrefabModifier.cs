namespace Mapbox.Unity.MeshGeneration.Modifiers
{
	using System;
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Utilities;
	using Mapbox.Unity.MeshGeneration.Data;
	using Mapbox.Unity.MeshGeneration.Components;
	using Mapbox.Unity.MeshGeneration.Interfaces;

    [CreateAssetMenu(menuName = "Mapbox/Modifiers/Spawn Prefab Modifier")]
    public class SpawnPrefabModifier : GameObjectModifier
    {
        enum SpawnLocation
        {
            Top,
            Front,
            Center
        };

        [SerializeField]
        private SpawnLocation _spawnLocation;

        [SerializeField]
        string _destinationLocationIdKey;

        [SerializeField]
        string _destinationLocationNameKey;

        [SerializeField]
        string _destinationLocationPositionKey;

        [SerializeField]
        string _destinationLocationHeadingKey;

        [SerializeField]
        private string _prefabLocation;

        //[SerializeField]
        //private GameObject _prefab;

        [SerializeField]
        private bool _scaleDownWithWorld = false;

        
        string colName;
        int pointId; // 추가
        

        public override void Run(VectorEntity ve, UnityTile tile)
		{
			Vector3 met = new Vector3();

            var dbobj = GameObject.Find("DBobj").GetComponent<DBscript>();

            int input_colNum = dbobj.col_num;//dbobj.col_num2
            int input_shelfNum = dbobj.book_sNum;//dbobj.book_sNum15
            string input_direction = dbobj.shelf_dir;//dbobj.shelf_dirfront
            int input_shelfNum1 = 0;
            int input_shelfNum2 = 0;

            if(input_direction == "front")
            {
                input_shelfNum1 = input_shelfNum - 1;
                input_shelfNum2 = input_shelfNum;
            }
            else if(input_direction == "back")
            {
                input_shelfNum1 = input_shelfNum;
                input_shelfNum2 = input_shelfNum + 1;
            }

            string input_colName = input_colNum + "_shelf " + input_shelfNum1 + "_" + input_shelfNum2; // 추가

            var typeName = ve.Feature.Properties["destination_type"].ToString();
            if (typeName == "phone-room")
            {
                if (input_colName == (colName = ve.Feature.Properties["name"].ToString()))
                {
                    Debug.Log(colName);
                    pointId = int.Parse(ve.Feature.Properties["id"].ToString());
                    Debug.Log(pointId);

                    {
                        string planetName = ve.Feature.Properties["name"].ToString();

                        string prefabName = "Prefabs/" + "scroll3"; //"Saturn" <- planetName
                                                                              //Debug.Log("PrefabName : " + prefabName);

                        var scale = tile.TileScale;
                        int selpos = ve.Feature.Points[0].Count / 2;
                        met = ve.Feature.Points[0][selpos];
                        var prefabGO = (GameObject)Instantiate(Resources.Load(prefabName));
                        prefabGO.name = prefabName;
                        //met.y = 7 * scale;
                        met.y = prefabGO.transform.Find("scrollTop").GetComponent<MeshRenderer>().bounds.extents.y+ 0.5f;
                        prefabGO.transform.localScale = new Vector3(1f,1f,1f);
                        prefabGO.transform.position = met;
                        prefabGO.transform.Rotate(40,126, -14);
                        prefabGO.transform.SetParent(ve.GameObject.transform, false);
                    }//여기까지 추가

                    var destPosition = met;
                    destPosition.y = 0;

                    Location.DestinationPointData locationData = new Location.DestinationPointData();
                    var feature = ve.Feature;

                    int id = 0;
                    float heading = 0.0f;
                    string locationName = string.Empty;
                    string latitudeLongitudeString;
                    Vector2d latitudeLongitude = Vector2d.zero;
                    string locationColor = string.Empty;

                    if (feature.Properties.ContainsKey(_destinationLocationIdKey))
                    {
                        if (!int.TryParse(feature.Properties[_destinationLocationIdKey].ToString(), out id))
                        {
                            Debug.Log("No property with key : " + _destinationLocationIdKey + "found!");
                        }
                    }

                    if (feature.Properties.ContainsKey(_destinationLocationNameKey))
                    {
                        locationName = feature.Properties[_destinationLocationNameKey].ToString();
                        if (string.IsNullOrEmpty(locationName))
                        {
                            Debug.Log("No property with key : " + _destinationLocationNameKey + "found!");
                        }
                    }

                    if (feature.Properties.ContainsKey(_destinationLocationPositionKey))
                    {
                        latitudeLongitudeString = feature.Properties[_destinationLocationPositionKey].ToString();
                        latitudeLongitude = Conversions.StringToLatLon(latitudeLongitudeString);
                    }

                    if (feature.Properties.ContainsKey(_destinationLocationHeadingKey))
                    {
                        if (!float.TryParse(feature.Properties[_destinationLocationHeadingKey].ToString(), out heading))
                        {
                            Debug.Log("No property with key : " + _destinationLocationHeadingKey + "found!");
                        }
                    }

                    if (feature.Properties.ContainsKey("destination_type"))
                    {
                        locationColor = feature.Properties["destination_type"].ToString();
                    }

                    if (typeName == "phone-room")
                    {
                        locationColor = "phone-room";
                    }
                        
                    locationData.SetLocation(pointId, colName, locationColor, latitudeLongitude, heading);
                    Debug.Log(pointId + " " + colName + " " + locationColor + " " + latitudeLongitude + " " + heading);

                    Location.DestinationPointLocationProvider.Instance.Register(locationData);
                }
            }
                /*
                var featureName = ve.Feature.Properties["destination_type"].ToString();
                if (featureName == "conference-room")
                {
                    string planetName = ve.Feature.Properties["name"].ToString();
                    string prefabName = "Prefabs/" + "Saturn" + "Prefab";
                    //Debug.Log("PrefabName : " + prefabName);

                    var scale = tile.TileScale;
                    int selpos = ve.Feature.Points[0].Count / 2;
                    met = ve.Feature.Points[0][selpos];
                    var prefabGO = (GameObject)Instantiate(Resources.Load(prefabName));
                    prefabGO.name = prefabName;
                    //met.y = 7 * scale;
                    met.y = prefabGO.transform.Find(planetName + "Model").GetComponent<MeshRenderer>().bounds.extents.y + 2;
                    prefabGO.transform.position = met;
                    prefabGO.transform.SetParent(ve.GameObject.transform, false);

                }

                else if (featureName == "phone-room")
                {
                    string phoneRoomName = ve.Feature.Properties["name"].ToString();
                    string prefabName = "Prefabs/Phone";
                    //Debug.Log("PrefabName : " + prefabName);

                    var scale = tile.TileScale;
                    int selpos = ve.Feature.Points[0].Count / 2;
                    met = ve.Feature.Points[0][selpos];
                    var prefabGO = (GameObject)Instantiate(Resources.Load(prefabName));
                    prefabGO.name = prefabName;

                    prefabGO.transform.Find("Text").GetComponent<TextMesh>().text = phoneRoomName;
                    met.y = prefabGO.transform.Find("Model").GetComponent<MeshRenderer>().bounds.extents.y + 2;
                    prefabGO.transform.position = met;
                    prefabGO.transform.SetParent(ve.GameObject.transform, false);
                }
                */

            /*
            var destPosition = met;
			destPosition.y = 0;

			Location.DestinationPointData locationData = new Location.DestinationPointData();
			var feature = ve.Feature;

			int id = 0;
			float heading = 0.0f;
			string locationName = string.Empty;
			string latitudeLongitudeString;
			Vector2d latitudeLongitude = Vector2d.zero;
			string locationColor = string.Empty;

			if (feature.Properties.ContainsKey(_destinationLocationIdKey))
			{
				if (!int.TryParse(feature.Properties[_destinationLocationIdKey].ToString(), out id))
				{
					Debug.Log("No property with key : " + _destinationLocationIdKey + "found!");
				}
			}

			if (feature.Properties.ContainsKey(_destinationLocationNameKey))
			{
				locationName = feature.Properties[_destinationLocationNameKey].ToString();
				if (string.IsNullOrEmpty(locationName))
				{
					Debug.Log("No property with key : " + _destinationLocationNameKey + "found!");
				}
			}

			if (feature.Properties.ContainsKey(_destinationLocationPositionKey))
			{
				latitudeLongitudeString = feature.Properties[_destinationLocationPositionKey].ToString();
				latitudeLongitude = Conversions.StringToLatLon(latitudeLongitudeString);
			}

			if (feature.Properties.ContainsKey(_destinationLocationHeadingKey))
			{
				if (!float.TryParse(feature.Properties[_destinationLocationHeadingKey].ToString(), out heading))
				{
					Debug.Log("No property with key : " + _destinationLocationHeadingKey + "found!");
				}
			}

			if (feature.Properties.ContainsKey("color"))
			{
				locationColor = feature.Properties["color"].ToString();
			}
			if (featureName == "phone-room")
				locationColor = "phone-room";
			locationData.SetLocation(id, locationName, locationColor, latitudeLongitude, heading);

			Location.DestinationPointLocationProvider.Instance.Register(locationData);
            */
		}
	}
}
