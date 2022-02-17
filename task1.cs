using System;
using System.IO;
using System.Text;

namespace Lab1
{
    class Task1
    {
        struct Pair
        {
            public string Word;
            public int Count;
        };

        static void Main(string[] args)
        {
            const int showWords = 25;
            int wordsLength = 0;
            int wordsCapacity = 5;
            Pair[] words = new Pair[wordsCapacity];

            string currentWord = "";
            string[] stopWords =
            {
                "the", "in", "on", "a",
                "an", "of", "at", "by",
                "are", "but", "is"
            };
            int j = 0;
            bool foundInStopWords = false;
            int wordIndex = 0;
            string inputText = File.ReadAllText("input1.txt");

            int inputLength = 0;
            int inputWordLength = 0;
            input:
            if ((inputText[inputLength] == ' ' || inputText[inputLength] == '\r' || inputText[inputLength] == '\n')
                && (inputLength != 0 && (inputText[inputLength - 1] != ' ' || inputText[inputLength - 1] != '\r' ||
                                         inputText[inputLength - 1] != '\n')))

            {
                inputWordLength++;
            }

            inputLength++;
            if (inputLength != inputText.Length - 1)
                goto input;

            string[] wordsInFile = new string[inputWordLength];


            int splitIndex = 0;
            int arrayIndex = 0;
            string splitWord = "";
            split:
            if ((inputText[splitIndex] == ' ' || inputText[splitIndex] == '\r' || inputText[splitIndex] == '\n'))
            {
                if (splitWord.Length != 0)
                {
                    wordsInFile[arrayIndex++] = splitWord;
                    splitWord = "";
                }
            }
            else
            {
                if (inputText[splitIndex] >= (int) 'A' && inputText[splitIndex] <= (int) 'Z' ||
                    inputText[splitIndex] >= (int) 'a' && inputText[splitIndex] <= (int) 'z')
                {
                    if (inputText[splitIndex] >= (int) 'A' && inputText[splitIndex] <= (int) 'Z')
                    {
                        splitWord += (char) (inputText[splitIndex] + 32);
                    }
                    else
                    {
                        splitWord += inputText[splitIndex];
                    }
                }
            }

            splitIndex++;
            if (splitIndex != inputText.Length)
                goto split;

            whileTrue:
            if (wordIndex >= wordsInFile.Length)
                goto afterWhileTrue;
            currentWord = wordsInFile[wordIndex];
            wordIndex++;
            if (wordIndex > wordsInFile.Length)
            {
                goto afterWhileTrue;
            }

            j = 0;
            whileToLowerCase:
            if (currentWord[j] == '\0')
            {
                if (j > 0 && !(currentWord[j - 1] >= 'A' && currentWord[j - 1] <= 'Z')
                          && !(currentWord[j - 1] >= 'a' && currentWord[j - 1] <= 'z'))
                {
                    StringBuilder s = new StringBuilder(currentWord);
                    s[j - 1] = '\0';
                    currentWord = s.ToString();
                }

                goto afterWhileToLowerCase;
            }

            if (currentWord[j] <= 'Z' && currentWord[j] >= 'A')
            {
                StringBuilder s = new StringBuilder(currentWord);
                s[j] += (char) ('a' - 'A');
                currentWord = s.ToString();
            }

            j++;
            if (j < currentWord.Length)
                goto whileToLowerCase;
            afterWhileToLowerCase:


            j = 0;
            foundInStopWords = false;
            filterStopWords:
            if (j >= stopWords.Length)
            {
                goto afterFilterStopWords;
            }

            if (currentWord == stopWords[j])
            {
                foundInStopWords = true;
            }

            j++;
            goto filterStopWords;
            afterFilterStopWords:


            if (!foundInStopWords)
            {
                bool found = false;
                j = 0;
                countingSimilars:
                if (j >= wordsLength)
                {
                    goto afterCountingSimilars;
                }

                if (words[j].Word == currentWord)
                {
                    words[j].Count++;
                    found = true;
                }

                j++;
                goto countingSimilars;
                afterCountingSimilars:
                if (!found)
                {
                    if (wordsLength + 1 > wordsCapacity)
                    {
                        wordsCapacity *= 2;
                        Pair[] tmpWords = new Pair[wordsCapacity];
                        int z = 0;
                        copyingWords:
                        if (z >= wordsLength)
                        {
                            goto afterCopyingWords;
                        }

                        tmpWords[z] = words[z];
                        z++;
                        goto copyingWords;
                        afterCopyingWords:
                        words = tmpWords;
                    }

                    words[wordsLength] = new Pair();
                    words[wordsLength].Word = currentWord;
                    words[wordsLength].Count = 1;
                    wordsLength++;
                }
            }

            goto whileTrue;
            afterWhileTrue:

            int i;
            i = 0;
            upperFor:
            if (i >= wordsLength - 1)
            {
                goto afterUpperFor;
            }

            j = i + 1;
            innerFor:
            if (j >= wordsLength)
            {
                goto afterInnerFor;
            }

            if (words[i].Count < words[j].Count)
            {
                Pair tmp;
                tmp = words[i];
                words[i] = words[j];
                words[j] = tmp;
            }

            j++;
            goto innerFor;
            afterInnerFor:
            i++;
            goto upperFor;
            afterUpperFor:

            i = 0;
            outputCycle:
            if (!(i < wordsLength && i < showWords))
            {
                goto finish;
            }

            Console.WriteLine(words[i].Word + " " + words[i].Count);
            i++;
            goto outputCycle;
            finish:
            return;
        }
    }
}