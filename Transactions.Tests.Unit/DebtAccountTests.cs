﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunsets.Transactions;
using Sunsets.Transactions.Accounts;

namespace Sunsets.Sunsets.Transactions.Tests.Unit
{
    [TestClass]
    public class DebtAccountTests
    {
        public static Calendar Calendar { get; } = new Calendar();

        public DebtAccount Account { get; set; } = new DebtAccount("Test");

        [TestClass]
        public class GetBalanceFromToday : DebtAccountTests
        {
            [TestMethod]
            public void Should_Decrease_Balance_From_Deposits()
            {
                Assert.AreEqual(-400, Account.GetDelta(400));
            }
        }
    }
}