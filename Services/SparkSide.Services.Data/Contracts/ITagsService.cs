namespace SparkSide.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITagsService
    {
        Task AddTag(int challengeId, string tag);

        Task<int> CreateAsync(string tagName);
    }
}
