namespace work_10.Authorization
{
    public static class Policies
    {
        public const string AdminOnly = "AdminOnly";
        public const string SameUserOrAdmin = "SameUserOrAdmin";
    }
}