namespace PancakeAuthBackend.Services {
    public interface IAdminService {

        //util
        Task<bool> SchoolExists(string name);
        Task<bool> AddSchool(SchoolDTO school);
        //TODO: Doesn't work adding a subscription replaces existing subscription. Fix
        Task<bool> EditSchool(string schoolName, SchoolDTO schoolObj);
        Task<bool> DeleteSchool(string schoolName)
            ;
    }
}
