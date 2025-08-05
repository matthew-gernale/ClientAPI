
namespace ClientAPI.Server.Utils
{
    public class TimeUtils
    {

        private static readonly TimeZoneInfo PhilippineTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Manila");

        public static DateTime PHTime() => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, PhilippineTimeZone);

        public static DateTime ToPHTime(DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return PHTime();

            var phNow = PHTime();

            var localDateWithPHTime = new DateTime(
                dateTime.Value.Year,
                dateTime.Value.Month,
                dateTime.Value.Day,
                phNow.Hour,
                phNow.Minute,
                phNow.Second,
                DateTimeKind.Unspecified
            );

            return TimeZoneInfo.ConvertTime(localDateWithPHTime, PhilippineTimeZone);
        }

        public static int GetAge(DateTime birthday)
        {
            var age = PHTime().Year - birthday.Year;
            if (birthday > PHTime().AddYears(-age)) age--;

            return age;
        }

        public static bool IsAgeInRange(DateTime birthday)
        {
            int age = GetAge(birthday);
            return age >= 18 && age <= 80;
        }
    }
}
