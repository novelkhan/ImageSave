using ImageSave.Models;

namespace ImageSave.IService
{
    public interface IStudentService
    {
        Student Save(Student oStudent);
        Student GetSavedStudent();
    }
}
