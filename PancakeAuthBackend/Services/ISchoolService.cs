
namespace PancakeAuthBackend.Services {
    public interface ISchoolService {

        //GET services
        Task<bool> SchoolExists(string name);
        Task<SchoolDTO?> GetSchoolData(string name);
        Task<List<StudentDTO>> GetSchoolStudents(string schoolName);
        Task<List<StudentDTO>> GetSchoolStudentByDepartment(string schoolName, string department);
        Task<List<StudentDTO>> GetSchoolStudentsByPage(string schoolName, int pageIndex, int pageSize);
        Task<List<StudentDTO>> GetSchoolStudentByDepartmentPaged(string schoolName, string department, int pageIndex, int pageSize);

        //POST services 
        Task<bool> AddStudents(string schoolName, List<StudentDTO> studentObjects);

        //PUT servics
        Task<bool> EditStudent(StudentDTO studentObj,string SSID, string schoolName);

        //DELETE services
        Task<bool> DeleteStudent(string SUID, string schoolName);
    }
}
