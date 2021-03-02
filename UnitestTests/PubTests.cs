using NUnit.Framework;
using Unitest;
using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using AutofacContrib.NSubstitute;

namespace Unitest.Tests
{
    [TestFixture()]
    public class PubTests
    {
        List<Customer> _customerList;
        Pub pub;
        [SetUp]
        public void SetUp() 
        {
            AutoSubstitute _autoSubstitute = new AutoSubstitute();
            //主測試的Pub
            pub = _autoSubstitute.Resolve<Pub>();
            //測試所需要使用的隔離資料
            _autoSubstitute.Resolve<ICheckInFee>().GetFree(Arg.Any<Customer>()).Returns(100);
            _customerList = new List<Customer>
            {
                new Customer{ IsMale = true},
                new Customer{ IsMale = true},
                new Customer{ IsMale = false},
            };

            //使用arg.do 假裝有新增進資料
            _autoSubstitute.Resolve<ICheckInFee>().AddOne(Arg.Do<Customer>(o=>_customerList.Add(o)));
            _autoSubstitute.Resolve<ICheckInFee>().Del(Arg.Do<Customer>(o=>_customerList.Remove(o)));


        }
        [Test()]
        public void CheckInTest_確定需買票人數()
        {
            //arrange
            decimal expected = 2;
            //act
            var actual = pub.CheckIn(_customerList);
            //assert
            Assert.AreEqual(expected,actual);
        }
        [Test()]
        public void CheckInTest_確認收到金額為200()
        {
            //arrange
            var inComeBeforeCheckIn = pub.GetInCome;
            Assert.AreEqual(0,inComeBeforeCheckIn); //尚未測試前的人數 為0
            decimal expectedIncome = 200;
            //act
            var actual = pub.CheckIn(_customerList);
            var actualIncome = pub.GetInCome;
            //assert
            Assert.AreEqual(expectedIncome, actualIncome);
        }
        [Test()] 
        public void CheckInTest_Mock確認程式被執行次數()
        {
            //arrange
            ICheckInFee stubCheckInFree = Substitute.For<ICheckInFee>();
            stubCheckInFree.GetFree(Arg.Any<Customer>()).Returns(100);
            Pub pub = new Pub(stubCheckInFree);

            var customers = new List<Customer>
            {
                new Customer{ IsMale = true},
                new Customer{ IsMale = true},
                new Customer{ IsMale = false},
            };
            var inComeBeforeCheckIn = pub.GetInCome;
            Assert.AreEqual(0, inComeBeforeCheckIn);

            decimal expectedIncome = 200;
            //act
            var actual = pub.CheckIn(customers);
            var actualIncome = pub.GetInCome;
            //assert
            Assert.AreEqual(expectedIncome, actualIncome);
        }
    }
}