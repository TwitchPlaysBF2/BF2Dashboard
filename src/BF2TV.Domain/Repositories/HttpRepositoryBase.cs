﻿using System.Text.Json;

namespace BF2TV.Domain.Repositories;

public class HttpRepositoryBase
{
    protected static readonly HttpClient HttpClient = new();
    protected static readonly JsonSerializerOptions JsonOptions = new() {AllowTrailingCommas = true};
}