
namespace PancakeAuthBackend.Services {
    public interface ISchoolService {

        //GET services
        bool SchoolExists(string name);
        School? GetSchoolData(string name);
        List<StudentDTO> GetSchoolStudents(string schoolName);
        List<StudentDTO> GetSchoolStudentsByPage(string schoolName, int pageIndex, int pageSize);

        List<SubscriptionDTO> GetSchoolSubscriptions(string schoolName);
        List<SubscriptionDTO> GetSchoolSubscriptionsByPage(string schoolName, int pageIndex, int pageSize);

        List<BatchDTO> GetSchoolBatches(string schoolName);
        List<BatchDTO> GetSchoolBatchesByPage(string schoolName, int pageIndex, int pageSize);

        List<Payment> GetSchoolPayments(string schoolName);
        List<Payment> GetSchoolPaymentsByPage(string schoolName, int pageIndex, int pageSize);

        //POST services 
        //TODO- Check that objs don't already exist in the db, only subs available in subscript should be addable
        Task<bool> AddStudents(List<StudentDTO> studentObjects, string schoolName);
        Task<bool> AddBatch(BatchDTO batchObj, string schoolName);
    }
}
