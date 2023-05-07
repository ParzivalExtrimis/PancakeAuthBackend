namespace PancakeAuthBackend.Services {
    public interface IAdminService {

        //util
        Task<bool> SchoolExists(string name);

        //services
        //--school--
        Task<List<SchoolDTO>> GetSchools();
        Task<List<SchoolDTO>> GetSchoolsByPage(int pageIndex, int pageSize);
        Task<bool> AddSchool(RegisterSchoolDTO school);

        //TODO: Doesn't work adding a subscription replaces existing subscription. Fix
        Task<bool> EditSchool(string schoolName, SchoolDTO schoolObj);
        Task<bool> DeleteSchool(string schoolName);

        //--chapters--
        List<ChapterDTO> GetChapters();
        Task<List<ChapterDTO>> GetChaptersBySubject(string subject);
        List<ChapterDTO> GetChaptersByPage(int pageIndex, int pageSize);
        Task<ChapterDTO?> FindChapter(string name);
        Task<bool> AddChapters(List<ChapterDTO> chapterToAdd);
        Task<bool> DeleteChapter(string chapterName);

        //--subjects--
        Task<List<SubjectDTO>> GetSubjects();
        Task<List<SubjectDTO>> GetSubjectsByPage(int pageIndex, int pageSize);
        Task AddSubjects(List<SubjectDTO> sub);
        Task<bool> DeleteSubject(string subjectName);
    }
}
