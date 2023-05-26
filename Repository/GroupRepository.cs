﻿using ServiceManagerApi.Data;
using ServiceManagerApi.Interfaces;

namespace ServiceManagerApi.Repository
{
    public class GroupRepository: GenericRepositoryAsync<Group>, IGroupRepository
    {
        public GroupRepository(EnpDbContext context) : base(context) { }
    }
}
