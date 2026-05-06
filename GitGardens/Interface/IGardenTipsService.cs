using GitGardens.Models;

namespace GitGardens.Interface
{
    public interface IGardenTipsService
    {
        Task<List<string>> GetTipsAsync(int gardenId);
    }
}
