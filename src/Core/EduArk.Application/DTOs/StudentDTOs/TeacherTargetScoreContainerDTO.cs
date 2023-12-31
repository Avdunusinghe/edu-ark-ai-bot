namespace EduArk.Application.DTOs.StudentDTOs
{
    public class TeacherTargetScoreContainerDTO
    {
        public TeacherTargetScoreContainerDTO()
        {
            TeacherTargetScores = new List<TeacherTargetScoreDTO>();
        }
       
        public List<TeacherTargetScoreDTO> TeacherTargetScores { get; set; }
    }


}
