using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class WeightedList<T> where T : IWeightedElement
{
    [JsonProperty]
    [SerializeField]
    private List<T> list;

    [JsonIgnore]
    public List<T> List { get => list; private set => list = value; }

    public WeightedList(List<T> standardList)
    {
        List = new List<T>(standardList);
    }

    public WeightedList()
    {
        List = new List<T>();
    }

    public T GetRandomWeightedElement()
    {
        if(List.All(weightedElement => weightedElement.Weight.Equals(0f)))
        {
            return List.GetRandomElement();
        }

        float weightsSum = List.Sum(weightedElement => weightedElement.Weight);
        float randomWeight = UnityEngine.Random.Range(0f, weightsSum);
        float currentWeightsSum = 0f;
        T randomWeightedElement = List.Find(weightedElement =>
        {
            currentWeightsSum += weightedElement.Weight;
            return currentWeightsSum >= randomWeight;
        });

        return randomWeightedElement;
    }
}
