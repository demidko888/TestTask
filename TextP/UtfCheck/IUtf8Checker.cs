using System.IO;

namespace TextP.UtfCheck
{
    public interface IUtf8Checker
    {
        bool Check(string fileName);
        bool IsUtf8(Stream stream);
    }
}