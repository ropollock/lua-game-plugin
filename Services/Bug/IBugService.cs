using System.Threading.Tasks;

namespace Lou.Services {
    public interface IBugService {
        Task<IssueResponse> Report(BugReport bugReport);
    }
}