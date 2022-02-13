namespace Lib
{
    /// <summary>
    /// Abstract class which contains notion and number in strings
    /// </summary>
    public abstract class Translator
    {
        protected int _Notion = 0;
        protected string _Number = "";

        public Translator(int notion, string number)
        {
            _Notion = notion;
            _Number = number;
        }
    }
}
