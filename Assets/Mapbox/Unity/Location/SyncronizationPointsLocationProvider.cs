namespace Mapbox.Unity.Location
{
	using UnityEngine;
	using System.Collections.Generic;
	using IndoorMappingDemo;

	public class SyncronizationPointsLocationProvider : AbstractLocationProvider
	{
        int input_roomNum = 6;
        float time = 0;
		private object _syncLock = new object();
		Dictionary<int, IFixedLocation> _syncronizationPoints = new Dictionary<int, IFixedLocation>();
		bool _isUISetupComplete = false;
		private void Awake()
		{
			if (_syncronizationPoints != null)
			{
				_syncronizationPoints = new Dictionary<int, IFixedLocation>();
			}
		}

		protected int Count
		{
			get
			{
				lock (_syncLock)
				{
					return _syncronizationPoints.Count;
				}
			}
		}
        protected void Enqueue(IFixedLocation locationProvider)
        {
            lock (_syncLock)
            {
                if (!_syncronizationPoints.ContainsKey(locationProvider.LocationId))
                {

                    Debug.Log("locationid : " + locationProvider.LocationId);
                    if (locationProvider.LocationId == input_roomNum)
                    {
                        Debug.Log("Registering id : " + locationProvider.LocationId);
                        _syncronizationPoints.Add(locationProvider.LocationId, locationProvider);
                    }
                }
            }
        }
        /*
		protected void Enqueue(IFixedLocation locationProvider)
		{
			lock (_syncLock)
			{
				if (!_syncronizationPoints.ContainsKey(locationProvider.LocationId))
				{
					Debug.Log("Registering id : " + locationProvider.LocationId);
					_syncronizationPoints.Add(locationProvider.LocationId, locationProvider);
				}
			}
		}*/

        public void Register(IFixedLocation locationProvider)
		{
			Enqueue(locationProvider);
		}

		private void Update()

		{
            if (Count == 0 || _isUISetupComplete == true)
            {
                return;
            }
            else
            {
                time += Time.deltaTime;
                if (time >= 1.0f)
                {
                    ApplicationUIManager.Instance.AddToSyncPointUI(input_roomNum, _syncronizationPoints[input_roomNum].LocationName, OnSyncRequested);

                    _isUISetupComplete = true;
                }
            }
            /*
                //HACK : To add buttons in increasing order. 

                int input_roomNum = 6;

                if (Count < 8 || _isUISetupComplete == true)
                {
                    return;
                }
                else
                {
                    for (int i = 0; i < Count; i++)
                    {
                        if (i == input_roomNum)
                        {
                            ApplicationUIManager.Instance.AddToSyncPointUI(i, _syncronizationPoints[i].LocationName, OnSyncRequested);

                        }
                    }
                    _isUISetupComplete = true;
                }*/
        }

		public void OnSyncRequested(int id)
		{
			Debug.Log("Pressed button");
			SendLocation(_syncronizationPoints[id].CurrentLocation);
			ApplicationUIManager.Instance.OnStateChanged(ApplicationState.SyncPoint_Calibration);
		}
	}
}