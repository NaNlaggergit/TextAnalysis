using System.Text;

namespace TextAnalysis;

static class TextGeneratorTask
{
    public static string ContinuePhrase(
        Dictionary<string, string> nextWords,
        string phraseBeginning,
        int wordsCount)
    {
        StringBuilder lastWords=new StringBuilder();
        StringBuilder text = new StringBuilder();
        text.Append(phraseBeginning);
        for (int count = 0; count < wordsCount; count++)
        {
            var words = text.ToString().Split(" ");
            if (words.Length >=2)
            {
                string key = words[words.Length - 2] + " " + words[words.Length-1];
                if (nextWords.ContainsKey(key))
                {
                    text.Append(" "+nextWords[key]);
                }
                else 
                {
                    if (nextWords.ContainsKey(words[words.Length - 1]))
                        text.Append(" "+nextWords[words[words.Length - 1]]);
                    else return text.ToString();
                }
            }
            if(words.Length == 1)
            {
                if (nextWords.ContainsKey(words[words.Length - 1]))
                    text.Append(" "+ nextWords[words[words.Length - 1]]);
                else return text.ToString();
            }
            if (words.Length == 0)
                break;
        }
        return text.ToString();
    }
}