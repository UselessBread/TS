﻿namespace IdentityService.Data.Contracts.DTO
{
    public class CreateNewGroupRequest
    {
        public string Name { get; set; }
        public Guid TeacherId { get; set; }
    }
}
