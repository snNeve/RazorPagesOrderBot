using System;
using System.IO;
using Xunit;
using OrderBot;
using Microsoft.Data.Sqlite;

namespace OrderBot.tests
{
    public class OrderBotTest
    {
        public OrderBotTest()
        {
            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        DELETE FROM orders
    ";
                commandUpdate.ExecuteNonQuery();
            }
        }
            [Fact]
           public   void Test1()
            {

            }
            [Fact]
            public void TestWelcome()
            {
                Session oSession = new Session("12345");
                String sInput = oSession.OnMessage("hello")[0];
                Assert.True(sInput.Contains("Welcome"));
            }
            [Fact]
            public void TestDental()
            {
                Session oSession = new Session("12345");
                String sInput = oSession.OnMessage("hello")[0];
                Assert.True(sInput.ToLower().Contains("dental"));
            }
            [Fact]
            public void TestAppointment()
            {
                Session oSession = new Session("12345");
                String sInput = oSession.OnMessage("hello")[1];
                Assert.True(sInput.ToLower().Contains("appointment"));
            }
            [Fact]
            public void TestBook()
            {
                Session oSession = new Session("12345");
                oSession.OnMessage("hello");
                String sInput = oSession.OnMessage("book")[0];
                Assert.True(sInput.ToLower().Contains("service"));
                Assert.True(sInput.ToLower().Contains("checkup"));
            }
            [Fact]
            public void TestService()
            {
                string sPath = DB.GetConnectionString();
                Session oSession = new Session("12345");
                oSession.OnMessage("hello");
                oSession.OnMessage("book");
                String sInput = oSession.OnMessage("checkup")[0];
                Assert.True(sInput.ToLower().Contains("date"));
                Assert.True(sInput.ToLower().Contains("appointment"));
                Assert.True(sInput.ToLower().Contains("booked"));
            }
        }
    }
