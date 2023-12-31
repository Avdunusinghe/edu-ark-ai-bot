namespace EduArk.Application.DTOs.StudentTargetSettingDTOs
{
    public class StudentTargetSettingConfigureDTO
    {
        public int CurrentAcademicYear { get; set; }
        public int AcademicLevel { get; set; }
        public int Subject { get; set; }
        public int ExamTypeId { get; set; }
        public int CurrentSemester { get; set; }


        public int? PreviousSemesterOne { get; set; }
        public int? PreviousSemesterOneAcademicYear { get; set; }

        public int? PreviousSemesterTwo { get; set; }
        public int? PreviousSemesterTwoAcademicYear { get; set; }

        public int? PreviousSemesterThree { get; set; }
        public int? PreviousSemesterThreeAcademicYear { get; set; }

        public int? PreviousSemesterFour { get; set; }
        public int? PreviousSemesterFourAcademicYear { get; set; }

        public int? PreviousSemesterFive { get; set; }
        public int? PreviousSemesterFiveAcademicYear { get; set; }
    }
}
