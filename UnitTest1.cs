using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SelfShunt
{

    public class Checkout
    {
        private readonly ITotalDisplay _totalDisplay;
        private readonly IDictionary<string, int> _prices;
        private int _total = 0;

        public Checkout(ITotalDisplay totalDisplay, IDictionary<string,int> prices )
        {
            _totalDisplay = totalDisplay;
            _prices = prices;
        }

        public void Scan(string item)
        {
            _total += _prices[item];
            _totalDisplay.Update(_total);
        }
    }

    public interface ITotalDisplay
    {
        void Update(int newValue);
    }

    [TestClass]
    public class UnitTest1 : ITotalDisplay
    {
        private int _displayValue;
        private Dictionary<string, int> _prices = new Dictionary<string, int>{{"A", 50}, {"B",40}}; 

        public void Update(int newValue)
        {
            _displayValue = newValue;
        }

        [TestMethod]
        public void Test_scanning_one_item()
        {
            CheckPriceForItemIsCorrect(new Checkout(this, _prices), "A");
            CheckPriceForItemIsCorrect(new Checkout(this, _prices), "B");
        }

        [TestMethod]
        public void Test_two_As_are_100()
        {
            var checkout = new Checkout(this,_prices);
            checkout.Scan("A");
            checkout.Scan("A");
            Assert.AreEqual(_displayValue, 100);
        }


        private void CheckPriceForItemIsCorrect(Checkout checkout, string item)
        {
            checkout.Scan(item);
            Assert.AreEqual(_displayValue, _prices[item]);
        }
    }
}
