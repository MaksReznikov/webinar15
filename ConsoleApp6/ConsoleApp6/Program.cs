string word = GetRandomWord();
string secretWord = new string('*', word.Length);
int triesCount = 0;
int triesLeft = 7;
while (true)
{
    Console.WriteLine($"\nYour number of tries left: {triesLeft}");
    Console.WriteLine(secretWord);
    Console.Write("> ");
    string? guessed = Console.ReadLine();
    triesCount++;
    if (guessed.Length == 1)
        OpenLetters(ref secretWord, word, guessed);
    else if(guessed.Length != secretWord.Length)
        Console.WriteLine("Wrong length of word!");
    else
    {
        if (guessed == word)
            break;
        Console.WriteLine("Incorrect!");
    }
    if(word == secretWord)
        break;
    triesLeft--;
    if(triesLeft == 0)
        break;
}
if (triesLeft == 0)
{
    Console.WriteLine($"Game over! The word was \"{word}\"");
}
else
{
    Console.WriteLine($"You win! The word was \"{word}\". You won in {triesCount} tries.");
}
File.AppendAllText("../../../UsedWords.txt", $"{word}\n");


static void OpenLetters(ref string secretWord, string word, string guessed)
{
    int letterCount = 0;
    for (int i = 0; i < word.Length; i++)
    {
        if (word[i].ToString() == guessed)
        {
            char[] secretletters = secretWord.ToCharArray();
            secretletters[i] = guessed[0];
            secretWord = new string(secretletters);
            letterCount++;
        }
    }
    if (letterCount == 0)
    {
        Console.WriteLine("No, there is no such letter in this word");
    }
}

static string GetRandomWord()
{
    string path = "../../../Dictionary.txt";
    try
    {
        List<string> words = File.ReadAllLines(path).ToList();
        List<string> usedWords = File.ReadAllLines("../../../UsedWords.txt").ToList();
        usedWords.ForEach(w => words.Remove(w));
        if (words.Count == 0)
        {
            Console.WriteLine("There are no words left!\nReuse old words? (enter 'y' for use old words)");
            string answer = Console.ReadLine();
            if (answer.Trim().ToLower() == "y")
            {
                words.AddRange(usedWords);
                File.WriteAllText("../../../UsedWords.txt", "");
            }
            else
            {
                Console.WriteLine("Bye then!");
                Environment.Exit(0);
            }
        }
        return words[new Random().Next(0, words.Count)];
    }
    catch
    {
        Console.WriteLine("there is no file with words");
        File.WriteAllLines(path, new string[] {"beer", "brandy", "whiskey", "wine", "tea", "coffee", "dancing"});
        return GetRandomWord();
    }
}