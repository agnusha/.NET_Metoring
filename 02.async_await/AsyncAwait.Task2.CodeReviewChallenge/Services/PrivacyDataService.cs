using System.Threading.Tasks;

namespace AsyncAwait.Task2.CodeReviewChallenge.Services
{
    public class PrivacyDataService : IPrivacyDataService
    {
        /*ValueTask is a struct
         remove AsTask
         more memory  */
        public async Task<string> GetPrivacyDataAsync()
        {
            return await new ValueTask<string>("This Policy describes how async/await processes your personal data," +
                                            "but it may not address all possible data processing scenarios.");
        }
    }
}
