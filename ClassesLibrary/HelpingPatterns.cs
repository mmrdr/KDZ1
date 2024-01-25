namespace ClassesLibrary
{
    /// <summary>
    /// Only usefull patterns and some usefull fields, like "currentAppUsers".
    /// </summary>
    public partial class HelpingMethods
    {
        internal const string TAB = "  ";

        internal static string file_path;

        internal static List<AppUser> currentAppUsers;

        internal static string namePattern = @"^[\p{L}\s.'-]+$";

        internal static string emailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

        internal static string cityPattern = @"\b\p{Lu}\p{L}*\b";

        internal static string doublesPattern = @"^(\d+([.,]\d+)?(:\d+([.,]\d+)?)+)$";
    }
}
