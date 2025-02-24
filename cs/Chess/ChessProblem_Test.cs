using System;
using System.IO;
using NUnit.Framework;

namespace Chess
{
    [TestFixture]
    public class ChessProblem_Test
    {
        [Test]
        public void RepeatedMethodCallDoNotChangeBehaviour()
        {
            var boardLines = new[]
            {
                "        ",
                "        ",
                "        ",
                "   q    ",
                "    K   ",
                " Q      ",
                "        ",
                "        ",
            };
            var ChessProblem = new ChessProblem();
            ChessProblem.LoadFrom(boardLines);
            ChessProblem.CalculateChessStatus(PieceColor.White);
            Assert.AreEqual(ChessStatus.Check, ChessProblem.ChessStatus, "first");

            // Now check that internal board modifications during the first call do not change answer
            ChessProblem.CalculateChessStatus(PieceColor.White);
            Assert.AreEqual(ChessStatus.Check, ChessProblem.ChessStatus, "second");
        }

        [Test]
        public void AllTests()
        {
            var dir = TestContext.CurrentContext.TestDirectory;
            var testsCount = 0;
            foreach (var filename in Directory.GetFiles(Path.Combine(dir, "ChessTests"), "*.in"))
            {
                TestOnFile(filename);
                testsCount++;
            }
            Console.WriteLine("Tests passed: " + testsCount);
        }

        private static void TestOnFile(string filename)
        {
            var boardLines = File.ReadAllLines(filename);
            var ChessProblem = new ChessProblem();
            ChessProblem.LoadFrom(boardLines);
            var expectedAnswer = File.ReadAllText(Path.ChangeExtension(filename, ".ans")).Trim();
            ChessProblem.CalculateChessStatus(PieceColor.White);
            Assert.AreEqual(expectedAnswer, ChessProblem.ChessStatus.ToString().ToLower(), "Failed test " + filename);
        }
    }
}