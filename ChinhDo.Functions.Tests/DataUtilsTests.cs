﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ChinhDo.Functions.Tests
{
    [TestClass]
    public class DataUtilsTests
    {
        [TestMethod]
        public void CanConvertListToDataTable()
        {
            List<Person> persons = new List<Person>();
            persons.Add(new Person() { FirstName = "John", LastName="Smith", Created=DateTime.Now });
            persons.Add(new Person() { FirstName = "Jane", LastName = "Smith", Created = DateTime.Now });
            DataTable table = DataUtils.ToDataTable(persons);

            Assert.AreEqual(table.Columns.Count, 3);
            Assert.AreEqual(table.Columns[0].ColumnName, "FirstName");
            Assert.AreEqual(table.Columns[0].DataType, typeof(string));

            Assert.AreEqual(table.Columns[2].ColumnName, "Created");
            Assert.AreEqual(table.Columns[2].DataType, typeof(DateTime));

            Assert.AreEqual(table.Rows.Count, 2);
        }

        [TestMethod]
        public void GetBreakListIntoChunks()
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 1000; i++)
            {
                persons.Add(new Person() { FirstName = "John", LastName = "Smith", Created = DateTime.Now });
            }
            List<List<Person>> chunks = DataUtils.BreakIntoChunks(persons, 100);
            Assert.AreEqual(10, chunks.Count);
            Assert.AreEqual(100, chunks[0].Count);
        }
    }

    public class Person
    {
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public DateTime Created { get; set; }
    }
}
