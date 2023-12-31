namespace EduArk.Application.Common.Constants
{
    public class ApplicationResponseConstant
    {


        #region Common
        public const string COMMON_EXCEPTION_RESPONSE_MESSAGE =
                                "Error has been occured, please try again";

        #endregion

        #region Users
        public const string SCHOOL_DOMAIN_NOT_VALID_EXCEPTION_RESPONSE_MESSAGE =
                              "School domain not found, please try again";

        public const string USERNAME_NOT_VALID_EXCEPTION_RESPONSE_MESSAGE =
                                "User name not exising system, please try again";

        public const string PASSWORD_NOT_VALID_EXCEPTION_RESPONSE_MESSAGE =
                               "Password incorrect, please try again";

        public const string USER_DETAILS_SAVE_SUCCESS_RESPONSE_MESSAGE =
                               "User details saved has been successfull";

        public const string USER_NOT_EXSISTING_THE_SYSTEM_RESPONSE_MESSAGE =
                               "User details Not found please try again";

        public const string USER_DELETE_SUCCESS_RESPONSE_MEESSAGE =
                              "User details Not found please try again";
        #endregion

        #region Lessons
        public const string LEARNIG_PLAN_DETAILS_SAVE_SUCCESS_RESPONSE_MESSAGE =
                               "Saving of lessons details has been successful";
        
        public const string LEARNING_PLAN_NOT_EXSISTING_THE_SYSTEM_RESPONSE_MESSAGE =
                                "Learnig Plan details Not found please try again";

        public const string LEARNING_PLAN_DELETE_SUCCESS_RESPONSE_MEESSAGE =
                                "Learning Plan details deleted";

        public const string LEARNIG_PLAN_UPDATE_SUCCESS_RESPONSE_MESSAGE =
                                "Learning Plan details has been updated!";

        public const string LEARNING_PLAN_GET_GY_ID_RESPONSE_MESSAGE =
                                "Learning Plan get by id has been updated.";


        
        public const string LESSON_NOT_EXSISTING_THE_SYSTEM_RESPONSE_MESSAGE =
                                "Lesson Not found please try again";

        public const string LESSON_DELETE_SUCCESS_RESPONSE_MEESSAGE =
                                "The lesson has been successfully deleted.";

        public const string LESSON_SAVE_SUCCESS_RESPONSE_MESSAGE =
                               "Saving of lesson has been successful";

        public const string LESSON_GET_BY_ID_RESPONSE_MESSAGE =
                                "Lesson get by id has been selected.";

        public const string LESSON_UPDATE_SUCCESS_RESPONSE_MESSAGE =
                                "Lesson has been updated!";


        #endregion

        #region Academic Level
        public const string ACADEMIC_LEVEL_SAVE_SUCCESS_RESPONSE_MESSAGE =
                          "Academic Level saved has been successfully";
        public const string ACADEMIC_LEVEL_UPDATE_SUCCESS_RESPONSE_MESSAGE =
                           "Academic Level updated has been  successfully";
        public const string ACADEMIC_LEVEL_NOT_AVAILABLE_RESPONSE_MESSAGE =
                          "Cannot find academic level please try again";
        public const string ACADEMIC_LEVEL_DELETE_SUCCESS_RESPONSE_MESSAGE =
                          "Academic Level deleted has been  successfully";
        #endregion

        #region Subject
        public const string SUBJECT_SAVE_SUCCESS_RESPONSE_MESSAGE =
                         "Subject saved has been successfully";
        public const string SUBJECT_UPDATE_SUCCESS_RESPONSE_MESSAGE =
                           "Subject updated has been  successfully";
        public const string SUBJECT_NOT_AVAILABLE_RESPONSE_MESSAGE =
                          "Cannot find Subject please try again";
        public const string SUBJECT_DELETE_SUCCESS_RESPONSE_MESSAGE =
                          "Subject deleted has been  successfully";
        #endregion

        #region ClassName
        public const string CLASS_NAME_SAVE_SUCCESS_RESPONSE_MESSAGE =
                            "Class Name saved has been successfully";
        public const string CLASS_NAME_UPDATE_SUCCESS_RESPONSE_MESSAGE =
                           "Class Name updated has been  successfully";
        public const string CLASS_NAME_NOT_AVAILABLE_RESPONSE_MESSAGE =
                          "Cannot find Class Name please try again";
        public const string CLASS_NAME_DELETE_SUCCESS_RESPONSE_MESSAGE =
                          "Class Name deleted has been  successfully";
        #endregion

        #region Class
        public const string CLASS_SAVE_SUCCESS_RESPONSE_MESSAGE =
                          "Class  has been saved successfully";
        public const string CLASS_UPDATE_SUCCESS_RESPONSE_MESSAGE =
                           "Class  has been updated successfully";
        public const string CLASS_NOT_AVAILABLE_RESPONSE_MESSAGE =
                          "Cannot find Class please try again";
        public const string CLASS_DELETE_SUCCESS_RESPONSE_MESSAGE =
                          "Class  has been deleted  successfully";
        #endregion

        #region Subject Teachers
        public const string SUBJECT_TEACHER_DETAILS_SAVE_SUCCESS_RESPONSE_MESSAGE =
                         "Subject Teachers details has been saved successfully"; 
        #endregion


        public const string ASSESSMENTS_NOT_EXSISTING_THE_SYSTEM_RESPONSE_MESSAGE =
                                "Assessment details Not found please try again";

        public const string ASSESSMENTS_DELETE_SUCCESS_RESPONSE_MEESSAGE =
                                "Assessments Not found please try again";

        public const string ASSESSMENTS_UPDATE_SUCCESS_RESPONSE_MESSAGE =
                                "Assessments has been updated!";

        public const string ASSESSMENTS_GET_GY_ID_RESPONSE_MESSAGE =
                                "Assessments  get by id has been updated.!";

        public static string ASSESSMENTS_SAVE_SUCCESS_RESPONSE_MESSAGE { get; internal set; }
    }
}
