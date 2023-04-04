
namespace PancakeAuthBackend.Services {
    public interface ISchoolService {

        //GET services
        Task<bool> SchoolExists(string name);
        Task<SchoolDTO?> GetSchoolData(string name);
        List<StudentDTO> GetSchoolStudents(string schoolName);
        List<StudentDTO> GetSchoolStudentsByPage(string schoolName, int pageIndex, int pageSize);

        List<SubscriptionDTO> GetSchoolSubscriptions(string schoolName);
        List<SubscriptionDTO> GetSchoolSubscriptionsByPage(string schoolName, int pageIndex, int pageSize);

        List<BatchDTO> GetSchoolBatches(string schoolName);
        List<BatchDTO> GetSchoolBatchesByPage(string schoolName, int pageIndex, int pageSize);

        List<Payment> GetSchoolPayments(string schoolName);
        List<Payment> GetSchoolPaymentsByPage(string schoolName, int pageIndex, int pageSize);

        //POST services 
        Task<bool> AddStudents(List<StudentDTO> studentObjects, string schoolName);
        Task<bool> AddBatch(BatchDTO batchObj, string schoolName);

        //PUT servics
        Task<bool> EditStudent(StudentDTO studentObj, string schoolName);
        Task<bool> EditBatch(List<string> subjects, string batchName, string schoolName);

        //DELETE services
        Task<bool> DeleteStudent(string SUID, string schoolName);
    }
}
