﻿using ancanet.server.Enums;

namespace ancanet.server.Dtos.Account
{
    public record ConfigureProfileRequestDto(string UserName, string FullName, Gender Gender, DateOnly DateOfBirth);
}
