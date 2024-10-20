using System.Text;
using System.Text.RegularExpressions;

namespace TextAnalysis;

static class SentencesParserTask
{
    public static List<List<string>> ParseSentences(string text)
    {
        var sentencesList = new List<List<string>>();
        text = text.Replace("...", "");
        var sentence = text.Split(new string[] { "!", "?", ":", ";", "(", ")","." }, StringSplitOptions.RemoveEmptyEntries);
        StringBuilder word=new StringBuilder();
        for (int i = 0; i < sentence.Length; i++)
        {
            List<string> words = new List<string>();
            for(int j = 0; j < sentence[i].Length; j++)
            {
                if (char.IsLetter(sentence[i][j])|| sentence[i][j]=='\'')
                    word.Append(sentence[i][j]);
                else if (word.Length > 0)
                {
                    words.Add(word.ToString().ToLower());
                    word.Clear();
                }
            }
            if (word.Length > 0)
            {
                words.Add(word.ToString().ToLower());
                word.Clear();
            }
            if(words.Count>0)
                sentencesList.Add(words);
        }
        return sentencesList;
    }
}