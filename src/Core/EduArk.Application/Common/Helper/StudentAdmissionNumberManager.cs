namespace EduArk.Application.Common.Helper
{
    public class StudentAdmissionNumberManager
    {
        // Static counter to keep track of the number of admissions
        private static int admissionCounter = 0;

        // Method to generate a unique student admission number
        public static string GenerateUniqueAdmissionNumber()
        {
            // Get the current DateTime
            DateTime now = DateTime.Now;

            // Format the DateTime as a string without any special characters
            string formattedDateTime = now.ToString("yyyyMMddHHmmss");

            // Generate a unique identifier (can be a random number or a counter)
            string uniqueIdentifier = (++admissionCounter).ToString("D4"); // D4 ensures a 4-digit number with leading zeros if needed

            // Combine the DateTime and unique identifier to create the final admission number
            string admissionNumber = formattedDateTime + uniqueIdentifier;

            return admissionNumber;
        }
    }
}
