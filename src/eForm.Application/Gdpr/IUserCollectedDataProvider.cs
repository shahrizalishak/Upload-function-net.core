﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using eForm.Dto;

namespace eForm.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
