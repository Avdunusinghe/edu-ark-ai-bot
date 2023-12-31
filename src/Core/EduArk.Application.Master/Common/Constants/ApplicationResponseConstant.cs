namespace EduArk.Application.Master.Common.Constants
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

        #region Tenant
        public const string TENANT_DETAILS_SAVE_SUCCESS_RESPONSE_MESSAGE =
                               "Tenant details saved has been successfull";

        public const string TENANT_DETAILS_UPDATE_SUCCESS_RESPONSE_MESSAGE =
                              "Tenant details updated has been successfull";

        public const string TENANT_DOMAIN_ALL_READY_EXSIST_RESPONSE_MESSAGE =
                              "Domain name all ready exsist";





        #endregion
    }
}
