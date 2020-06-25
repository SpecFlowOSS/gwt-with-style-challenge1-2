namespace UserRepository
{
    public class User
    {
        public string UserName { get; set; }

        public string PersonalName { get; set; }

        public string Email { get; set; }

        public override string ToString()
        {
            return $"{PersonalName} ({UserName} - {Email})";
        }
    }
}
