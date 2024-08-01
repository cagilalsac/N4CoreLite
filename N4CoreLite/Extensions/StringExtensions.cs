namespace N4CoreLite.Extensions
{
    public static class StringExtensions
    {
        public static string ChangeTurkishCharactersToEnglish(this string turkishValue)
        {
            string englishValue = string.Empty;
            if (!string.IsNullOrWhiteSpace(turkishValue))
            {
                Dictionary<string, string> characterDictionary = new Dictionary<string, string>()
                {
                    { "Ö", "O" },
                    { "Ç", "C" },
                    { "Ş", "S" },
                    { "Ğ", "G" },
                    { "Ü", "U" },
                    { "ö", "o" },
                    { "ç", "c" },
                    { "ş", "s" },
                    { "ğ", "g" },
                    { "ü", "u" },
                    { "İ", "I" },
                    { "ı", "i" }
                };
                foreach (char character in turkishValue)
                {
                    if (characterDictionary.ContainsKey(character.ToString()))
                        englishValue += characterDictionary[character.ToString()];
                    else
                        englishValue += character.ToString();
                }
            }
            return englishValue;
        }
    }
}
