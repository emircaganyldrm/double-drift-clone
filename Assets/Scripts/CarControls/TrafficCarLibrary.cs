using Car;
using UnityEngine;

[CreateAssetMenu(fileName = "TrafficCarLibrary", menuName = "Create/Traffic Car Library", order = 0)]
public class TrafficCarLibrary : ScriptableObject
{
    public TrafficCarController[] trafficCarControllers;
    
    public TrafficCarController GetRandomCar()
    {
        int randomIndex = Random.Range(0, trafficCarControllers.Length);
        return trafficCarControllers[randomIndex];
    }
}