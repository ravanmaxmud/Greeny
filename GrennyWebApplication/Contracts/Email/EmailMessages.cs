namespace GrennyWebApplication.Contracts.Email
{
    public static class EmailMessages
    {
        public static class Subject
        {
            public const string ACTIVATION_MESSAGE = $"Hesabin aktivlesdirilmesi";
            public const string ORDER_ACTIVATION_MESSAGE = $"Hesabin aktivdi qaqa";
            public const string CHANGEPASSWORD_MESSAGE = $"CHANGE PASSWORD";
        }

        public static class Body
        {
            public const string ACTIVATION_MESSAGE = $"Sizin activation urliniz : {EmailMessageKeywords.ACTIVATION_URL}";
            public const string CHANGEPASSWORD_MESSAGE = $"Sizin change urlniz : {EmailMessageKeywords.CHANGEPASSWORD_URL}";
        }
    }
}
