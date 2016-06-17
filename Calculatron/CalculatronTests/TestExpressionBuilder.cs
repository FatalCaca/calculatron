using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculatron;
using System.Diagnostics;

namespace CalculatronTests
{
    /// <summary>
    /// Description résumée pour TestExpressionBuilder
    /// </summary>
    [TestClass]
    public class TestExpressionBuilder
    {
        public TestExpressionBuilder()
        {
            //
            // TODO: ajoutez ici la logique du constructeur
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: ajoutez ici la logique du test
            //
        }

        [TestMethod]
        public void AddTwoNumberAndGetResult()
        {
            float a = 2;
            float b = 1;

            BaseCalculator baseCalculator = new BaseCalculator();

            float actualSum = 3;

            float expectedSum = 0;

            expectedSum = baseCalculator.Addition(a, b);

            Assert.AreEqual(expectedSum, actualSum);
        }

        [TestMethod]
        public void SubtractTwoNumberAndGetResult()
        {
            float a = 2;
            float b = 1;

            BaseCalculator baseCalculator = new BaseCalculator();

            float actualSub = 1;

            float expectedSub = 0;

            expectedSub = baseCalculator.Substraction(a, b);

            Assert.AreEqual(expectedSub, actualSub);
        }

        [TestMethod]
        public void DivideTwoNumberAndGetResult()
        {
            float a = 10;
            float b = 2;

            BaseCalculator baseCalculator = new BaseCalculator();

            float actualDivide = 5;

            float expectedDivide= 0;

            expectedDivide = baseCalculator.Division(a, b);

            Assert.AreEqual(expectedDivide, actualDivide);
        }

        [TestMethod]
        public void MultipleTwoNumberAndGetResult()
        {
            float a = 10;
            float b = 2;

            BaseCalculator baseCalculator = new BaseCalculator();

            float actualMul = 20;

            float expectedMul = 0;

            expectedMul = baseCalculator.Multiplication(a, b);

            Assert.AreEqual(expectedMul, actualMul);
        }
        [TestMethod]
        public void SquareANumberAndGetResult()
        {
            float a = 5;
            
            BaseCalculator baseCalculator = new BaseCalculator();

            float actualSquare = 25;

            float expectedSquare = 0;
            expectedSquare = baseCalculator.Square(a);

            Assert.AreEqual(expectedSquare, actualSquare);
        }
    }

}
