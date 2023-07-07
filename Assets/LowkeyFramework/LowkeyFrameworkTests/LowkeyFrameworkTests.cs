using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LowkeyFrameworkTests
{
    [Test]
    public void WeightedListBasicWithIntegerElementsWeightsTest()
    {
        int testsNum = 100000;
        WeightedElement element1 = new WeightedElement(1f);
        WeightedElement element3 = new WeightedElement(3f);
        WeightedElement element6 = new WeightedElement(6f);

        Dictionary<WeightedElement, int> chosenElementNums = new Dictionary<WeightedElement, int>();
        chosenElementNums.Add(element1, 0);
        chosenElementNums.Add(element3, 0);
        chosenElementNums.Add(element6, 0);

        WeightedList<WeightedElement> weightedList = new WeightedList<WeightedElement>(new List<WeightedElement> 
        {
            element1,
            element3,
            element6,
        });

        for(int i = 0; i < testsNum; i++)
        {
            WeightedElement chosenElement = weightedList.GetRandomWeightedElement();
            chosenElementNums[chosenElement]++;
        }

        Assert.GreaterOrEqual(chosenElementNums[element1], 9000, "Element with a weight of 1");
        Assert.LessOrEqual(chosenElementNums[element1], 11000, "Element with a weight of 1");
        Assert.GreaterOrEqual(chosenElementNums[element3], 29000, "Element with a weight of 3");
        Assert.LessOrEqual(chosenElementNums[element3], 31000, "Element with a weight of 3");
        Assert.GreaterOrEqual(chosenElementNums[element6], 59000, "Element with a weight of 6");
        Assert.LessOrEqual(chosenElementNums[element6], 61000, "Element with a weight of 6");
    }

    [Test]
    public void WeightedListWith0ElementWeightTest()
    {
        int testsNum = 100000;
        WeightedElement element4 = new WeightedElement(4f);
        WeightedElement element0 = new WeightedElement(0f);
        WeightedElement element6 = new WeightedElement(6f);

        Dictionary<WeightedElement, int> chosenElementNums = new Dictionary<WeightedElement, int>();
        chosenElementNums.Add(element4, 0);
        chosenElementNums.Add(element0, 0);
        chosenElementNums.Add(element6, 0);

        WeightedList<WeightedElement> weightedList = new WeightedList<WeightedElement>(new List<WeightedElement>
        {
            element4,
            element0,
            element6,
        });

        for (int i = 0; i < testsNum; i++)
        {
            WeightedElement chosenElement = weightedList.GetRandomWeightedElement();
            chosenElementNums[chosenElement]++;
        }

        Assert.GreaterOrEqual(chosenElementNums[element4], 39000, "Element with a weight of 4f");
        Assert.LessOrEqual(chosenElementNums[element4], 41000, "Element with a weight of 4f");
        Assert.AreEqual(chosenElementNums[element0], 0, "Element with a weight of 0f");
        Assert.GreaterOrEqual(chosenElementNums[element6], 59000, "Element with a weight  of 6f");
        Assert.LessOrEqual(chosenElementNums[element6], 61000, "Element with a weight of 6f");
    }

    [Test]
    public void WeightedListWithFloatElementWeights()
    {
        int testsNum = 100000;
        WeightedElement element37 = new WeightedElement(3.7f);
        WeightedElement element20 = new WeightedElement(2f);
        WeightedElement element43 = new WeightedElement(4.3f);

        Dictionary<WeightedElement, int> chosenElementNums = new Dictionary<WeightedElement, int>();
        chosenElementNums.Add(element37, 0);
        chosenElementNums.Add(element20, 0);
        chosenElementNums.Add(element43, 0);

        WeightedList<WeightedElement> weightedList = new WeightedList<WeightedElement>(new List<WeightedElement>
        {
            element37,
            element20,
            element43,
        });

        for (int i = 0; i < testsNum; i++)
        {
            WeightedElement chosenElement = weightedList.GetRandomWeightedElement();
            chosenElementNums[chosenElement]++;
        }

        Assert.GreaterOrEqual(chosenElementNums[element37], 36000, "Element with a weight of 3.7f");
        Assert.LessOrEqual(chosenElementNums[element37], 38000, "Element with a weight of 3.7f");
        Assert.GreaterOrEqual(chosenElementNums[element20], 19000, "Element with a weight of 2f");
        Assert.LessOrEqual(chosenElementNums[element20], 21000, "Element with a weight of 2f");
        Assert.GreaterOrEqual(chosenElementNums[element43], 42000, "Element with a weight of 4.3f");
        Assert.LessOrEqual(chosenElementNums[element43], 44000, "Element with a weight of 4.3f");
    }

    [Test]
    public void WeightedList0WeightOnAllElements()
    {
        int testsNum = 100000;
        WeightedElement element0 = new WeightedElement(0f);
        WeightedElement element00 = new WeightedElement(0f);
        WeightedElement element000 = new WeightedElement(0f);

        Dictionary<WeightedElement, int> chosenElementNums = new Dictionary<WeightedElement, int>();
        chosenElementNums.Add(element0, 0);
        chosenElementNums.Add(element00, 0);
        chosenElementNums.Add(element000, 0);

        WeightedList<WeightedElement> weightedList = new WeightedList<WeightedElement>(new List<WeightedElement>
        {
            element0,
            element00,
            element000,
        });

        for (int i = 0; i < testsNum; i++)
        {
            WeightedElement chosenElement = weightedList.GetRandomWeightedElement();
            chosenElementNums[chosenElement]++;
        }

        Assert.GreaterOrEqual(chosenElementNums[element0], 32333, "First element with a weight of 0f");
        Assert.LessOrEqual(chosenElementNums[element0], 34333, "First element with a weight of 0f");
        Assert.GreaterOrEqual(chosenElementNums[element00], 32333, "Second element with a weight of 0f");
        Assert.LessOrEqual(chosenElementNums[element00], 34333, "Second element with a weight of 0f");
        Assert.GreaterOrEqual(chosenElementNums[element000], 32333, "Third element with a weight of 0f");
        Assert.LessOrEqual(chosenElementNums[element000], 34333, "Third element with a weight of 0f");
    }

    private class WeightedElement : IWeightedElement
    {
        private readonly float weight = 1f;

        public float Weight => weight;

        public WeightedElement(float weight)
        {
            this.weight = weight;
        }
    }
}
