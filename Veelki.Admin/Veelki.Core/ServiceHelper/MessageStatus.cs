namespace Veelki.Core.ServiceHelper
{
    public class MessageStatus
    {
        public static string Success
        {
            get { return "Data Retreived Successfully !"; }
            set { }
        }
        public static string Error
        {
            get { return "An Error Occurred !"; }
            set { }
        }
        public static string Create
        {
            get { return "Data Created Successfully !"; }
            set { }
        }
        public static string Update
        {
            get { return "Data Updated Successfully !"; }
            set { }
        }
        public static string Delete
        {
            get { return "Data Deleted Successfully !"; }
            set { }
        }
        public static string NotExist
        {
            get { return "Data Not Exist !"; }
            set { }
        }

        public static string NoRecord
        {
            get { return "No Data Found!"; }
            set { }
        }
        public static string InvalidData
        {
            get { return "Invalid Data Passed!"; }
            set { }
        }
        public static string Save
        {
            get { return "Data Saved Successfully"; }
            set { }
        }

        public static string Exist
        {
            get { return "Data Already Exist !"; }
            set { }
        }

        public static string SessionDateTimeExist
        {
            get { return "Session Already Exist for given Date or Time!"; }
            set { }
        }

        public static string EmailExist
        {
            get { return "Email Already Exist !"; }
            set { }
        }

        public static string DeletePSWD
        {
            get { return "Please enter currect paasword ...!"; }
            set { }
        }

        public static string Uploaded
        {
            get { return "Data Uploaded Successfully"; }
            set { }
        }

        public static string ErrorMandatoryField
        {
            get { return "Mandatory fields are null or empty"; }
            set { }
        }

        public static string ErrorProcessingImage
        {
            get { return "Error uploading Image/Image not found"; }
            set { }
        }

        public static string DateOutOfSession
        {
            get { return "Selected Date is out of Session Period !"; }
            set { }
        }

        public static string OtherSessionActive
        {
            get { return "Other seesion Active !"; }
            set { }
        }

        public static string SessionActive
        {
            get { return "Can't Delete this session, an active session Exists"; }
            set { }
        }

        public static string RecordExists
        {
            get { return "Can't Delete, Session record exists ! "; }
            set { }
        }

        public static string InvalidDelete
        {
            get { return "Can't Delete, Either Issue is Assigned Or Completed ! "; }
            set { }
        }

        public static string InvalidDeleteSuggestion
        {
            get { return "Can't Delete, Either Suggestion is Assigned Or Approved ! "; }
            set { }
        }
    }

    public static class CustomMessageStatus
    {
        public static string SessionTimeOut = "Session TimeOut";
        public static string SessionExpired = "Session Expired ";
        public static string success = "Success";
        public static string Loginsuccess = "Login Success";
        public static string Logoutsuccess = "Logout Successfully";
        public static string RefreshJWTToken = "JWT token refresh Successfully";

        public static string fail = "Fail";
        public static string error = "Error";
        public static string notFound = "No record found";

        public static string ProfileUpdated = "Profile successfully updated";
        public static string ApiAccessInvalid = "Api access token invalid";
        public static string UserSessionTokenInvalid = "Your session is time out";
        public static string FileNotValid = "Selected file type not valid";
        public static string FileSize = "Selected file must be less than ";


        //common error
        public static string isTeam = "Please select Team";
        public static string isRequest = "Please Enter RequestId";
        public static string isICAO = "ICAO Code Required.";
        public static string sameCounrty = "Same Country Group Already Exits.";
        public static string isCounrtyId = "This country id is not exist.";
        public static string isCounrtyISOCode = "Same Country ISO Code Already Exists.";
        public static string sameCounrtyName = "Country Name Already Exits.";
        public static string personName = "Person Name Already Exits.";
        public static string companyName = "Company Name Already Exits.";
        public static string vendorName = "Vendor Name Already Exits.";
        public static string fileExist = "File Already Exits.";
        public static string selectAttachments = "Please Select Attachments.";
        public static string replaceSuccessfully = "Data Replace Successfully";
        public static string idRequired = "Id required.";
        public static string setValues = "Please Set Values in Attachments.";
        public static string uploadSuccessfully = "Data Upload Successfully.";
        public static string ServiceNameExits = "Service Name Already Exits.";
        public static string locationISOCode = "Same Location ICAO Code Already Exists.";
        public static string locationIATACode = "Same Location IATA Code Already Exists.";
        public static string workspaceName = "Workspace Name Already Exits.";
        public static string isUserId = "This user id is not exist.";

        public static string InvalidModelState = "Invalid Model State.";
        public static string Authtoken = "Authentication token not found.";
        public static string InvliadLogin = "Invalid login attempt.";
        public static string InvliadEmail = "Invalid Email Id.";
        public static string Mail = "Check your mail for credential!";
        public static string logout = "User logged out.";
        public static string ConfirmEmail = "ConfirmEmail";
        public static string ConfirmEmailError = "Error in Confirmation";
        public static string userNotFound = "User not found.";
        public static string forgetMail = "Mail sent successfully. Check mail for forget password";
        public static string changeMail = "Mail sent successfully. Check mail for change password";
        public static string resetPwd = "Password reset successfull.";
        public static string oldPwd = "Old Password not match.";

        public static string attachmentDownload = "Downloading in progress.";
        public static string somethingError = "Something went wrong.";
        public static string emailInvalid = "Email address is not in correct formate.";

    }

    public static class ResponseStatusCode
    {
        public static int WARNING = -1;
        public static int OK = 200;
        public static int ERROR = 201;
        public static int SESSIONEXPIRE = 203;
        public static int NOCONTENT = 204;
        public static int USERSESSIONTOKEN = 205;
        public static int APIACCESSTOKEN = 207;
        public static int ALREADYEXISTS = 208;
        public static int BADREQUEST = 400;
        public static int UNAUTHORIZED = 401;
        public static int NOTFOUND = 404;
        public static int NOTACCEPTABLE = 406;
        public static int EXCEPTION = 417;
    }
}

