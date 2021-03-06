﻿namespace SparkSide.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SparkSide.Data.Common.Repositories;
    using SparkSide.Data.Models;
    using SparkSide.Services.Data.Contracts;

    public class TagsService : ITagsService
    {
        private readonly IDeletableEntityRepository<Tag> tagsRepository;
        private readonly IDeletableEntityRepository<ChallengeTag> challengeTagsRepository;

        public TagsService(
            IDeletableEntityRepository<Tag> tagsRepository,
            IDeletableEntityRepository<ChallengeTag> challengeTagsRepository)
        {
            this.tagsRepository = tagsRepository;
            this.challengeTagsRepository = challengeTagsRepository;
        }

        public async Task AddTag(int challengeId, string tag)
        {
            tag = tag.Trim();

            if (tag.Length == 0 || tag.Length > 50)
            {
                throw new ArgumentException("Invalid Tag.");
            }

            int tagId = await this.CreateAsync(tag);
            ChallengeTag entity = new ChallengeTag
            {
                ChallengeId = challengeId,
                TagId = tagId,
            };

            await this.challengeTagsRepository.AddAsync(entity);
            await this.challengeTagsRepository.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(string tagName)
        {
            Tag existingTag = this.tagsRepository
                .All()
                .Where(t => t.Title == tagName.ToLower())
                .FirstOrDefault();

            if (existingTag == null)
            {
                Tag newTag = new Tag
                {
                    Title = tagName.ToLower(),
                };
                await this.tagsRepository.AddAsync(newTag);
                await this.tagsRepository.SaveChangesAsync();

                return newTag.Id;
            }
            else
            {
                return existingTag.Id;
            }
        }

        public async Task UpdateTagsAsync(Challenge challenge, ICollection<string> tags)
        {
            List<ChallengeTag> currentTags = challenge.Tags.ToList();
            List<string> currentTagsNames = currentTags.Select(c => c.Tag.Title).ToList();

            foreach (ChallengeTag challengeTag in currentTags)
            {
                if (!tags.Contains(challengeTag.Tag.Title))
                {
                    this.challengeTagsRepository.HardDelete(challengeTag);
                }
            }

            for (int i = 0; i < tags.Count; i++)
            {
                string tag = tags.ElementAt(i);

                if (!currentTagsNames.Contains(tag))
                {
                    await this.AddTag(challenge.Id, tag);
                }
            }

            await this.challengeTagsRepository.SaveChangesAsync();
        }
    }
}