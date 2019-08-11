using exam_210419.Pages.DatepickerPage;
using exam_210419.Pages.DroppablePage;
using exam_210419.Pages.HomePage;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace exam_210419
{
    public class SeleniumTests
    {
        private ChromeDriver driver;
        private WebDriverWait wait;
        private Homepage Homepage;
        private Droppablepage Droppablepage;
        private Datepickerpage Datepickerpage;

        public object Integer { get; private set; }

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            Homepage = new Homepage(driver);
            Droppablepage = new Droppablepage(driver);
            Datepickerpage = new Datepickerpage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void TestElementDraggingToTarget()
        {
            //Load home page
            Homepage.Navigate();
            Thread.Sleep(2000);

            //Find droppable element and drag to target
            IWebElement droppableLink = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[1]/aside[1]/ul/li[4]/a"));
            droppableLink.Click();
            IWebElement droppableElement = driver.FindElement(By.CssSelector("#draggable"));
            IWebElement targetElement = driver.FindElement(By.CssSelector("#droppable"));
            droppableElement.Click();

            Actions action = new Actions(driver);
            action.DragAndDrop(droppableElement, targetElement).Build().Perform();
            Thread.Sleep(60);
            String colorBackground = targetElement.GetCssValue("background-color");
            String colorText = targetElement.GetCssValue("color");
            
            //IWebElement targetElementText = driver.FindElement(By.CssSelector("#droppable>p:nth-child(1)"));
            //var colorText = targetElementText.GetProperty("colortext");
            //String[] hexValue = color.Replace("rgba(", "").Replace(")", "").Split(",");
            //int hexValue1 = (int.Parse)(hexValue[0]);
            //hexValue[1] = hexValue[1].Trim();
            //int hexValue2 = (int.Parse)(hexValue[1]);
            //hexValue[2] = hexValue[2].Trim();
            //int hexValue3 = (int.Parse)(hexValue[2]);

            //String actualColor = String.Format("#%02x%02x%02x", hexValue1, hexValue2, hexValue3);

            //Assert
            Assert.AreEqual("rgba(255, 250, 144, 1)", colorBackground);
            Assert.AreEqual("rgba(119, 118, 32, 1)", colorText);
        }

        [Test]
        public void TestElementDraggingAnywhereOnPage()
        {
            //Load home page
            Homepage.Navigate();
            Thread.Sleep(2000);

            //Find droppable element and drag to target
            IWebElement droppableLink = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[1]/aside[1]/ul/li[4]/a"));
            droppableLink.Click();
            IWebElement droppableElement = driver.FindElement(By.CssSelector("#draggable"));
            IWebElement targetElement = driver.FindElement(By.CssSelector("#droppable"));
            droppableElement.Click();

            Actions action = new Actions(driver);
            action.DragAndDropToOffset(droppableElement, 0,200).Build().Perform();
            Thread.Sleep(600);
            String colorBackground = targetElement.GetCssValue("background-color");
            String colorText = targetElement.GetCssValue("color");

            //Assert
            Assert.AreNotEqual("rgba(255, 250, 144, 1)", colorBackground);
            Assert.AreNotEqual("rgba(119, 118, 32, 1)", colorText);
        }

        [Test]
        public void TestSelectDateApril21()
        {
            //Load home page
            Homepage.Navigate();
            Thread.Sleep(2000);

            //Find calendar and pick date 21/04/2019
            IWebElement datepickerLink = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[1]/aside[2]/ul/li[9]/a"));
            datepickerLink.Click();
            IWebElement datepickerElement = driver.FindElement(By.Id("datepicker"));
            datepickerElement.Click();
            Thread.Sleep(60);

            IWebElement dateBox = driver.FindElement(By.XPath("/html/body/div[6]/table/tbody/tr[4]/td[1]/a"));
            dateBox.Click();
            var date = dateBox.Text;

            IWebElement month = driver.FindElement(By.XPath("/html/body/div[6]/div/div/span[1]"));
            IWebElement year = driver.FindElement(By.XPath("/html/body/div[6]/div/div/span[2]"));
            var monthCheck = month.Text;
            var yearCheck = year.Text;

            //Assert date is 21/04/2019
            Assert.AreEqual("21", date);
            Assert.AreEqual("April", monthCheck);
            Assert.AreEqual("2019", yearCheck);
        }

        [Test]
        public void TestSelectPassedDate()
        {
            //Load home page
            Homepage.Navigate();
            Thread.Sleep(2000);

            //Find calendar and pick date 21/03/2019
            IWebElement datepickerLink = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[1]/aside[2]/ul/li[9]/a"));
            datepickerLink.Click();
            IWebElement datepickerElement = driver.FindElement(By.Id("datepicker"));
            datepickerElement.Click();
            Thread.Sleep(60);

            IWebElement navigateLeft = driver.FindElement(By.XPath("/html/body/div[6]/div/a[1]/span"));
            navigateLeft.Click();
            Thread.Sleep(60);

            IWebElement dateBox = driver.FindElement(By.XPath("/html/body/div[6]/table/tbody/tr[4]/td[5]/a"));
            dateBox.Click();
            var date = dateBox.Text;

            IWebElement month = driver.FindElement(By.XPath("/html/body/div[6]/div/div/span[1]"));
            IWebElement year = driver.FindElement(By.XPath("/html/body/div[6]/div/div/span[2]"));
            var monthCheck = month.Text;
            var yearCheck = year.Text;

            //Assert date is 21/04/2019
            Assert.AreEqual("21", date);
            Assert.AreEqual("March", monthCheck);
            Assert.AreEqual("2019", yearCheck);
        }

        [Test]
        public void TestSelectFutureDate()
        {
            //Load home page
            Homepage.Navigate();
            Thread.Sleep(2000);

            //Find calendar and pick date 21/05/2019
            IWebElement datepickerLink = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[1]/aside[2]/ul/li[9]/a"));
            datepickerLink.Click();
            IWebElement datepickerElement = driver.FindElement(By.Id("datepicker"));
            datepickerElement.Click();
            Thread.Sleep(60);

            IWebElement navigateRight = driver.FindElement(By.XPath("/html/body/div[6]/div/a[2]/span"));
            navigateRight.Click();
            Thread.Sleep(60);

            IWebElement dateBox = driver.FindElement(By.XPath("/html/body/div[6]/table/tbody/tr[4]/td[3]/a"));
            dateBox.Click();
            var date = dateBox.Text;

            IWebElement month = driver.FindElement(By.XPath("/html/body/div[6]/div/div/span[1]"));
            IWebElement year = driver.FindElement(By.XPath("/html/body/div[6]/div/div/span[2]"));
            var monthCheck = month.Text;
            var yearCheck = year.Text;

            //Assert date is 21/04/2019
            Assert.AreEqual("21", date);
            Assert.AreEqual("May", monthCheck);
            Assert.AreEqual("2019", yearCheck);
        }
    }
}
