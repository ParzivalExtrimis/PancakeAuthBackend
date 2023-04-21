namespace PancakeAuthBackend.Services {
    public interface IAdminService {

        //util
        Task<bool> SchoolExists(string name);

        //services
        //--school--
        Task<List<SchoolDTO>> GetSchools();
        Task<List<SchoolDTO>> GetSchoolsByPage(int pageIndex, int pageSize);
        Task<List<SchoolDTO>> GetSchoolsForSubscription(string subscriptionName);
        Task<List<SchoolDTO>> GetSchoolsForSubscriptionByPage(string subscriptionName, int pageIndex, int pageSize);
        Task<bool> AddSchool(SchoolDTO school);

        //TODO: Doesn't work adding a subscription replaces existing subscription. Fix
        Task<bool> EditSchool(string schoolName, SchoolDTO schoolObj);
        Task<bool> DeleteSchool(string schoolName);

        //--subscriptions--
        Task<List<SubscriptionDTO>> GetSubscriptions();
        Task<List<SubscriptionDTO>> GetSubscriptionsByPage(int pageIndex, int pageSize);
        Task<SubscriptionDTO?> FindSubscription(string name);
        Task<bool> AddSubscription(SubscriptionDTO subscription);
        Task<bool> UpdateSubscription(string subscriptionName, SubscriptionDTO subscription);
        //TODO: Foreign key constraints can't delete
        Task<bool> DeleteSubscription(string subscriptionName);

        //--chapters--
        List<ChapterDTO> GetChapters();
        List<ChapterDTO> GetChaptersByPage(int pageIndex, int pageSize);
        Task<ChapterDTO?> FindChapter(string name);
        Task<bool> AddChapters(List<ChapterDTO> chapterToAdd);
        Task<bool> DeleteChapter(string chapterName);

        //--subjects--
        List<SubjectDetailsDTO> GetSubjects();
        List<SubjectDetailsDTO> GetSubjectsByPage(int pageIndex, int pageSize);
        Task AddSubjects(List<SubjectDTO> sub);
        Task<bool> DeleteSubject(string subjectName);
    }
}
