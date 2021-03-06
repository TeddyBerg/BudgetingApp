﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sunsets.Transactions.Tests.Unit.RecurringTransactionTests
{

    [TestClass]
    public class GetValueBetweenDates
    {
        RecurringTransactionTester Tester { get; } = new RecurringTransactionTester();

        [TestMethod]
        public void Should_ReturnZero_If_Ends_BeforeStart()
        {
            RecurringTransaction t = new RecurringTransaction(new Income(500), Tester.MockFrequency.Object, new DateTime(2019, 1, 1), null);

            Assert.AreEqual(0, t.GetValueBetweenDates(new DateTime(2018, 1, 1), new DateTime(2018, 1, 30)));
        }

        [TestMethod]
        public void Should_ReturnZero_If_Starts_AfterEnd()
        {
            RecurringTransaction t = new RecurringTransaction(new Income(500), Tester.MockFrequency.Object, new DateTime(2019, 1, 1), new DateTime(2019, 1, 30));

            Assert.AreEqual(0, t.GetValueBetweenDates(new DateTime(2019, 2, 1), new DateTime(2019, 2, 24)));
        }

        [TestMethod]
        public void Should_ComputeValue_From_Dates()
        {
            RecurringTransaction t = new RecurringTransaction(new Income(500), Tester.MockFrequency.Object, new DateTime(2019, 1, 1), new DateTime(2019, 1, 30));

            Tester.MockFrequency.DateCollection.Add(new DateTime(2019, 1, 4));
            Tester.MockFrequency.DateCollection.Add(new DateTime(2019, 1, 11));

            Assert.AreEqual(1000, t.GetValueBetweenDates(new DateTime(2019, 1, 1), new DateTime(2019, 2, 24)));
        }

        [TestMethod]
        public void Should_TruncateElapsedEventsRequest_If_StartDate_Is_AfterFromDate()
        {
            RecurringTransaction t = new RecurringTransaction(new Income(500), Tester.MockFrequency.Object, new DateTime(2019, 1, 1), new DateTime(2019, 1, 30));

            Tester.MockFrequency.DateCollection.Add(new DateTime(2018, 1, 4));
            Tester.MockFrequency.DateCollection.Add(new DateTime(2019, 1, 11));

            Assert.AreEqual(500, t.GetValueBetweenDates(new DateTime(2018, 1, 1), new DateTime(2019, 2, 24)));
        }

        [TestMethod]
        public void Should_TruncateElapsedEventsRequest_If_End_Is_BeforeToDate()
        {
            RecurringTransaction t = new RecurringTransaction(new Income(500), Tester.MockFrequency.Object, new DateTime(2019, 1, 1), new DateTime(2019, 1, 30));

            Tester.MockFrequency.DateCollection.Add(new DateTime(2019, 1, 4));
            Tester.MockFrequency.DateCollection.Add(new DateTime(2020, 1, 11));

            Assert.AreEqual(500, t.GetValueBetweenDates(new DateTime(2018, 1, 1), new DateTime(2020, 2, 24)));
        }
    }
}