using NUnit.Framework;
using TextClassificationWPF;
using TextClassificationWPF.Business;
using TextClassificationWPF.Domain;
using TextClassificationWPF.FileIO;
using TextClassificationWPF.Foundation;
using TextClassificationWPF.Controller;

namespace Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.AreNotEqual(true, false);
        }

        [Test]
        public void TestWordItemGetWord()
        {
            // arrange
            string expected = "nice";
            WordItem wI = new WordItem("nice", 0);

            // act
            string actual = wI.Word;

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestStringOperationsGetFileName()
        {
            // deprecated - use 
            // arrange
            string expected = "Suduko";
            string path = "c:\\users\\tha\\documents\\Suduko.txt";

            // act
            string actual = StringOperations.getFileName(path);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestFileGetAllFileNames()
        {
            // arrange
            string folderA = "ClassA";
            string fileType = "txt";
            List<string> expected = new List<string>();
            expected.Add("C:\\Users\\Bruger\\source\\repos\\My Work\\week 40\\TextClassificationWPF\\TextClassificationWPF\\bin\\Debug\\" + folderA + "\\bbcsportsfootball." + fileType);
            expected.Add("C:\\Users\\Bruger\\source\\repos\\My Work\\week 40\\TextClassificationWPF\\TextClassificationWPF\\bin\\Debug\\" + folderA + "\\dailymirrornfl." + fileType);
            expected.Add("C:\\Users\\Bruger\\source\\repos\\My Work\\week 40\\TextClassificationWPF\\TextClassificationWPF\\bin\\Debug\\" + folderA + "\\sunsportsboxing." + fileType);

            // act
            FileAdapter fa = new TextFile(fileType);
            List<string> actual = fa.GetAllFileNames(folderA);

            // assert
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
        }

        [Test]
        public void TestGetFilePathA()
        {
            // arrange
            string folderA = "ClassA";
            string fileType = "txt";
            string fileName = "filnavn";
            string expected = "C:\\Users\\Bruger\\source\\repos\\My Work\\week 40\\TextClassificationWPF\\TextClassificationWPF\\bin\\Debug\\" + folderA + "\\filnavn." + fileType;

            // act
            TextFile tf = new TextFile(fileType);
            string actual = tf.GetFilePathA(fileName);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestRemovePunctuation()
        {
            // arrange
            // Here we make the work to test and what we expect to get back
            string word = "\".......hello,.//?!\"\"\"\"\"\"";
            string expected = "hello";

            // act
            // Here we use the method we want to test, and create the result
            string actual = Tokenization.RemovePunctuation(word);
            Console.WriteLine(actual);

            // assert
            // Here we test the result with what we expect to get
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void TestVectorLenghtOne() {
            // arrange
            KnowledgeBuilder kb = new KnowledgeBuilder();
            kb.Train();
            Knowledge knowledge = kb.GetKnowledge();
            BagOfWords bagOfWords = knowledge.GetBagOfWords();

            int expected = bagOfWords.GetAllWordsInDictionary().Count();

            // act
            int actual = knowledge.GetVectors().GetVectorA()[0].Count();

            // assert
            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void TestVectorLenghtTwo()
        {
            // arrange
            KnowledgeBuilder kb = new KnowledgeBuilder();
            kb.Train();
            Knowledge knowledge = kb.GetKnowledge();
            BagOfWords bagOfWords = knowledge.GetBagOfWords();

            int expected = bagOfWords.GetAllWordsInDictionary().Count();

            // act
            int actual = knowledge.GetVectors().GetVectorA()[2].Count();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestVectorLenghtThree()
        {
            // arrange
            KnowledgeBuilder kb = new KnowledgeBuilder();
            kb.Train();
            Knowledge knowledge = kb.GetKnowledge();
            BagOfWords bagOfWords = knowledge.GetBagOfWords();

            int expected = bagOfWords.GetAllWordsInDictionary().Count();

            // act
            int actual = knowledge.GetVectors().GetVectorB()[0].Count();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestVectorLenghtForth()
        {
            // arrange
            KnowledgeBuilder kb = new KnowledgeBuilder();
            kb.Train();
            Knowledge knowledge = kb.GetKnowledge();
            BagOfWords bagOfWords = knowledge.GetBagOfWords();

            int expected = bagOfWords.GetAllWordsInDictionary().Count();

            // act
            int actual = knowledge.GetVectors().GetVectorB()[1].Count();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestVectorPosetionOne() {
            // arragne
            bool expected = true;

            // act
            KnowledgeBuilder kb = new KnowledgeBuilder();
            kb.Train();
            Knowledge knowledge = kb.GetKnowledge();
            bool actual = knowledge.GetVectors().GetVectorA()[0][1];

            // assert
            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void TestVectorPosetionTwo()
        {
            // arragne
            bool expected = false;

            // act
            KnowledgeBuilder kb = new KnowledgeBuilder();
            kb.Train();
            Knowledge knowledge = kb.GetKnowledge();
            bool actual = knowledge.GetVectors().GetVectorA()[0][0];

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestVectorPosetionThree()
        {
            // arragne
            bool expected = false;

            // act
            KnowledgeBuilder kb = new KnowledgeBuilder();
            kb.Train();
            Knowledge knowledge = kb.GetKnowledge();
            bool actual = knowledge.GetVectors().GetVectorB()[0][15];

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}