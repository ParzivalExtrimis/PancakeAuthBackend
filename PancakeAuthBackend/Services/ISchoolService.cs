
namespace PancakeAuthBackend.Services {
    public interface ISchoolService {

        //GET services
        Task<bool> SchoolExists(string name);
        Task<SchoolDTO?> GetSchoolData(string name);
        Task<List<StudentDTO>> GetSchoolStudents(string schoolName);

        Task<List<StudentDTO>> GetSchoolStudentByGrade(string schoolName, string grade);
        Task<List<StudentDTO>> GetSchoolStudentByBatch(string schoolName, string batch);

        Task<List<StudentDTO>> GetSchoolStudentsByPage(string schoolName, int pageIndex, int pageSize);
        Task<List<StudentDTO>> GetSchoolStudentByGradePaged(string schoolName, string grade, int pageIndex, int pageSize);
        Task<List<StudentDTO>> GetSchoolStudentByBatchPaged(string schoolName, string batch, int pageIndex, int pageSize);

        Task<List<SubscriptionDTO>> GetSchoolSubscriptions(string schoolName);

        Task<List<BillingDTO>> GetSchoolBillings(string schoolName);
        Task<List<BillingDTO>> GetSchoolBillingsByPage(string schoolName, int pageIndex, int pageSize);

        //POST services 
        Task<bool> AddStudents(List<StudentDTO> studentObjects, string batchName);
        Task<bool> AddBatch(BatchDTO batchObj, string schoolName);

        //PUT servics
        Task<bool> EditStudent(StudentDTO studentObj,string SSID, string schoolName);

        //DELETE services
        Task<bool> DeleteStudent(string SUID, string schoolName);
    }
}
